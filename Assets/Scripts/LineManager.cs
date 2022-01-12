using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    [SerializeField] private List<Line> lineList;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Animator catAnimator;
    
    public void AddClick()
    {
        gameManager.AddClick();
    }

    

    public void ChangeGravityDirection(Transform transform, bool isVertical)
    {
        Direction jumpDirection = gameManager.GetJumpDirection(transform, isVertical);
        gameManager.ChangeGravityDirection(jumpDirection);
    }

    
    public void ChangeOtherLines(Vector2 top, Vector2 bottom, EraseDirection eraseDirection, bool hover, bool isVertical)
    {
        if (hover)
        {
            catAnimator.Play("squish");
            Debug.Log("squish");
            Direction dir = gameManager.GetJumpDirection(transform, isVertical);
        }
        else
            catAnimator.Play("leave_squish");
        
        
        foreach (Line line in lineList)
        {
            // go through each line - and check for needed changes
            // change logic - in LinePart
            line.ChangePartsOnOtherClick(top, bottom, eraseDirection, hover);
        }
    }

    /**
     * Resets all the marked lines
     */
    public void MarkLines()
    {
        foreach (Line line in lineList)
        {
            line.MarkLines(true);
        }
        Debug.Log("LLSS");
        catAnimator.Play("leave_squish");
    }
    
}
