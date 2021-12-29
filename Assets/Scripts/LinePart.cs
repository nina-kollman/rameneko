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

    /**
     * When clicking on another line, activate this function for each line part on the board.
     * Check if this parts is need to deactivate, if so - do that.
     */
    public void ChangeByPosition(Transform clickedTop, Transform clickedBottom)
    {
        
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
