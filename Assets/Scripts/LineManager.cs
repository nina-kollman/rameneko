using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    [SerializeField] private List<Line> lineList;
    [SerializeField] private GameManager gameManager;
    
    public void AddClick()
    {
        gameManager.AddClick();
    }

    public void ChangeGravityDirection(Transform transform, bool isVertical)
    {
        Direction jumpDirection = gameManager.GetJumpDirection(transform, isVertical);
        gameManager.ChangeGravityDirection(jumpDirection);
    }

    
    public void ChangeOtherLines(Vector2 top, Vector2 bottom, EraseDirection eraseDirection, bool hover)
    {
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
    }
    
}
