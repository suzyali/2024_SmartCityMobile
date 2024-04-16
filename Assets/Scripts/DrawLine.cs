using UnityEngine;

public class DrawLine : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Transform[] points;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        SetupLine(points);
    }

    void SetupLine(Transform[] points)
    {
        lineRenderer.positionCount = points.Length;
        for (int i = 0; i < points.Length; i++)
        {
            lineRenderer.SetPosition(i, points[i].position);
        }
    }
}