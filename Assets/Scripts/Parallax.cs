using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public bool useGlobalScroll;

    public float scrollSpeed;
    public float tileSize;

    private Vector2 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (useGlobalScroll)
        {
            if (GameManager.Instance?.currentScrollSpeed > 0)
            {
                float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSize);
                transform.position = startPosition + Vector2.left * newPosition;
            }
        }
        else
        {
            float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSize);
            transform.position = startPosition + Vector2.left * newPosition;
        }
    }
}