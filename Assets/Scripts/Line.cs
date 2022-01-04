using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private LineManager lineMng;
    [SerializeField] private List<LinePart> lineParts;
    [SerializeField] private bool isVertical;
    [SerializeField] private EraseDirection eraseDirection;
    [SerializeField] private GameObject leftMarkSquare;
    [SerializeField] private GameObject rightMarkSquare;


    private Vector2 top;
    private Vector2 bottom;

    private void Awake()
    {
        // init full line top and bottom by his parts
        top = lineParts[lineParts.Count - 1].GetTop();
        bottom = lineParts[0].GetBottom();
    }

    /**
     * create the effect of clicking on line
     */
    public void ClickOnPart(Transform linePartTransform, bool hover)
    {
        // 1. add one more click to click count
        if (!hover)
        {
            lineMng.AddClick();

            // 2. activate all of the line
            foreach (LinePart part in lineParts)
            {
                part.ActivateCommandPart(true);
            }
        }

        // 3. destroy all the other lines (that needed to be destroyed by nina's new rule)
        lineMng.ChangeOtherLines(top, bottom, eraseDirection, hover);
        if (hover)
            MarkSquares(true);
        
        // 4. change gravity direction
        if(!hover)
            lineMng.ChangeGravityDirection(linePartTransform, isVertical);
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

    public bool GetVertical()
    {
        return isVertical;
    }

    
    /**
     * toMark:true - Marks all the line that will be deleted upon selecting the line.
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
        leftMarkSquare.SetActive(active);
        rightMarkSquare.SetActive(active);
    }
}
