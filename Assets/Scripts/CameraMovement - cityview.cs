using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 5.0f; // ī�޶��� �̵� �ӵ�
    public float rotationSpeed = 100.0f; // ī�޶��� ȸ�� �ӵ�
    public float zoomSpeed = 10.0f; // ī�޶��� �� �ӵ�

    Camera cameraComponent; // ī�޶� ������Ʈ

    void Start()
    {
        cameraComponent = GetComponent<Camera>(); // ī�޶� ������Ʈ ��������
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D Ű �Է� ����
        float verticalInput = Input.GetAxis("Vertical"); // W/S Ű �Է� ����
        float ascendInput = 0;

        if (Input.GetKey(KeyCode.Q)) // Q Ű �Է� ����
        {
            ascendInput = 1;
        }
        else if (Input.GetKey(KeyCode.E)) // E Ű �Է� ����
        {
            ascendInput = -1;
        }

        Vector3 movement = new Vector3(horizontalInput, ascendInput, verticalInput) * speed * Time.deltaTime;
        transform.Translate(movement, Space.Self); // ī�޶� �̵�

        float mouseY = Input.GetAxis("Mouse Y"); // ���콺�� ���� ������ ����

        transform.Rotate(new Vector3(-mouseY, 0, 0) * Time.deltaTime * rotationSpeed, Space.Self); // ���콺 �����ӿ� ���� ī�޶� ȸ��(���ϸ� ����)

        float zoomChange = -Input.GetAxis("Mouse ScrollWheel"); // ���콺 �� �Է� ����
        cameraComponent.fieldOfView += zoomChange * zoomSpeed * Time.deltaTime; // ����/�ܾƿ�
        cameraComponent.fieldOfView = Mathf.Clamp(cameraComponent.fieldOfView, 15, 90); // field of view ���� 15�� 90 ���̷� ����
    }
}