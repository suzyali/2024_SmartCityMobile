using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class GeoPathEditor : MonoBehaviour
{
    public GeoPath geoPath;
    public InputField latitudeInput;
    public InputField longitudeInput;

    public TextMeshProUGUI locationtext;
    double latitude, longitude;


    public void Start()
    {
        Input.location.Start();

    }

    public void AddLocationFromInput()
    {

        // �Է� �ʵ忡�� ������ �浵 ��������
        if (double.TryParse(latitudeInput.text, out latitude) && double.TryParse(longitudeInput.text, out longitude))
        {
            // GeoPath�� ���ο� ��ġ �߰�
            geoPath.AddLocation(latitude, longitude);

            // �Է� �ʵ� �ʱ�ȭ
            latitudeInput.text = "";
            longitudeInput.text = "";
        }
        else
        {
            Debug.LogError("Invalid latitude or longitude input.");
        }
    }

    public void AddLastlocation()
    {

        float latitude = Input.location.lastData.latitude;
        float longitude = Input.location.lastData.longitude;
        geoPath.AddLocation(latitude, longitude);
        locationtext.text = $"lat:{latitude},lon:{longitude}";

    }



}
