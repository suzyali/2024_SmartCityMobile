using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class ShapefileReader : MonoBehaviour
{
    public string filePath; // Inspector���� ����, ��: "Assets/Data/myShapefile.shp"

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
                // SHP ���� ����� �ǳʶٱ� ���� �ʿ��� �ڵ�
                br.BaseStream.Seek(100, SeekOrigin.Begin);

                while (br.BaseStream.Position < br.BaseStream.Length)
                {
                    // �� ���� ������ �б� (�� �������� ������ ��������, ���� ������ ������ �°� ���� �ʿ�)
                    // ��: ������ X, Y ��ǥ�� �д� ����
                    float x = br.ReadSingle() - 202214.9937f;
                    float y = br.ReadSingle() - 531996.3721f;

                    // ���� �����͸� �������� Unity���� ��ü ����
                    CreateObjectInUnity(x, y);
                }
            }
        }
    }

    void CreateObjectInUnity(float x, float y)
    {
        // Unity���� ��ü�� �����ϴ� ���� �ڵ�
        GameObject newObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        newObj.transform.position = new Vector3(x, y, 0);
    }
}
