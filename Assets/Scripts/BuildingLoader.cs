using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class BuildingLoader : MonoBehaviour
{
    public string  filePath = "Data/경기도_성남시_분당구_건물.geojson";

    void Start()
    {
        LoadGeoJson();
    }

    void LoadGeoJson()
    {
        string fullPath = Path.Combine(Application.dataPath, filePath);
        if (File.Exists(fullPath))
        {
            string dataAsJson = File.ReadAllText(fullPath);
            ParseGeoJson(dataAsJson);
        }
        else
        {
            Debug.LogError("Cannot find GeoJSON file!");
        }
    }

    void ParseGeoJson(string jsonString)
    {
        var N = JSON.Parse(jsonString);
        foreach (JSONNode feature in N["features"].AsArray)
        {
            // 가정: GeoJSON 파일에 포인트 타입의 피처가 있고, 각 피처가 위치를 가지고 있다.
            float latitude = feature["geometry"]["coordinates"][1].AsFloat;
            float longitude = feature["geometry"]["coordinates"][0].AsFloat;

            // 간단한 예로, 위도와 경도를 바탕으로 유니티 월드에서의 위치를 계산하고, 큐브를 생성
            Vector3 position = GeoToWorldPosition(latitude, longitude);
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = position;
        }
    }

    Vector3 GeoToWorldPosition(float latitude, float longitude)
    {
        // 이 함수는 위도와 경도를 받아 유니티의 월드 포지션으로 변환합니다.
        // 실제 프로젝트에서는 지도 데이터와 스케일에 따라 변환 방식을 조정해야 합니다.
        return new Vector3(longitude, 0, latitude);
    }
}