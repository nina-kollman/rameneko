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
        myCollider = transform.parent.GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        lineMarked = false;
        lastColor = spriteRenderer.color;
    }

    private void Start()
    {
        poof = this.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
    }

    public void ClickOnPart()
    {
        lineParent.ClickOnLine(transform);
    }

    /**
     * Called when the player clicked on another point on the board.
     * Rests all the lines that were marked when clicked on the line for the first time. 
     */
    public void UnClickPart(bool isClickedNeedToTurnOff)
    {
        lineParent.isClicked = isClickedNeedToTurnOff ? false : lineParent.isClicked;
        lineParent.MarkLines(false);
        lineParent.MarkSquares(false);
    }

    private void SetAnimation(string firstVertical, string secondVertical, string firstHorizontal, string secondHorizontal)
    {
        if (!myCollider.isTrigger) // line part is white and active
        {
            if (lineParent.isVertical)
                myAnimator.Play(firstVertical);
            else
            {
                myAnimator.Play(secondVertical);
            }
        }
        else // not active
        {
            if (lineParent.isVertical)
                myAnimator.Play(firstHorizontal);
            else
            {
                myAnimator.Play(secondHorizontal);
            }
        }
    }

    public void ActivateCommandPart(bool activateCommand)
    {
        if (activateCommand)
        {
            // activate part
            //spriteRenderer.color = whiteColor;
           // Debug.Log(this);
            if (lineParent.isVertical)
                myAnimator.Play("V-DtoE");
            else 
                myAnimator.Play("H-DtoE");
            
            spriteRenderer.sortingOrder = 2;
            myCollider.isTrigger = false;
        }
        else
        {
            if (myCollider.isTrigger != true)
                poof.Play();
            // deactivate part
            //spriteRenderer.color = greyColor;
           // spriteRenderer.sprite = disablesBamboo;
            if (!myCollider.isTrigger)
            {
                if (lineParent.isVertical)
                    myAnimator.Play("V-EtoD");
                else
                    myAnimator.Play("H-EtoD");
                
            }

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

                if(lineParent.isVertical)
                    myAnimator.Play("shake-V");
                else
                    myAnimator.Play("shake-H");
            }
                
        }
        else if (lineMarked)
        {
            lineMarked = false;
            if (!myCollider.isTrigger) // Set back the active line to white
            {
                if(lineParent.isVertical)
                    myAnimator.Play("shake-V-stop");
                else
                    myAnimator.Play("shake-H-stop");
            }
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
