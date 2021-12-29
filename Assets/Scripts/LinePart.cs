using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePart : MonoBehaviour
{
    [SerializeField] private Line lineParent;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    [SerializeField] private Transform top;
    [SerializeField] private Transform bottom;
    
    private bool isVertical;

    private void Awake()
    {
        isVertical = lineParent.GetVertical();
    }

    private void OnMouseDown()
    {
        lineParent.ClickOnPart();
    }

    public void ActivateCommandPart(bool activateCommand)
    {
        if (activateCommand)
        {
            // activate part
            spriteRenderer.color = Color.white;
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else
        {
            // deactivate part
            spriteRenderer.color = Color.grey;
            GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    public void DeActivateByPosition(Transform clickedTop, Transform clickedBottom)
    {
        if (isVertical)
        {
            
        }
        else
        {
             
        }
    }

    public Transform GetTop()
    {
        return top;
    }
    
    public Transform GetBottom()
    {
        return bottom;
    }
}
