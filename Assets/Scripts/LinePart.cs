using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePart : MonoBehaviour
{
    private Line lineParent;
    private SpriteRenderer spriteRenderer;
    
    [SerializeField] private Vector2 top;
    [SerializeField] private Vector2 bottom;
    
    private bool isVertical;

    private void Awake()
    {
        isVertical = lineParent.GetVertical();
        lineParent = GetComponentInParent<Line>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
    
    public Vector2 GetTop()
    {
        return top;
    }
    
    public Vector2 GetBottom()
    {
        return bottom;
    }

    /**
     * When clicking on another line, activate this function for each line part on the board.
     * Check if this parts is need to deactivate, if so - do that.
     */
    public void PartChangeByOtherClick(Vector2 clickedTop, Vector2 clickedBottom, EraseDirection eraseDirection)
    {
        // booleans for grid position check
        bool isInsideYPosition = clickedTop.y >= top.y && bottom.y >= clickedBottom.y;
        bool isInsideXPosition = clickedTop.x >= top.x && bottom.x >= clickedBottom.x;
        bool isClickedXisBigger = clickedTop.x >= top.x;
        bool isClickedYisBigger = clickedTop.y >= top.y;
        
        // checking the requirments for the line - by the booleans
        bool checkXMinus = eraseDirection == EraseDirection.Xminus && isInsideYPosition && isClickedXisBigger;
        bool checkXPlus = eraseDirection == EraseDirection.Xplus && isInsideYPosition && !isClickedXisBigger;
        bool checkYMinus = eraseDirection == EraseDirection.Yminus && isInsideXPosition && isClickedYisBigger;
        bool checkYPlus = eraseDirection == EraseDirection.Yplus && isInsideXPosition && !isClickedYisBigger;
        
        // make sure that the original line is not destroyed
        bool sameAsClickedLine = (isInsideYPosition && isInsideXPosition);

        if (!sameAsClickedLine && (checkXMinus || checkXPlus || checkYMinus || checkYPlus))
        {
            ActivateCommandPart(false);
        }
    }
}
