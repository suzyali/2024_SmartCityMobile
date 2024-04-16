using System.IO;
using UnityEngine;
using SimpleJSON; // SimpleJSON 라이브러리 사용


public class GeoJsonToTerrain : MonoBehaviour
{
    public string filePath = "Data/성남시 분당구_matching.geojson"; // 파일 경로 설정
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
                // Feature의 Properties 정보
                var properties = feature["properties"];
                float left =  properties["left"].AsFloat;
                float right =  properties["right"].AsFloat ;
                float top =  properties["top"].AsFloat ;
                float bottom =  properties["bottom"].AsFloat ;


                string id = properties["id"];

                // Terrain 생성
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
        // TerrainData 객체 생성
        TerrainData terrainData = new TerrainData();
        terrainData.heightmapResolution = 513; // 기본값, 필요에 따라 조정
        terrainData.size = new Vector3(1000, 600, 1000); // 가로, 높이, 세로 크기 설정

        // Terrain 객체 생성
        GameObject terrainObject = Terrain.CreateTerrainGameObject(terrainData);
        terrainObject.name = $"Terrain_{id}"; // Terrain 명 설정
        terrainObject.transform.position = CalculateTerrainPosition(left,top,right,bottom); // 위치 설정
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
