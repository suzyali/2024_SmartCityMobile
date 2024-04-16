using System.IO;
using UnityEngine;
using SimpleJSON; // SimpleJSON ���̺귯�� ���


public class GeoJsonToTerrain : MonoBehaviour
{
    public string filePath = "Data/������ �д籸_matching.geojson"; // ���� ��� ����
    private bool isInitialSet = false;
    private Vector3 initialPosition;
    private void Start()
    {
        LoadGeoJsonAndCreateTerrain();
    }

    void LoadGeoJsonAndCreateTerrain()
    {
        string fullPath = Path.Combine(Application.dataPath, filePath);

        if (File.Exists(fullPath))
        {
            string dataAsJson = File.ReadAllText(fullPath);
            var N = JSON.Parse(dataAsJson);
            var firstFeatureProperties = N["features"][0]["properties"];
            Debug.Log(firstFeatureProperties);
            float firstleft =firstFeatureProperties["left"].AsFloat;
            float firsbottom =firstFeatureProperties["bottom"].AsFloat;
            foreach (JSONNode feature in N["features"].AsArray)
            {
                // Feature�� Properties ����
                var properties = feature["properties"];
                float left =  properties["left"].AsFloat;
                float right =  properties["right"].AsFloat ;
                float top =  properties["top"].AsFloat ;
                float bottom =  properties["bottom"].AsFloat ;


                string id = properties["id"];

                // Terrain ����
                CreateTerrain(left,top, right, bottom, id);
            }
        }
        else
        {
            Debug.LogError("Cannot find GeoJSON file!");
        }
    }

    void CreateTerrain(float left, float top, float right, float bottom, string id)
    {
        // TerrainData ��ü ����
        TerrainData terrainData = new TerrainData();
        terrainData.heightmapResolution = 513; // �⺻��, �ʿ信 ���� ����
        terrainData.size = new Vector3(1000, 600, 1000); // ����, ����, ���� ũ�� ����

        // Terrain ��ü ����
        GameObject terrainObject = Terrain.CreateTerrainGameObject(terrainData);
        terrainObject.name = $"Terrain_{id}"; // Terrain �� ����
        terrainObject.transform.position = CalculateTerrainPosition(left,top,right,bottom); // ��ġ ����
    }
    Vector3 CalculateTerrainPosition(float left, float top, float right, float bottom)
    {
        if (!isInitialSet)
        {
            initialPosition = new Vector3((float)(left + right) / 2, 0, (float)(top + bottom) / 2);
            isInitialSet = true;
            return Vector3.zero;
        }

        float xPosition = ((float)(left + right) / 2) - initialPosition.x;
        float zPosition = ((float)(top + bottom) / 2) - initialPosition.z;
        return new Vector3(xPosition, 0, zPosition);
    }
}
