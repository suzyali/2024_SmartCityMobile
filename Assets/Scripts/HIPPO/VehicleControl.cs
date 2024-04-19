using UnityEngine;

public class CarController : MonoBehaviour
{
    public float maxTorque = 1500f; // 최대 토크
    public float maxSpeed = 30f; // 최고 속도 (단위: m/s)
    public float rotationSpeed = 100.0f; // 회전 속도

    private Rigidbody rb;

    void Start()
    {
        rb = gameObject.AddComponent<Rigidbody>();
        rb.mass = 5000f; // 차량의 질량을 5톤으로 설정
        rb.drag = 0.5f; // 공기 저항을 추가하여 실제 차량처럼 느껴지도록 함
        rb.angularDrag = 0.5f; // 회전 시의 저항도 설정
        rb.centerOfMass = Vector3.down;
    }

    void FixedUpdate()
    {
        float moveVertical = Input.GetAxis("Vertical");

        // 현재 속도가 최고 속도 미만인 경우에만 토크를 적용
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(transform.forward * moveVertical * maxTorque);
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float rotation = moveHorizontal * rotationSpeed * Time.deltaTime;
        Quaternion turn = Quaternion.Euler(0f, rotation, 0f);
        rb.MoveRotation(rb.rotation * turn);
    }
}
