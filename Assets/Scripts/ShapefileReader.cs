using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class ShapefileReader : MonoBehaviour
{
    public string filePath; // Inspector에서 설정, 예: "Assets/Data/myShapefile.shp"

    void Start()
    {
        LoadShapefile(filePath);
    }

    void LoadShapefile(string path)
    {
        using (FileStream fs = new FileStream(path, FileMode.Open)) 
        {
            using (BinaryReader br = new BinaryReader(fs))
            {
                // SHP 파일 헤더를 건너뛰기 위해 필요한 코드
                br.BaseStream.Seek(100, SeekOrigin.Begin);

                while (br.BaseStream.Position < br.BaseStream.Length)
                {
                    // 각 지형 데이터 읽기 (이 예에서는 간단한 구현으로, 실제 데이터 구조에 맞게 조정 필요)
                    // 예: 지형의 X, Y 좌표를 읽는 로직
                    float x = br.ReadSingle() - 202214.9937f;
                    float y = br.ReadSingle() - 531996.3721f;

                    // 읽은 데이터를 바탕으로 Unity에서 객체 생성
                    CreateObjectInUnity(x, y);
                }
            }
        }
    }

    void CreateObjectInUnity(float x, float y)
    {
        // Unity에서 객체를 생성하는 예시 코드
        GameObject newObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        newObj.transform.position = new Vector3(x, y, 0);
    }
}
