using UnityEngine;
using SimpleJSON;
using System.Collections.Generic;

public class TerrainBuild : MonoBehaviour
{
    public TextAsset jsonFile;
    public Terrain terrainPrefab; // 참조하지만, 실제로는 TerrainData를 새로 생성합니다.
    private List<float> idList = new List<float>();
    private Vector3 initialPosition;
    private bool isInitialSet = false;

    void Start()
    {
        if (jsonFile == null)
        {
            Debug.LogError("GeoJSON file is not assigned!");
            return;
        }

        string jsonString = jsonFile.text;
        JSONNode jsonData = JSON.Parse(jsonString);
        JSONArray features = jsonData["features"].AsArray;

        foreach (JSONNode feature in features)
        {
            JSONNode properties = feature["properties"];
            float left, top, right, bottom, id;

            if (!float.TryParse(properties["left"], out left) ||
                !float.TryParse(properties["top"], out top) ||
                !float.TryParse(properties["right"], out right) ||
                !float.TryParse(properties["bottom"], out bottom) ||
                !float.TryParse(properties["id"], out id))
            {
                Debug.LogError("Failed to parse terrain properties!");
                continue;
            }

            idList.Add(id);

            CreateTerrain(left, top, right, bottom, id);
        }

        Debug.Log("ID List:");
        foreach (int id in idList)
        {
            Debug.Log(id);
        }
    }

    Vector3 CalculateTerrainPosition(double left, double top, double right, double bottom)
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
    Terrain CreateTerrain(double left, double top, double right, double bottom, float id)
    {
        Vector3 position = CalculateTerrainPosition(left, top, right, bottom);

        // TerrainData를 새로 생성하고 각 Terrain에 적용합니다.
        TerrainData terrainData = new TerrainData
        {

            size = new Vector3(1000, 0, 1000) // 각 Terrain의 크기를 설정합니다.
        };

        // Terrain 객체를 생성합니다.
        GameObject terrainObject = Terrain.CreateTerrainGameObject(terrainData);
        terrainObject.transform.position = position;
        terrainObject.name = "Terrain_ID_" + id;

        Terrain terrain = terrainObject.GetComponent<Terrain>();
        terrain.terrainData = terrainData;
        return terrain;
    }
}