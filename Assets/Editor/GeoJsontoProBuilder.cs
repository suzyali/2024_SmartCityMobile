using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using SimpleJSON;
using System.IO;
using UnityEngine.ProBuilder.MeshOperations;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GeoJsonToProBuilder : MonoBehaviour
{
    public string geoJsonFilePath = "Assets/Data/경기도_성남시_분당구_건물2.geojson";
    public Material polygonMaterial; // Unity Inspector에서 설정

    void Start()
    {
        // GeoJSON 파일 로드
        string geoJsonString = File.ReadAllText(geoJsonFilePath);
        var geoJson = JSON.Parse(geoJsonString);

        // 'features' 배열 순회
        foreach (JSONNode feature in geoJson["features"].AsArray)
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
            string polyShapeName = feature["properties"]["A9"];
            GameObject polyShapeObject = new GameObject(polyShapeName);
            ProBuilderMesh pbMesh = polyShapeObject.AddComponent<ProBuilderMesh>();

            // 폴리곤 경로 설정 및 생성
            float extrudeDistance = feature["properties"]["A17"].AsFloat;
            pbMesh.CreateShapeFromPolygon(points.ToArray(), extrudeDistance, flipNormals: false);

            // 머티리얼 적용
            if (polygonMaterial != null)
            {
                pbMesh.GetComponent<MeshRenderer>().sharedMaterial = polygonMaterial;
            }
            else
            {
                Debug.LogWarning("Polygon material is not set in the Inspector.");
            }

#if UNITY_EDITOR
            // 폴리곤 프리팹으로 저장
            string prefabPath = "Assets/Prefab/" + polyShapeName + ".prefab";
            PrefabUtility.SaveAsPrefabAsset(polyShapeObject, prefabPath);
#endif
        }
    }
}
