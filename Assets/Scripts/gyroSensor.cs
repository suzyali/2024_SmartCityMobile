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

    private float logInterval = 10f; // 로그 간격 (초)
    private float timer = 0f;
    private string filePath;


    void Start()
    {
        // 자이로스코프가 지원되는지 확인
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true; // 자이로스코프 활성화
        }
        else
        {
            Debug.Log("자이로스코프가 지원되지 않는 기기입니다.");
        }

        // 파일 경로 설정 (앱 실행 디렉토리에 저장)
        filePath = Application.persistentDataPath + "/gyro_log.txt";

        // 맨 처음에 로그 저장


    }

    void Update()
    {
        // 자이로 센서 값 읽기
        if (gyro != null)
        {
            // 자이로스코프의 회전 값을 가져옴
            Quaternion gyroAttitude = gyro.attitude;
            // 자이로스코프 값을 사용하여 오브젝트를 회전시킬 수 있음
            transform.rotation = gyroAttitude;

            // 자이로 센서 값 출력

            gyroText_x.text = "X: " + gyroAttitude.x.ToString("F2");
            gyroText_y.text = "Y: " + gyroAttitude.y.ToString("F2");
            gyroText_z.text = "Z: " + gyroAttitude.z.ToString("F2");

            // 로그 간격 타이머 업데이트
            timer += Time.deltaTime;

            // 로그 간격이 지난 경우
            if (timer >= logInterval)
            {
                timer -= logInterval; // 타이머 재설정
                SaveGyroLog(gyroAttitude); // 로그 저장
            }
        }
    }


    void SaveGyroLog(Quaternion gyroAttitude)
    {
        // 로그를 파일에 저장
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine("Time: " + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                             ", X: " + gyroAttitude.x.ToString("F2") +
                             ", Y: " + gyroAttitude.y.ToString("F2") +
                             ", Z: " + gyroAttitude.z.ToString("F2"));
        }
    }


}
