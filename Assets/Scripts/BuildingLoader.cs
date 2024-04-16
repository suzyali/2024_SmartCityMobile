using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class BuildingLoader : MonoBehaviour
{
    public string  filePath = "Data/��⵵_������_�д籸_�ǹ�.geojson";

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
            // ����: GeoJSON ���Ͽ� ����Ʈ Ÿ���� ��ó�� �ְ�, �� ��ó�� ��ġ�� ������ �ִ�.
            float latitude = feature["geometry"]["coordinates"][1].AsFloat;
            float longitude = feature["geometry"]["coordinates"][0].AsFloat;

            // ������ ����, ������ �浵�� �������� ����Ƽ ���忡���� ��ġ�� ����ϰ�, ť�긦 ����
            Vector3 position = GeoToWorldPosition(latitude, longitude);
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = position;
        }
    }

    Vector3 GeoToWorldPosition(float latitude, float longitude)
    {
        // �� �Լ��� ������ �浵�� �޾� ����Ƽ�� ���� ���������� ��ȯ�մϴ�.
        // ���� ������Ʈ������ ���� �����Ϳ� �����Ͽ� ���� ��ȯ ����� �����ؾ� �մϴ�.
        return new Vector3(longitude, 0, latitude);
    }
}