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

        // 입력 필드에서 위도와 경도 가져오기
        if (double.TryParse(latitudeInput.text, out latitude) && double.TryParse(longitudeInput.text, out longitude))
        {
            // GeoPath에 새로운 위치 추가
            geoPath.AddLocation(latitude, longitude);

            // 입력 필드 초기화
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
