using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 5.0f; // 카메라의 이동 속도
    public float rotationSpeed = 100.0f; // 카메라의 회전 속도
    public float zoomSpeed = 10.0f; // 카메라의 줌 속도

    Camera cameraComponent; // 카메라 컴포넌트

    void Start()
    {
        cameraComponent = GetComponent<Camera>(); // 카메라 컴포넌트 가져오기
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D 키 입력 감지
        float verticalInput = Input.GetAxis("Vertical"); // W/S 키 입력 감지
        float ascendInput = 0;

        if (Input.GetKey(KeyCode.Q)) // Q 키 입력 감지
        {
            ascendInput = 1;
        }
        else if (Input.GetKey(KeyCode.E)) // E 키 입력 감지
        {
            ascendInput = -1;
        }

        Vector3 movement = new Vector3(horizontalInput, ascendInput, verticalInput) * speed * Time.deltaTime;
        transform.Translate(movement, Space.Self); // 카메라 이동

        float mouseY = Input.GetAxis("Mouse Y"); // 마우스의 수직 움직임 감지

        transform.Rotate(new Vector3(-mouseY, 0, 0) * Time.deltaTime * rotationSpeed, Space.Self); // 마우스 움직임에 따른 카메라 회전(상하만 가능)

        float zoomChange = -Input.GetAxis("Mouse ScrollWheel"); // 마우스 휠 입력 감지
        cameraComponent.fieldOfView += zoomChange * zoomSpeed * Time.deltaTime; // 줌인/줌아웃
        cameraComponent.fieldOfView = Mathf.Clamp(cameraComponent.fieldOfView, 15, 90); // field of view 값을 15와 90 사이로 제한
    }
}