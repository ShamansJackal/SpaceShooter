using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableSkins : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public List<Sprite> Sprites;
    void Start()
    {
        spriteRenderer.sprite = Sprites[Random.Range(0, Sprites.Count - 1)];
    }
}
