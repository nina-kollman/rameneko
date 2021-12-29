using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private LineManager lineMng;
    [SerializeField] private bool isVertical;


    public void ClickOnPart()
    {
        // 1. add one more click to click count
        lineMng.AddClick();
        // 2. activate all of the line
        
        // 3. destroy all the other lines (that needed to be destroyed by nina's new rule)
        
        // 4. change gravity direction
        lineMng.ChangeGravityDirection(transform, isVertical);
    }

    public void DeActivateParts(Transform clickedTop, Transform clickedBottom)
    {
        
    }

    public bool GetVertical()
    {
        return isVertical;
    }
}
