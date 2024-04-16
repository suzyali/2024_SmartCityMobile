using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;

public class PolygonCreator_Probuilder : MonoBehaviour
{
    void Start()
    {
        // �������� ��θ� ������ ��ǥ��
        Vector3[] points = new Vector3[]
        {
            new Vector3(51.088081465917639f, 0, 90.181115332059562f),
            new Vector3(76.792763164761709f, 0, 97.368471601046622f),
            new Vector3(79.534896027937066f, 0, 87.761592045193538f),
            new Vector3(53.73921627731761f, 0, 80.49723492260091f)
            // ������ ��ǥ ����: �������� �ݱ� ���� �������� �ٽ� �߰��� �ʿ䰡 �����ϴ�.
        };

        // �� ���� ������Ʈ ���� �� ProBuilderMesh ������Ʈ �߰�
        GameObject polyShapeObject = new GameObject("PolyShape");
        ProBuilderMesh pbMesh = polyShapeObject.AddComponent<ProBuilderMesh>();

        // ������ ����� �����ϰ�, extrude(�о��)�� �����Ͽ� 3D ���·� ��ȯ
        //float extrudeDistance = 2.0f; // �о�� �Ÿ�

        float extrudeDistance = 30f; 
        bool flipNormals = false; // ��� ���� ������ ����

        // ProBuilder API�� ����Ͽ� ������ ��� ����
        pbMesh.CreateShapeFromPolygon(points, extrudeDistance, flipNormals);
    }
}
