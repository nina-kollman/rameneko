using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private List<LinePart> lineParts;
    [SerializeField] public bool isVertical;
    [SerializeField] private EraseDirection eraseDirection;
    [SerializeField] private GameObject leftMarkSquare;
    [SerializeField] private GameObject rightMarkSquare;
    public bool unClickable;
    
    private LineManager lineMng;
    private Vector2 top;
    private Vector2 bottom;
    public bool isClicked;

    private void Awake()
    {
        // init full line top and bottom by his parts
        top = lineParts.Last().GetTop();
        bottom = lineParts.First().GetBottom();
        isClicked = false;
        lineMng = GetComponentInParent<LineManager>();
    }

    /**
     * create the effect of clicking on line
     */
    public void ClickOnLine(Transform partTransform)
    {
        // 1. add one more click to click count
        if (isClicked)
        {
            lineMng.AddClick();

            // 2. activate all of the line
            foreach (LinePart part in lineParts)
            {
                part.ActivateCommandPart(true);
            }
        }
        // first click
        else 
        {
            foreach (LinePart part in lineParts)
            {
                part.FirstClickAnimation();
            }
        }

        // 3. destroy all the other lines (that needed to be destroyed by nina's new rule)
        lineMng.ChangeOtherLines(top, bottom, eraseDirection, !isClicked, isVertical);
        if (!isClicked)
            MarkSquares(true);
        
        // 4. change gravity direction
        if(isClicked)
            lineMng.ChangeGravityDirection(partTransform, isVertical);
        // 5. change isClicked
        isClicked = !isClicked;
    }

    /**
     * going through his parts, change each part according to the given coordinates
     */
    public void ChangePartsOnOtherClick(Vector2 clickedTop, Vector2 clickedBottom, EraseDirection eraseDirection, bool hover)
    {
        foreach (LinePart part in lineParts)
        {
            part.PartChangeByOtherClick(clickedTop, clickedBottom, eraseDirection, hover);
        }
    }

    /**
     * toMark:true - Marks all the lines that will be deleted upon selecting the line.
     * toMark:false - Sets the MarkLines function from the line manager 
     */
    public void MarkLines(bool toMark)
    {
        if (toMark)
        {
            foreach (var part in lineParts)
            {
                part.MarkPartToBeDeleted(false);
            }
        }
        else
        {
            lineMng.MarkLines();
        }
    }

    public void MarkSquares(bool active)
    {
        if (leftMarkSquare && rightMarkSquare)
        {
            leftMarkSquare.SetActive(active);
            rightMarkSquare.SetActive(active);
        }
    }

    /**
     * (let's say the line is already clicked - first click)
     * when clicking on another element, decline the blinking animation.
     */
    public void UnBlinkParts()
    {
        foreach (var part in lineParts)
        {
            part.UnClickFirstClick();
        }
    }
    
}
