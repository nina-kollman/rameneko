using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmerTry : MonoBehaviour
{
    
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(155, 100, 100, 255);
        Debug.Log($"{this} {spriteRenderer.color}");
       // spriteRenderer.color = Color.magenta;
        
    }
    
}
