using UnityEngine;

using System.Collections;

using UnityEngine.UI;

using System;
using TMPro;

public class GPSLocation : MonoBehaviour
{

    bool gpsInit = false;

    LocationInfo currentGPSPosition;

    int gps_connect = 0;
    double detailed_num = 1.0;//���� ��ǥ�� float������ �Ҽ��� �ڸ��� ���� �ڼ��� ��µǴ� double�� ���Ͽ� �ڼ��� ���� ���մϴ�.
    //public Text text_latitude;
    //public Text text_longitude;
    //public Text text_refresh;
    //public TMP_Text text_GPSStstus;
    public TMP_Text text_latitude;
    public TMP_Text text_longitude;
    //public TMP_Text text_altitude;
    //public TMP_Text text_HorizontalAccuracyValue;
    //public TMP_Text text_refresh;
    // Use this for initialization

    void Start()

    {

        Input.location.Start(0.5f);



        int wait = 1000; // �⺻ ��

        // Checks if the GPS is enabled by the user (-> Allow location ) 

        if (Input.location.isEnabledByUser)//����ڿ� ���Ͽ� ��ǥ���� ���� �� �� ���� ���

        {

            while (Input.location.status == LocationServiceStatus.Initializing && wait > 0)//�ʱ�ȭ �������̸�

            {

                wait--; // ��ٸ��� �ð��� ����

            }

            //GPS�� ��� ���ð�

            if (Input.location.status != LocationServiceStatus.Failed)//GPS�� �������̶��

            {

                gpsInit = true;

                // We start the timer to check each tick (every 3 sec) the current gps position

                InvokeRepeating("RetrieveGPSData", 0.0001f, 1.0f);//0.0001�ʿ� �����ϰ� 1�ʸ��� �ش� �Լ��� �����մϴ�.

            }

        }

        else//GPS�� ���� ��� (GPS�� ���� ���ų� �ȵ���̵� GPS�� ���� ���� �ʾ��� ���

        {

            text_latitude.text = "GPS not available";

            text_longitude.text = "GPS not available";

        }

    }

    void RetrieveGPSData()

    {
        currentGPSPosition = Input.location.lastData;//gps�� �����͸� �޽��ϴ�.
        if (Input.location.status == LocationServiceStatus.Running)
        {
            //text_GPSStstus.text = "Running";
        }

        text_latitude.text = (currentGPSPosition.latitude).ToString();
        //���� ���� �޾�,�ؽ�Ʈ�� ����մϴ�

        text_longitude.text = (currentGPSPosition.longitude).ToString();
        //�浵 ���� �޾�, �ؽ�Ʈ�� ����մϴ�.

        //text_altitude.text = (currentGPSPosition.altitude).ToString();
        //�� ���� �޾�, �ؽ�Ʈ�� ����մϴ�.
        //text_HorizontalAccuracyValue.text = (currentGPSPosition.horizontalAccuracy).ToString();

        gps_connect++;

        //text_refresh.text = gps_connect.ToString();

    }

}
