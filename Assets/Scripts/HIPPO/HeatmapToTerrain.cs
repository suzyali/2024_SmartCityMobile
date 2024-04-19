using UnityEngine;
using System.IO;

public class HeatmapToTerrain : MonoBehaviour
{
    public Terrain terrain;
    public string heatmapFilePath = "Assets/Data/dem/ID_129669.tif";

    void Start()
    {
        // 히트맵 이미지를 불러옵니다.
        byte[] fileData = File.ReadAllBytes(heatmapFilePath);
        Texture2D heatmap = new Texture2D(2, 2);
        heatmap.LoadImage(fileData);  // 이미지 데이터를 텍스처로 로드합니다.

        // 터레인의 높이맵 크기를 가져옵니다.
        int width = terrain.terrainData.heightmapResolution;
        int height = terrain.terrainData.heightmapResolution;

        // 높이맵을 저장할 2차원 배열을 생성합니다.
        float[,] heights = new float[width, height];

        // 히트맵의 각 픽셀을 순회하면서 높이맵을 설정합니다.
        for (int i = 0; i < heatmap.width; i++)
        {
            for (int j = 0; j < heatmap.height; j++)
            {
                // 픽셀의 밝기를 높이 값으로 사용합니다.
                float heightValue = heatmap.GetPixel(i, j).grayscale;

                // 높이 값을 300미터에서 500미터 사이의 값으로 스케일링합니다.
                heightValue = heightValue * 200 + 300;

                // 높이맵에 높이 값을 설정합니다.
                heights[i, j] = heightValue / terrain.terrainData.size.y;
            }
        }

        // 터레인의 높이맵을 설정합니다.
        terrain.terrainData.SetHeights(0, 0, heights);
    }
}
