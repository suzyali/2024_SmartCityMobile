using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;
using SimpleJSON; // SimpleJSON ���̺귯�� ���
public class PolygonCreator : MonoBehaviour
{
    public string filePath = "Data/������ �д籸_matching.geojson"; // ���� ��� ����
    void Start()
    {
        LoadGeoJsonAndCreateBuilding();
       // �������� ��θ� ������ ��ǥ��
       Vector3[] points = new Vector3[]
        {
            new Vector3(73.638676120637683f, 0,51.077821673243307f),
            new Vector3(74.19974471689784f,0,46.551866629975848f),
            new Vector3(69.237801476905588f,0, 45.454795277793892f),
            new Vector3(70.283869493490783f,0,40.727849651128054f),
            new Vector3(64.823933693027357f,0, 39.412771998555399f),
            new Vector3(64.562936779664597f,0, 39.348768296069466f),
            new Vector3(61.609800265257945f,0, 49.252642083563842f),
            new Vector3(73.549665368889691f,0, 51.787814603187144f)
        };

        // PolyShape ������Ʈ�� ���� �� ���� ������Ʈ ����
        GameObject polyShapeObject = new GameObject("PolyShape");
        PolyShape polyShape = polyShapeObject.AddComponent<PolyShape>();
        ProBuilderMesh pbMesh = polyShapeObject.AddComponent<ProBuilderMesh>();

        // ������ ��� ����
        polyShape.SetControlPoints(points);

        // ������ ����� �����ϰ�, extrude(�о��)�� �����Ͽ� 3D ���·� ��ȯ
        float extrudeDistance = 2.0f; // �о�� �Ÿ�
        bool flipNormals = false; // ��� ���� ������ ����

        // ProBuilder API�� ����Ͽ� ������ ��� ����
        pbMesh.CreateShapeFromPolygon(points, extrudeDistance, flipNormals);

    }
    void LoadGeoJsonAndCreateBuilding()
    {
        
    }
}
