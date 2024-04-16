using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class gyroSensor : MonoBehaviour
{
    private Gyroscope gyro;

    public Text gyroText_x;
    public Text gyroText_y;
    public Text gyroText_z;

    private float logInterval = 10f; // �α� ���� (��)
    private float timer = 0f;
    private string filePath;


    void Start()
    {
        // ���̷ν������� �����Ǵ��� Ȯ��
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true; // ���̷ν����� Ȱ��ȭ
        }
        else
        {
            Debug.Log("���̷ν������� �������� �ʴ� ����Դϴ�.");
        }

        // ���� ��� ���� (�� ���� ���丮�� ����)
        filePath = Application.persistentDataPath + "/gyro_log.txt";

        // �� ó���� �α� ����


    }

    void Update()
    {
        // ���̷� ���� �� �б�
        if (gyro != null)
        {
            // ���̷ν������� ȸ�� ���� ������
            Quaternion gyroAttitude = gyro.attitude;
            // ���̷ν����� ���� ����Ͽ� ������Ʈ�� ȸ����ų �� ����
            transform.rotation = gyroAttitude;

            // ���̷� ���� �� ���

            gyroText_x.text = "X: " + gyroAttitude.x.ToString("F2");
            gyroText_y.text = "Y: " + gyroAttitude.y.ToString("F2");
            gyroText_z.text = "Z: " + gyroAttitude.z.ToString("F2");

            // �α� ���� Ÿ�̸� ������Ʈ
            timer += Time.deltaTime;

            // �α� ������ ���� ���
            if (timer >= logInterval)
            {
                timer -= logInterval; // Ÿ�̸� �缳��
                SaveGyroLog(gyroAttitude); // �α� ����
            }
        }
    }


    void SaveGyroLog(Quaternion gyroAttitude)
    {
        // �α׸� ���Ͽ� ����
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine("Time: " + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                             ", X: " + gyroAttitude.x.ToString("F2") +
                             ", Y: " + gyroAttitude.y.ToString("F2") +
                             ", Z: " + gyroAttitude.z.ToString("F2"));
        }
    }


}
