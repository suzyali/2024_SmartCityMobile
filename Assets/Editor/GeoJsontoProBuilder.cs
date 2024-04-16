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
    public string geoJsonFilePath = "Assets/Data/��⵵_������_�д籸_�ǹ�2.geojson";
    public Material polygonMaterial; // Unity Inspector���� ����

    void Start()
    {
        // GeoJSON ���� �ε�
        string geoJsonString = File.ReadAllText(geoJsonFilePath);
        var geoJson = JSON.Parse(geoJsonString);

        // 'features' �迭 ��ȸ
        foreach (JSONNode feature in geoJson["features"].AsArray)
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
            string polyShapeName = feature["properties"]["A9"];
            GameObject polyShapeObject = new GameObject(polyShapeName);
            ProBuilderMesh pbMesh = polyShapeObject.AddComponent<ProBuilderMesh>();

            // ������ ��� ���� �� ����
            float extrudeDistance = feature["properties"]["A17"].AsFloat;
            pbMesh.CreateShapeFromPolygon(points.ToArray(), extrudeDistance, flipNormals: false);

            // ��Ƽ���� ����
            if (polygonMaterial != null)
            {
                pbMesh.GetComponent<MeshRenderer>().sharedMaterial = polygonMaterial;
            }
            else
            {
                Debug.LogWarning("Polygon material is not set in the Inspector.");
            }

#if UNITY_EDITOR
            // ������ ���������� ����
            string prefabPath = "Assets/Prefab/" + polyShapeName + ".prefab";
            PrefabUtility.SaveAsPrefabAsset(polyShapeObject, prefabPath);
#endif
        }
    }
}
