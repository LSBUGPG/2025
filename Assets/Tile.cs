using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tile : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 offset;
    public void StartDrag(Vector3 world)
    {
        dragging = true;
        offset = transform.position - world;
        Debug.Log($"{name} was clicked at {world}");
    }

    void Update()
    {
        if (dragging)
        {
            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                dragging = false;
                Debug.Log($"{name} was released");
            }
            else if (Mouse.current.delta.magnitude > 0)
            {
                Vector2 mouse = Mouse.current.position.ReadValue();
                Vector3 world = Camera.main.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, 0));
                world += offset;
                // Snap to grid
                float tileSizeOffset = Mathf.Repeat(transform.localScale.x / 2f, 1);
                world.x = Mathf.Round(world.x) + tileSizeOffset;
                world.y = Mathf.Round(world.y) + tileSizeOffset;
                transform.position = new Vector3(world.x, world.y, 0);
            }
        }
    }
}
