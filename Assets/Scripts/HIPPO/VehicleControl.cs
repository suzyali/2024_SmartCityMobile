using UnityEngine;

public class CarController : MonoBehaviour
{
    public float maxTorque = 1500f; // �ִ� ��ũ
    public float maxSpeed = 30f; // �ְ� �ӵ� (����: m/s)
    public float rotationSpeed = 100.0f; // ȸ�� �ӵ�

    private Rigidbody rb;

    void Start()
    {
        rb = gameObject.AddComponent<Rigidbody>();
        rb.mass = 5000f; // ������ ������ 5������ ����
        rb.drag = 0.5f; // ���� ������ �߰��Ͽ� ���� ����ó�� ���������� ��
        rb.angularDrag = 0.5f; // ȸ�� ���� ���׵� ����
        rb.centerOfMass = Vector3.down;
    }

    void FixedUpdate()
    {
        float moveVertical = Input.GetAxis("Vertical");

        // ���� �ӵ��� �ְ� �ӵ� �̸��� ��쿡�� ��ũ�� ����
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
