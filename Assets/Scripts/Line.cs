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

    private Vector2 top;
    private Vector2 bottom;

    private void Awake()
    {
        // init full line top and bottom by his parts
        top = lineParts[-1].GetTop();
        bottom = lineParts[0].GetBottom();
    }

    /**
     * create the effect of clicking on line
     */
    public void ClickOnPart()
    {
        // 1. add one more click to click count
        lineMng.AddClick();
        // 2. activate all of the line
        foreach (LinePart part in lineParts)
        {
            part.ActivateCommandPart(true); 
        }
        // 3. destroy all the other lines (that needed to be destroyed by nina's new rule)
        lineMng.ChangeOtherLines(top, bottom, eraseDirection);
        // 4. change gravity direction
        lineMng.ChangeGravityDirection(transform, isVertical);
    }

    /**
     * going through his parts, change each part according to the given coordinates
     */
    public void ChangePartsOnOtherClick(Vector2 clickedTop, Vector2 clickedBottom, EraseDirection eraseDirection)
    {
        foreach (LinePart part in lineParts)
        {
            part.PartChangeByOtherClick(clickedTop, clickedBottom, eraseDirection);
        }
    }

    public bool GetVertical()
    {
        return isVertical;
    }
}
