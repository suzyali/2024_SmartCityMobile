using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using SimpleJSON;
using System.IO;
using UnityEngine.ProBuilder.MeshOperations;

public class GeoJsonToProBuilder : MonoBehaviour
{
    public string geoJsonFilePath = "Data/��⵵_������_�д籸_�ǹ�2.geojson";
    public Material polygonMaterial; // Unity Inspector���� ����

    void Start()
    {
        LoadAndCreatePolygons();
    }

    void LoadAndCreatePolygons()
    {
        // GeoJSON ���� �ε�
        string geoJsonString = File.ReadAllText(geoJsonFilePath);
        var geoJson = JSON.Parse(geoJsonString);

        // 'features' �迭 ��ȸ
        foreach (JSONNode feature in geoJson["features"].AsArray)
        {
            CreatePolygon(feature);
        }
    }

    void CreatePolygon(JSONNode feature)
    {
        // 'geometry'�� 'coordinates' ���� �� ��ȯ
        var coordinates = feature["geometry"]["coordinates"][0][0].AsArray;
        List<Vector3> points = new List<Vector3>();
        foreach (JSONNode coordinate in coordinates)
        {
            float x = coordinate[0].AsFloat - 202214.9937f;
            float z = coordinate[1].AsFloat - 531996.3721f;
            points.Add(new Vector3(x, 0, z));
        }

        // ������ ����
        string polyShapeName = feature["properties"]["A0"];
        GameObject polyShapeObject = new GameObject(polyShapeName);
        ProBuilderMesh pbMesh = polyShapeObject.AddComponent<ProBuilderMesh>();

        // ������ ��� ���� �� ����
        float extrudeDistance = feature["properties"]["A17"].AsFloat;
        pbMesh.CreateShapeFromPolygon(points.ToArray(), extrudeDistance, flipNormals: false);

        // �޽� �������� ��� �Ҵ� �� ���̿� ���� ���� ����
      AssignMaterialWithHeight(polyShapeObject, extrudeDistance);
    }

    void AssignMaterialWithHeight(GameObject polyShapeObject, float height)
    {
        var meshRenderer = polyShapeObject.GetComponent<MeshRenderer>();
        if (polygonMaterial != null && meshRenderer != null)
        {
            meshRenderer.material = polygonMaterial;

            // ���̿� ���� ������ �����մϴ�. ���̰��� ���� RGB�� ��ȭ��Ű��
            // ���� ���, ���̰� ������ �Ķ���, �߰��� �ʷϻ�, ������ ���������� ��ȭ
            float normalizedHeight = Mathf.Clamp(height / 100.0f, 0f, 1f); // 0.0���� 1.0 ���̷� ���̰��� ����ȭ
            Color colorBasedOnHeight = Color.Lerp(Color.blue, Color.gray, normalizedHeight); // �Ķ������� �ʷϻ�����
            colorBasedOnHeight = Color.Lerp(colorBasedOnHeight, Color.green, normalizedHeight * normalizedHeight); // �ʷϿ��� �������� �ε巴�� ��ȭ

            meshRenderer.material.color = colorBasedOnHeight;
        }
        else
        {
            Debug.LogWarning("Polygon material is not set in the Inspector, or MeshRenderer is missing.");
        }
    }

}
