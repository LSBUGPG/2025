using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private float lineWidth = 0.1f;
    [SerializeField] private Material lineMaterial;
    private int width = 45;
    private int height = 45;

    private LineRenderer CreateLineRenderer(string name)
    {
        GameObject line = new GameObject(name);
        line.transform.SetParent(transform, false); 
        LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.useWorldSpace = false;
        return lineRenderer;
    }

    private void CreateHorizontalLine(int y)
    {
        LineRenderer lineRenderer = CreateLineRenderer($"Y{y}");
        float left = 0 - width / 2;
        float right = width - width / 2;
        lineRenderer.SetPositions(new Vector3[] { new Vector3(left, y, 0), new Vector3(right, y, 0) });
    }

    private void CreateVerticalLine(int x)
    {
        LineRenderer lineRenderer = CreateLineRenderer($"X{x}");
        float bottom = 0 - height / 2;
        float top = height - height / 2;
        lineRenderer.SetPositions(new Vector3[] { new Vector3(x, top, 0), new Vector3(x, bottom, 0) });
    }

    void Start()
    {
        for (int y = 0; y <= height; y++)
        {
            CreateHorizontalLine(y - height / 2);
        }

        for (int x = 0; x <= width; x++)
        {
            CreateVerticalLine(x - width / 2);
        }
    }
}
