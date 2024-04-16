using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class PolygonCreator2 : MonoBehaviour
{
    void Start()
    {
        Vector2[] vertices2D = new Vector2[]
        {
            new Vector2(210073.638676120637683f, 534551.077821673243307f),
            new Vector2(210074.19974471689784f, 534546.551866629975848f),
            new Vector2(210069.237801476905588f, 534545.454795277793892f),
            new Vector2(210070.283869493490783f, 534540.727849651128054f),
            new Vector2(210064.823933693027357f, 534539.412771998555399f),
            new Vector2(210064.562936779664597f, 534539.348768296069466f),
            new Vector2(210061.609800265257945f, 534549.252642083563842f),
            new Vector2(210073.549665368889691f, 534551.787814603187144f)
        };

        CreatePolygon(vertices2D);
    }

    void CreatePolygon(Vector2[] vertices2D)
    {
        // Normalize coordinates and convert to 3D
        float xOffset = 210000f;
        float yOffset = 534000f;
        Vector3[] vertices3D = new Vector3[vertices2D.Length + 1]; // +1 to close the loop
        for (int i = 0; i < vertices2D.Length; i++)
        {
            var normalized = new Vector2(vertices2D[i].x - xOffset, vertices2D[i].y - yOffset);
            vertices3D[i] = new Vector3(normalized.x, 0, normalized.y);
        }
        // Close the loop by repeating the first vertex at the end
        vertices3D[vertices3D.Length - 1] = vertices3D[0];

        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.vertices = vertices3D;

        // Create triangles
        // Note: This naive approach works for convex polygons only
        List<int> triangles = new List<int>();
        for (int i = 0; i < vertices2D.Length - 2; i++)
        {
            triangles.Add(0);
            triangles.Add(i + 1);
            triangles.Add(i + 2);
        }
        // Closing the last triangle
        triangles.Add(0);
        triangles.Add(vertices2D.Length - 1);
        triangles.Add(1);

        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }
}
