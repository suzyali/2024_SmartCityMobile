using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;

public class PolygonCreator_Probuilder : MonoBehaviour
{
    void Start()
    {
        // 폴리곤의 경로를 형성할 좌표들
        Vector3[] points = new Vector3[]
        {
            new Vector3(51.088081465917639f, 0, 90.181115332059562f),
            new Vector3(76.792763164761709f, 0, 97.368471601046622f),
            new Vector3(79.534896027937066f, 0, 87.761592045193538f),
            new Vector3(53.73921627731761f, 0, 80.49723492260091f)
            // 마지막 좌표 제거: 폴리곤을 닫기 위해 시작점을 다시 추가할 필요가 없습니다.
        };

        // 새 게임 오브젝트 생성 및 ProBuilderMesh 컴포넌트 추가
        GameObject polyShapeObject = new GameObject("PolyShape");
        ProBuilderMesh pbMesh = polyShapeObject.AddComponent<ProBuilderMesh>();

        // 폴리곤 모양을 생성하고, extrude(밀어내기)를 수행하여 3D 형태로 변환
        //float extrudeDistance = 2.0f; // 밀어내기 거리

        float extrudeDistance = 30f; 
        bool flipNormals = false; // 노멀 방향 뒤집기 여부

        // ProBuilder API를 사용하여 폴리곤 모양 생성
        pbMesh.CreateShapeFromPolygon(points, extrudeDistance, flipNormals);
    }
}
