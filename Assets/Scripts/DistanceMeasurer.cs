using UnityEngine;
using TMPro;
using UnityEngine.UI; // UI 컴포넌트 사용을 위해 추가

public class DistanceMeasurer : MonoBehaviour
{
    public Camera mainCamera;
    public TMP_Text distanceTextTMP;
    public LineRenderer lineRenderer;
    public Button measureButton; // 측정을 시작하거나 중지하는 버튼

    private Vector3 lastPoint;
    private float totalDistance = 0f;
    private bool isMeasuring = false;
    
    void Start()
    {
        // 버튼 클릭 이벤트에 메소드 연결
        measureButton.onClick.AddListener(ToggleMeasuring);
    }

    void Update()
    {
        if (!isMeasuring) return;

        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭
        {
            Vector3 newPoint = GetWorldPosition();
            if (newPoint != Vector3.zero) // 유효한 지점을 클릭했는지 확인
            {
                if (lineRenderer.positionCount == 0)
                {
                    // 측정 시작
                    lastPoint = newPoint;
                    lineRenderer.positionCount = 1;
                    lineRenderer.SetPosition(0, lastPoint);
                    lineRenderer.startWidth = 1f;
                    lineRenderer.endWidth = 1f;
                }
                else
                {
                    // 측정 계속 및 누적 거리 계산
                    lineRenderer.positionCount += 1;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, newPoint);

                    totalDistance += Vector3.Distance(lastPoint, newPoint);
                    distanceTextTMP.text = $"Total Distance: {totalDistance:0.00}m";

                    lastPoint = newPoint; // 마지막 지점 업데이트
                }
            }
        }

        // ESC 키로 측정 초기화 기능은 제거됨
    }

    Vector3 GetWorldPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    void ToggleMeasuring()
    {
        isMeasuring = !isMeasuring;

        // 측정을 중지하는 경우
        if (!isMeasuring)
        {
            lineRenderer.positionCount = 0;
            totalDistance = 0f;
            distanceTextTMP.text = "Total Distance: 0m";
        }
    }
}
