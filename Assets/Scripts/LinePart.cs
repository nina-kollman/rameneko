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
    private Collider2D myCollider;
    private bool mouseOver;
    private Color lastColor;
    private bool lineMarked;
    
    private void Awake()
    {
        lineParent = GetComponentInParent<Line>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<BoxCollider2D>();
        mouseOver = false;
        lineMarked = false;
        lastColor = spriteRenderer.color;
        // TODO: not working...
        // Bounds bounds = GetComponent<BoxCollider2D>().bounds;
        // top = new Vector2(bounds.max.x, bounds.max.y);
        // top = new Vector2(bounds.min.x, bounds.min.y);
    }

    private void OnMouseDown()
    {
        lineParent.ClickOnPart(this.transform, false);
    }

    /**
     * Called when the player hovers on the line. Marks all the line parts that will be deleted upon selecting the line
     */
    private void OnMouseOver()
    {
        Debug.Log(this);
    
        if (!mouseOver)
        {
            mouseOver = true;
            lineParent.ClickOnPart(this.transform, true); 
        }
    }

    /**
     * Called when the player stops hovering on the line.
     * Rests all the lines that were marked when hovered on the line. 
     */
    private void OnMouseExit()
    {
        Debug.Log("in mouse exit");
        mouseOver = false;
        lineParent.MarkLines(false);
    }

    public void ActivateCommandPart(bool activateCommand)
    {
        if (activateCommand)
        {
            // activate part
            spriteRenderer.color = Color.white;
            myCollider.isTrigger = false;
        }
        else
        {
            // deactivate part
            spriteRenderer.color = Color.grey;
            myCollider.isTrigger = true;
        }
    }
    
    /**
     * Marks the line parts that will be deleted when the player selects a line - this will be activated when the player
     * is hovering on the line.
     * Resets the Mark when toDelete=false, the player stopped to hover.
     */
    public void MarkPartToBeDeleted(bool toDelete)
    {
        Color myColor = spriteRenderer.color;
        Debug.Log("in Mark");
        
        if (toDelete)
        {
            lineMarked = true;
            Debug.Log("Mark this");
            lastColor = spriteRenderer.color;
            if(!myCollider.isTrigger) // line part is white and active
                spriteRenderer.color = Color.magenta;
        }
        else if (lineMarked)
        {
            lineMarked = false;
            Debug.Log("Reset Mark");
            if(!myCollider.isTrigger) // Set back the active line to white
                spriteRenderer.color = lastColor;
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
     * When hover is true - the parts will be marked to be deleted.
     */
    public void PartChangeByOtherClick(Vector2 clickedTop, Vector2 clickedBottom, EraseDirection eraseDirection, bool hover)
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
            if (hover)
            {
                Debug.Log("part change by click");
                MarkPartToBeDeleted(true);
            }
            else // On Click 
            {
                ActivateCommandPart(false);
            }
        }
    }
}
