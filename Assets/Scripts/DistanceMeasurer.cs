using UnityEngine;
using TMPro;
using UnityEngine.UI; // UI ������Ʈ ����� ���� �߰�

public class DistanceMeasurer : MonoBehaviour
{
    public Camera mainCamera;
    public TMP_Text distanceTextTMP;
    public LineRenderer lineRenderer;
    public Button measureButton; // ������ �����ϰų� �����ϴ� ��ư

    private Vector3 lastPoint;
    private float totalDistance = 0f;
    private bool isMeasuring = false;
    
    void Start()
    {
        // ��ư Ŭ�� �̺�Ʈ�� �޼ҵ� ����
        measureButton.onClick.AddListener(ToggleMeasuring);
    }

    void Update()
    {
        if (!isMeasuring) return;

        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư Ŭ��
        {
            Vector3 newPoint = GetWorldPosition();
            if (newPoint != Vector3.zero) // ��ȿ�� ������ Ŭ���ߴ��� Ȯ��
            {
                if (lineRenderer.positionCount == 0)
                {
                    // ���� ����
                    lastPoint = newPoint;
                    lineRenderer.positionCount = 1;
                    lineRenderer.SetPosition(0, lastPoint);
                    lineRenderer.startWidth = 1f;
                    lineRenderer.endWidth = 1f;
                }
                else
                {
                    // ���� ��� �� ���� �Ÿ� ���
                    lineRenderer.positionCount += 1;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, newPoint);

                    totalDistance += Vector3.Distance(lastPoint, newPoint);
                    distanceTextTMP.text = $"Total Distance: {totalDistance:0.00}m";

                    lastPoint = newPoint; // ������ ���� ������Ʈ
                }
            }
        }

        // ESC Ű�� ���� �ʱ�ȭ ����� ���ŵ�
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

        // ������ �����ϴ� ���
        if (!isMeasuring)
        {
            lineRenderer.positionCount = 0;
            totalDistance = 0f;
            distanceTextTMP.text = "Total Distance: 0m";
        }
    }
}
