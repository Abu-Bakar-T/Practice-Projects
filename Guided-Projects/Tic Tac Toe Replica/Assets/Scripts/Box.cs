using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public int index;
    public Mark mark;
    public bool isMarked;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        index = transform.GetSiblingIndex();
        mark = Mark.None;
        isMarked = false;
    }

    public void SetAsMarked(Sprite sprite, Mark mark, Color color)
    {
        isMarked = true;
        this.mark = mark;
        spriteRenderer.sprite = sprite;
        color.a = 1.0f;
        spriteRenderer.color = color;

        // Disable Collider to avoid marking twice
        GetComponent<CircleCollider2D>().enabled = false;

    }
}
