using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeThrough : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> sprites;
    [SerializeField] private float lerpSpeed = 3f;

    private Color color;
    private Color targetColor;

    private void Awake()
    {
        targetColor = sprites[0].color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            targetColor.a = 0.5f;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            targetColor.a = 1f;
    }

    private void Update()
    {
        color = Color.Lerp(color, targetColor, lerpSpeed * Time.deltaTime);

        foreach (var sprite in sprites)
        {
            sprite.color = color;
        }
    }
}
