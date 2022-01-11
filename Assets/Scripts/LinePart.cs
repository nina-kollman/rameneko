using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class LinePart : MonoBehaviour
{
    private Line lineParent;
    private SpriteRenderer spriteRenderer;
    
    [SerializeField] private Vector2 top;
    [SerializeField] private Vector2 bottom;
    [SerializeField] private Sprite disablesBamboo;
    [SerializeField] private Sprite enabledBamboo;

    private Animator myAnimator;
    private Collider2D myCollider;
    private bool mouseOver;
    private Color lastColor;
    private Sprite lastSprite;
    private bool lineMarked;
    private Stopwatch stopWatch;
    private Color whiteColor = new Color(1f, 1f, 1f ,1f);
    private Color greyColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    private ParticleSystem poof;
    
    private void Awake()
    {
        lineParent = GetComponentInParent<Line>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        mouseOver = false;
        lineMarked = false;
        lastColor = spriteRenderer.color;
    }

    private void Start()
    {
        poof = this.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
    }

    private void OnMouseDown()
    {
        lineParent.ClickOnPart(transform, false);
    }

    /**
     * Called when the player hovers on the line. Marks all the line parts that will be deleted upon selecting the line
     */
    private void OnMouseOver()
    {
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
        mouseOver = false;
        lineParent.MarkLines(false);
        lineParent.MarkSquares(false);
    }

    public void ActivateCommandPart(bool activateCommand)
    {
        if (activateCommand)
        {
            // activate part
            //spriteRenderer.color = whiteColor;
            spriteRenderer.sprite = enabledBamboo;

            if (lineParent.isVertical)
                myAnimator.Play("appear-V");
            else 
                myAnimator.Play("appear-H");
            
            spriteRenderer.sortingOrder = 2;
            myCollider.isTrigger = false;
        }
        else
        {
            if (myCollider.isTrigger != true)
                poof.Play();
            // deactivate part
            //spriteRenderer.color = greyColor;
            spriteRenderer.sprite = disablesBamboo;
            // if (lineParent.isVertical)
            //     myAnimator.Play("disappear-V");
            // else 
            //     myAnimator.Play("disappear-H");
            spriteRenderer.sortingOrder = 1;
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
        
        if (toDelete)
        {
            lineMarked = true;
            lastColor = spriteRenderer.color;
            lastSprite = spriteRenderer.sprite;
            if (!myCollider.isTrigger) // line part is white and active
            {
                var opacityColor = spriteRenderer.color;
                Debug.Log("Shake");
                myAnimator.Play("shake");
                //spriteRenderer.color = greyColor;
                // for (int i = 255; i >= 0; i--)
                // {
                //     spriteRenderer.color = new Color(1, 1, 1, i);
                // }
            }
                
            //spriteRenderer.color = Color.magenta;
        }
        else if (lineMarked)
        {
            lineMarked = false;
            if (!myCollider.isTrigger) // Set back the active line to white
            {
                spriteRenderer.sprite = lastSprite;
                spriteRenderer.color = whiteColor;
            }
            // spriteRenderer.color = new Color(1,1,1,255);
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
                MarkPartToBeDeleted(true);
            }
            else // On Click 
            {
                ActivateCommandPart(false);
            }
        }
    }
}
