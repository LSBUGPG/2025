using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Tiles : MonoBehaviour
{
    [SerializeField] private Sprite tileSprite;
    [SerializeField] private Color[] colors;

    private void CreateTileSprite(int n, float x, float y)
    {
        GameObject tile = new GameObject($"Tile{n}x{n}");
        tile.transform.SetParent(transform, false);
        tile.transform.localPosition = new Vector3(x, y, 0);
        SpriteRenderer spriteRenderer = tile.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = tileSprite;
        spriteRenderer.color = colors[n - 1];
        tile.transform.localScale = new Vector3(n, n, 1);
        tile.AddComponent<BoxCollider2D>();
        tile.AddComponent<Tile>();
    }

    void Start()
    {
        Application.targetFrameRate = 60;
        int tiles = colors.Length;
        float x = tiles / 2f;
        float y = 22 + tiles / 2f;
        float ny = y - tiles - (2 * tiles - 1) / 2f - 1;
        for (int n = tiles; n >= 1; n--)
        {
            float dx = x;
            float dy = y;
            for (int i = 0; i < n; i++)
            {
                CreateTileSprite(n, dx, dy);
                dx += 1;
                dy -= 1;
            }
            x += n + (2 * n - 1) / 2f + 1;
            if (x > tiles * tiles / 2f)
            {
                x = (n - 1) / 2f;
                y = ny - (n - 1) / 2f;
                ny = y - n - (2 * n - 1) / 2f - 1;
            }
        }
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0));
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
            if (hit.collider)
            {
                Tile tile = hit.collider.GetComponent<Tile>();
                if (tile)
                {
                    tile.StartDrag(worldPosition);
                }
            }
        }
    }
}
