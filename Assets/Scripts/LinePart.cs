using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
        lineParent = GetComponentInParent<Line>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        myCollider = transform.parent.GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        linePartMarked = false;
    }

    private void Start()
    {
        poof = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
    }

    public void ClickOnPart()
    {
        lineParent.ClickOnLine(transform);
    }

    /**
     * Called when the player clicked on another point on the board or second time on the part before activating.
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
           // Debug.Log(this)
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
            // AudioManager.Instance.Play("bambooOff");
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
                Debug.Log($"hover_enable_V {this}");
                Debug.Log($"{spriteRenderer.sprite.name}");
                myAnimator.Play("hover_enable_V");
            }
            else
            {
                Debug.Log($"hover_disable_V {this}");
                myAnimator.Play("hover_disable_V");
            }
        }
        else
        {
            if (!myCollider.isTrigger) // line part is active
            {
                Debug.Log($"hover_enable_H {this}");
                Debug.Log($"{spriteRenderer.sprite.name}");
                myAnimator.Play("hover_enable_H");
            }
            else
            {
                Debug.Log($"hover_disable_H {this}");
                myAnimator.Play("hover_disable_H");
            }
        }
    }
}
