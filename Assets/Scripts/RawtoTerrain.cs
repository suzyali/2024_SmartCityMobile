using System.IO;
using UnityEngine;

public class RawToTerrain : MonoBehaviour
{
    public string filePath = "terrain2/122303.raw"; // RAW ���� ���
    public int heightmapResolution = 256; // Terrain ���̸� �ػ�
    public Vector3 terrainSize = new Vector3(1000, 600, 1000); // Terrain ũ�� (����, ����, ����)

    private void Start()
    {
        CreateTerrainFromRaw(filePath);
    }

    void CreateTerrainFromRaw(string relativePath)
    {
        // TerrainData ��ü ����
        TerrainData terrainData = new TerrainData();
        terrainData.heightmapResolution = heightmapResolution + 1; // Unity�� ���������� +1�� �䱸��
        terrainData.size = terrainSize;

        // RAW ���� �ε� �� ���� ������ ����
        string fullPath = Path.Combine(Application.dataPath, relativePath);
        if (!LoadHeightMapFromRaw(fullPath, terrainData))
        {
            Debug.LogError("Heightmap loading failed.");
            return;
        }

        // Terrain ��ü ����
        GameObject terrainObject = Terrain.CreateTerrainGameObject(terrainData);
        terrainObject.name = "GeneratedTerrain";
    }

    bool LoadHeightMapFromRaw(string fullPath, TerrainData terrainData)
    {
        int size = terrainData.heightmapResolution;
        float[,] heights = new float[size, size];

        // RAW ���� �б�
        byte[] rawData = File.ReadAllBytes(fullPath);

        // ���� ũ��� �ػ� ����
        int expectedSize = (size - 1) * (size - 1) * 2; // 16��Ʈ ������ = 2 ����Ʈ, Unity�� ���� �ػ󵵺��� 1 �� ū ���� ���
        if (rawData.Length != expectedSize)
        {
            Debug.LogError($"RAW ������ ũ�Ⱑ ����� �ٸ��ϴ�. ���� ũ��: {expectedSize}, ���� ũ��: {rawData.Length}");
            return false;
        }

        // ����: RAW ������ 16��Ʈ, unsigned, little endian ����
        for (int i = 0, y = 0; y < size - 1; y++)
        {
            for (int x = 0; x < size - 1; x++, i += 2)
            {
                ushort heightValue = System.BitConverter.ToUInt16(rawData, i);
                float normalizedHeight = (float)heightValue / ushort.MaxValue;
                heights[y, x] = normalizedHeight;
            }
        }

        // TerrainData�� ���̸� ����
        terrainData.SetHeights(0, 0, heights);
        return true;
    }
}
