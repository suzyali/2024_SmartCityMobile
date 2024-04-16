using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using SimpleJSON;
using System.IO;
using UnityEngine.ProBuilder.MeshOperations;

public class GeoJsonToProBuilder2 : MonoBehaviour
{
    public string geoJsonFilePath = "Data/경기도_성남시_분당구_건물2.geojson";
    public Material polygonMaterial; // Unity Inspector에서 설정

    void Start()
    {
        LoadAndCreatePolygons();
    }

    public void LoadAndCreatePolygons()
    {
        // GeoJSON 파일 로드
        string geoJsonString = File.ReadAllText(geoJsonFilePath);
        var geoJson = JSON.Parse(geoJsonString);

        // 'features' 배열 순회
        foreach (JSONNode feature in geoJson["features"].AsArray)
        {
            CreatePolygon(feature);
        }
    }

    public void CreatePolygon(JSONNode feature)
    {
      

        // 'geometry'의 'coordinates' 추출 및 변환
        var coordinates = feature["geometry"]["coordinates"][0][0].AsArray;
        List<Vector3> points = new List<Vector3>();
        foreach (JSONNode coordinate in coordinates)
        {
            float x = coordinate[0].AsFloat - 202214.9937f;
            float z = coordinate[1].AsFloat - 531996.3721f;
            points.Add(new Vector3(x, 0, z));
        }

        // 폴리곤 생성
        string polyShapeName = feature["properties"]["A0"];
        GameObject polyShapeObject = new GameObject(polyShapeName);
        ProBuilderMesh pbMesh = polyShapeObject.AddComponent<ProBuilderMesh>();

        // 폴리곤 경로 설정 및 생성
        float extrudeDistance = feature["properties"]["A17"].AsFloat;
        pbMesh.CreateShapeFromPolygon(points.ToArray(), extrudeDistance, flipNormals: false);

        // 메시 렌더러에 재료 할당 및 높이에 따라 색상 변경
       AssignMaterialWithHeight(polyShapeObject, extrudeDistance);
    }

    void AssignMaterialWithHeight(GameObject polyShapeObject, float height)
    {
        var meshRenderer = polyShapeObject.GetComponent<MeshRenderer>();
        if (polygonMaterial != null && meshRenderer != null)
        {
            meshRenderer.material = polygonMaterial;

            // 높이에 따라 색상을 조절합니다. 예를 들어, 높이가 클수록 더 밝은 색을 사용합니다.
            float colorValue = Mathf.Clamp(height / 10.0f, 0.0f, 1.0f); // 색상 값을 0.0에서 1.0 사이로 제한합니다.
            Color colorBasedOnHeight = new Color(colorValue, colorValue, colorValue, 1.0f);
            meshRenderer.material.color = colorBasedOnHeight;
        }
        else
        {
            Debug.LogWarning("Polygon material is not set in the Inspector, or MeshRenderer is missing.");
        }
    }
}
