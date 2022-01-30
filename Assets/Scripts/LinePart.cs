using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class LinePart : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    
    [SerializeField] private Vector2 top;
    [SerializeField] private Vector2 bottom;
    
    private Line lineParent;
    private Animator myAnimator;
    private Collider2D myCollider;
    private Sprite lastSprite;
    private bool linePartMarked;
    private Stopwatch stopWatch;
    private ParticleSystem poof;
    
    private void Awake()
    {
        lineParent = transform.GetComponentInParent<Line>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // this line changed due to the prefab change
        myCollider = transform.parent.parent.GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        linePartMarked = false;
    }

    private void Start()
    {
        poof = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
    }
    
   
    public void ClickOnPart()
    {
        // clicked on unbreakable line
        if (lineParent.unClickable)
        {
            if (lineParent.isVertical)
            {
                myAnimator.Play("unClickable-V");
            }
            else
            {
                myAnimator.Play("unClickable-H");
            }
        }
        else
        {
            lineParent.ClickOnLine(transform);
        }
    }

    /**
     * Called when the player clicked on another point on the board or second time on the part before activating.
     * Rests all the lines that were marked when clicked on the line for the first time. 
     */
    public void UnClickPart(bool isClickedNeedToTurnOff)
    {
        // get the lines out of firstClick mode
        lineParent.UnBlinkParts();
        // update parent isClicked
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
           if (lineParent.isVertical)
                myAnimator.Play("V-DtoE");
            else 
                myAnimator.Play("H-DtoE");
            
            AudioManager.Instance.Play("bambooOn");
            spriteRenderer.sortingOrder = 2;
            myCollider.isTrigger = false;
        }
        else
        {
            if (myCollider.isTrigger != true)
                poof.Play();
            // deactivate part
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
        if (toDelete)
        {
            linePartMarked = true;
            if (!myCollider.isTrigger) // line part is white and active
            {
                if (lineParent.isVertical)
                    myAnimator.Play("shake-V");
                else
                    myAnimator.Play("shake-H");
            }
        }
        else if (linePartMarked)
        {
            linePartMarked = false;
            if (!myCollider.isTrigger) // Set back the active line to white
            {
                if (lineParent.isVertical)
                    myAnimator.Play("shake-V-stop");
                else
                    myAnimator.Play("shake-H-stop");
            }
        }
        else
        {
            // myAnimator.Play("idle");
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

    public void FirstClickAnimation()
    {
        if (lineParent.isVertical)
        {
            if (!myCollider.isTrigger)
            {
                myAnimator.Play("hover_enable_V");
            }
            else
            {
                myAnimator.Play("hover_disable_V");
            }
        }
        else
        {
            if (!myCollider.isTrigger) // line part is active
            {
                myAnimator.Play("hover_enable_H");
            }
            else
            {
                myAnimator.Play("hover_disable_H");
            }
        }
    }

    public void UnClickFirstClick()
    {
        if (lineParent.isVertical)
        {
            if (!myCollider.isTrigger)
            {
                myAnimator.Play("idle-V-E");
            }
            else
            {
                myAnimator.Play("idle-V-D");
            }
        }
        else
        {
            if (!myCollider.isTrigger) // line part is active
            {
                myAnimator.Play("idle-H-E");
            }
            else
            {
                myAnimator.Play("idle-H-D");
            }
        }
    }
    
    
    public bool IsParentUnClickable()
    {
        return lineParent.unClickable;
    }
}
