using UnityEngine;
using UnityEditor;

public class CustomTerrainGenerator : EditorWindow
{
    Vector3 startPoint = Vector3.zero; // 시작 지점
    Vector3 endPoint = Vector3.zero; // 끝 지점
    Vector3 terrainSize = new Vector3(500, 50, 500); // 사용자가 지정하는 터레인 크기

    [MenuItem("Tools/Custom Terrain Generator")]
    public static void ShowWindow()
    {
        GetWindow<CustomTerrainGenerator>("Custom Terrain Generator");
    }

    void OnGUI()
    {
        GUILayout.Label("Set the terrain bounds and size", EditorStyles.boldLabel);

        startPoint = EditorGUILayout.Vector3Field("Start Point", startPoint);
        endPoint = EditorGUILayout.Vector3Field("End Point", endPoint);
        terrainSize = EditorGUILayout.Vector3Field("Terrain Size", terrainSize);

        if (GUILayout.Button("Generate Terrain"))
        {
            GenerateTerrains(startPoint, endPoint, terrainSize);
        }
    }

    void GenerateTerrains(Vector3 start, Vector3 end, Vector3 size)
    {
        Vector3 currentStart = start;
        float terrainWidth = size.x;
        float terrainLength = size.z;

        // 계산된 영역의 너비와 길이
        float areaWidth = Mathf.Abs(end.x - start.x);
        float areaLength = Mathf.Abs(end.z - start.z);

        int terrainCountX = Mathf.CeilToInt(areaWidth / terrainWidth);
        int terrainCountZ = Mathf.CeilToInt(areaLength / terrainLength);

        for (int x = 0; x < terrainCountX; x++)
        {
            for (int z = 0; z < terrainCountZ; z++)
            {
                // 마지막 터레인의 크기 조정
                float currentTerrainWidth = (x == terrainCountX - 1) ? (areaWidth % terrainWidth) : terrainWidth;
                float currentTerrainLength = (z == terrainCountZ - 1) ? (areaLength % terrainLength) : terrainLength;

                // 0보다 작은 크기를 가지는 경우 기본 크기 사용
                currentTerrainWidth = currentTerrainWidth <= 0 ? terrainWidth : currentTerrainWidth;
                currentTerrainLength = currentTerrainLength <= 0 ? terrainLength : currentTerrainLength;

                Vector3 currentPosition = new Vector3(currentStart.x + (terrainWidth * x), 0, currentStart.z + (terrainLength * z));
                CreateTerrain(currentPosition, new Vector3(currentTerrainWidth, size.y, currentTerrainLength));
            }
        }
    }

    void CreateTerrain(Vector3 position, Vector3 size)
    {
        // 터레인 데이터 설정
        TerrainData terrainData = new TerrainData();
        terrainData.heightmapResolution = 513;
        terrainData.size = size;
        terrainData.baseMapResolution = 1024;

        // 높이 맵 초기화
        float[,] heights = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];
        for (int x = 0; x < terrainData.heightmapResolution; x++)
        {
            for (int y = 0; y < terrainData.heightmapResolution; y++)
            {
                heights[x, y] = 0.5f; // 평평한 지형을 생성
            }
        }

        terrainData.SetHeights(0, 0, heights);

        // 터레인 게임 오브젝트 생성 및 배치
        Terrain terrain = Terrain.CreateTerrainGameObject(terrainData).GetComponent<Terrain>();
        terrain.terrainData = terrainData;
        terrain.transform.position = position;
        terrain.gameObject.name = "GeneratedTerrain";
    }
}
