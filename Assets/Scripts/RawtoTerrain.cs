using System.IO;
using UnityEngine;

public class RawToTerrain : MonoBehaviour
{
    public string filePath = "terrain2/122303.raw"; // RAW 파일 경로
    public int heightmapResolution = 256; // Terrain 높이맵 해상도
    public Vector3 terrainSize = new Vector3(1000, 600, 1000); // Terrain 크기 (가로, 높이, 세로)

    private void Start()
    {
        CreateTerrainFromRaw(filePath);
    }

    void CreateTerrainFromRaw(string relativePath)
    {
        // TerrainData 객체 생성
        TerrainData terrainData = new TerrainData();
        terrainData.heightmapResolution = heightmapResolution + 1; // Unity는 내부적으로 +1을 요구함
        terrainData.size = terrainSize;

        // RAW 파일 로드 및 높이 데이터 설정
        string fullPath = Path.Combine(Application.dataPath, relativePath);
        if (!LoadHeightMapFromRaw(fullPath, terrainData))
        {
            Debug.LogError("Heightmap loading failed.");
            return;
        }

        // Terrain 객체 생성
        GameObject terrainObject = Terrain.CreateTerrainGameObject(terrainData);
        terrainObject.name = "GeneratedTerrain";
    }

    bool LoadHeightMapFromRaw(string fullPath, TerrainData terrainData)
    {
        int size = terrainData.heightmapResolution;
        float[,] heights = new float[size, size];

        // RAW 파일 읽기
        byte[] rawData = File.ReadAllBytes(fullPath);

        // 파일 크기와 해상도 검증
        int expectedSize = (size - 1) * (size - 1) * 2; // 16비트 데이터 = 2 바이트, Unity는 실제 해상도보다 1 더 큰 값을 사용
        if (rawData.Length != expectedSize)
        {
            Debug.LogError($"RAW 파일의 크기가 예상과 다릅니다. 예상 크기: {expectedSize}, 실제 크기: {rawData.Length}");
            return false;
        }

        // 가정: RAW 파일은 16비트, unsigned, little endian 포맷
        for (int i = 0, y = 0; y < size - 1; y++)
        {
            for (int x = 0; x < size - 1; x++, i += 2)
            {
                ushort heightValue = System.BitConverter.ToUInt16(rawData, i);
                float normalizedHeight = (float)heightValue / ushort.MaxValue;
                heights[y, x] = normalizedHeight;
            }
        }

        // TerrainData에 높이맵 설정
        terrainData.SetHeights(0, 0, heights);
        return true;
    }
}
