using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    [SerializeField] private List<Line> lineList;
    private GameManager gameManager;
    [SerializeField] private Animator catAnimator;

    private void Awake()
    {
        // get GameManager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        // get Lines
        foreach(Transform child in transform)
        {
            foreach (Transform lineObject in child)
            {
                lineList.Add(lineObject.GetComponent<Line>());
            }
        }
    }

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
        catAnimator.Play("leave_squish");
    }
}
