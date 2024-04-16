using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;
using SimpleJSON; // SimpleJSON 라이브러리 사용
public class PolygonCreator : MonoBehaviour
{
    public string filePath = "Data/성남시 분당구_matching.geojson"; // 파일 경로 설정
    void Start()
    {
        LoadGeoJsonAndCreateBuilding();
       // 폴리곤의 경로를 형성할 좌표들
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

        // PolyShape 컴포넌트를 가진 새 게임 오브젝트 생성
        GameObject polyShapeObject = new GameObject("PolyShape");
        PolyShape polyShape = polyShapeObject.AddComponent<PolyShape>();
        ProBuilderMesh pbMesh = polyShapeObject.AddComponent<ProBuilderMesh>();

        // 폴리곤 경로 설정
        polyShape.SetControlPoints(points);

        // 폴리곤 모양을 생성하고, extrude(밀어내기)를 수행하여 3D 형태로 변환
        float extrudeDistance = 2.0f; // 밀어내기 거리
        bool flipNormals = false; // 노멀 방향 뒤집기 여부

        // ProBuilder API를 사용하여 폴리곤 모양 생성
        pbMesh.CreateShapeFromPolygon(points, extrudeDistance, flipNormals);

    }
    void LoadGeoJsonAndCreateBuilding()
    {
        
    }
}
