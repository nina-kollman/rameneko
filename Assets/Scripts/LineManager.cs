using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    [SerializeField] private Line[] verticalLines;
    [SerializeField] private Line[] horizontalLines;
    [SerializeField] private GameManager gameManager;
    
    public void AddClick()
    {
        gameManager.AddClick();
    }

    public void ChangeGravityDirection(Transform transform, bool isVertical)
    {
        gameManager.ChangeGravityDirection(transform, isVertical);
    }

    
    public void ChangeOtherLines(Transform top, Transform bottom, bool isVertical)
    {
        // isVertical => if the original line is vertical
        // change horizontal lines
        if (isVertical)
        {
            
        }
        // change vertical lines
        else
        {
            
        }
    }
}
