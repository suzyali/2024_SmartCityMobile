using UnityEngine;
using System.IO;

public class HeatmapToTerrain : MonoBehaviour
{
    public Terrain terrain;
    public string heatmapFilePath = "Assets/Data/dem/ID_129669.tif";

    void Start()
    {
        // ��Ʈ�� �̹����� �ҷ��ɴϴ�.
        byte[] fileData = File.ReadAllBytes(heatmapFilePath);
        Texture2D heatmap = new Texture2D(2, 2);
        heatmap.LoadImage(fileData);  // �̹��� �����͸� �ؽ�ó�� �ε��մϴ�.

        // �ͷ����� ���̸� ũ�⸦ �����ɴϴ�.
        int width = terrain.terrainData.heightmapResolution;
        int height = terrain.terrainData.heightmapResolution;

        // ���̸��� ������ 2���� �迭�� �����մϴ�.
        float[,] heights = new float[width, height];

        // ��Ʈ���� �� �ȼ��� ��ȸ�ϸ鼭 ���̸��� �����մϴ�.
        for (int i = 0; i < heatmap.width; i++)
        {
            for (int j = 0; j < heatmap.height; j++)
            {
                // �ȼ��� ��⸦ ���� ������ ����մϴ�.
                float heightValue = heatmap.GetPixel(i, j).grayscale;

                // ���� ���� 300���Ϳ��� 500���� ������ ������ �����ϸ��մϴ�.
                heightValue = heightValue * 200 + 300;

                // ���̸ʿ� ���� ���� �����մϴ�.
                heights[i, j] = heightValue / terrain.terrainData.size.y;
            }
        }

        // �ͷ����� ���̸��� �����մϴ�.
        terrain.terrainData.SetHeights(0, 0, heights);
    }
}
