using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private int tutorialLevel;
    [SerializeField] private List<GameObject> tutorialLineList;
    [SerializeField] private List<GameObject> tutorialHelpers;

    private GameObject lastClickedPart;
    // number of times we clicked on line - need for line list
    public int clicksOnLine;
    // number of empty clicks - on the whole level
    public int emptyClicks;

    private void Start()
    {
        lastClickedPart = null;
        clicksOnLine = 0;
        PlayTutorialAnimations();
    }

    /**
     * Called by the GameManager, this function handles all of the Tutorial clicks
     * and animation.
     */
    public void TutorialClicksManager()
    {
        // for the tutorial - check if empty click is needed
        if (IsEmptyClickNeeded())
        {
            emptyClicks += 1;
        }
        else
        {
            ClickOnScreenTutorial();
        }
        
        PlayTutorialAnimations();
    }

    /**
     * when clicking on the screen - analyze the click place and check for on-line-part's click.
     */
    private void ClickOnScreenTutorial()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D[] hit = Physics2D.RaycastAll(mousePos2D, Vector2.zero);
        Transform partHit = null;

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.CompareTag("TouchDetect"))
            {
                // get the TouchDetect object
                partHit = hit[i].collider.transform;
                break;
            }
        }
        
        if (partHit)
        {
            GameObject linePartObject = partHit.GetChild(0).gameObject;
            // if we clicked on the wanted line in the tutorial -> double click
            string lastClickedLineName =
                lastClickedPart ? lastClickedPart.GetComponentInParent<Line>().transform.name : null;
            string clickedParentLineName = partHit.parent.parent.name;
            // tutorial - check for the specific wanted card
            bool tutorialLine = tutorialLevel == 4 || tutorialLineList[clicksOnLine / 2].name == clickedParentLineName;
            if (lastClickedPart && lastClickedLineName == clickedParentLineName && tutorialLine)
            {
                // after the second time - clear the 'hover' indication
                lastClickedPart.GetComponent<LinePart>().UnClickPart(false);
                // click on the line for the second time
                lastClickedPart.GetComponent<LinePart>().ClickOnPart();
                lastClickedPart = null;
                // advance tutorial click counter
                UpdateClickCount(true);
            }
            // if we clicked on the wonted line - for the first time
            else if (tutorialLine)
            {
                if (lastClickedPart)
                {
                    // un-click the previous line
                    lastClickedPart.GetComponent<LinePart>().UnClickPart(true);
                }
                // save the new line, and then click on it
                lastClickedPart = linePartObject;
                lastClickedPart.GetComponent<LinePart>().ClickOnPart();
                // advance tutorial click counter
                UpdateClickCount(true);
            }
            // if we clicked on another line
            else
            {
                if (lastClickedPart)
                {
                    // un-click the previous line
                    lastClickedPart.GetComponent<LinePart>().UnClickPart(true);
                    // Roll the Tutorial one step back
                    UpdateClickCount(false);
                }
                lastClickedPart = null;
            }
        }
        // if we clicked on another part of the screen
        else
        {
            if (lastClickedPart)
            {
                // un-click the previous line
                lastClickedPart.GetComponent<LinePart>().UnClickPart(true);
            }
            // clear the previous line
            lastClickedPart = null;
            // Roll the Tutorial one step back
            UpdateClickCount(false);
        }
    }
    
    private void PlayTutorialAnimations()
    {
        int stage = emptyClicks + clicksOnLine;
        if (tutorialLevel == 1)
        {
            if (stage == 0)
            {
                tutorialHelpers[0].SetActive(true); // pre tutorial 1
            }
            else if (stage == 1)
            {
                tutorialHelpers[1].SetActive(true); // pre tutorial 2
                tutorialHelpers[0].SetActive(false); // pre tutorial 1 - off

            }
            else if (stage == 2)
            {
                tutorialHelpers[2].SetActive(true); // pre tutorial 3
                tutorialHelpers[1].SetActive(false); // pre tutorial 2 - off
            }
            else if (stage == 3)
            {
                tutorialHelpers[2].SetActive(false); // pre tutorial - off
                tutorialHelpers[3].SetActive(true); // tap the line
                tutorialHelpers[4].SetActive(true); // arrow and dots
                tutorialHelpers[5].SetActive(false); // tap again - off
            }
            else if (stage == 4)
            {
                tutorialHelpers[3].SetActive(false); // tap the line - off
                tutorialHelpers[5].SetActive(true); // tap again
            }
            else
            {
                tutorialHelpers[4].SetActive(false); // arrow and dots - off
                tutorialHelpers[5].SetActive(false); // tap again - off
            }
        }
        // Tutorial 2 is not a real tutorial
        else if (tutorialLevel == 3)
        {
            if (stage == 0)
            {
                tutorialHelpers[0].SetActive(true); // pre tutorial 1
            }
            else if (stage == 1)
            {
                tutorialHelpers[0].SetActive(false); // pre tutorial 1 - off
                tutorialHelpers[1].SetActive(true); // pre tutorial 2
            }
            else if (stage == 2)
            {
                tutorialHelpers[1].SetActive(false); // pre tutorial 2 - off
                tutorialHelpers[2].SetActive(true); // tap the line
                tutorialHelpers[3].SetActive(true); // arrow and dots 1
                tutorialHelpers[4].SetActive(false); // tap again - off
            }
            else if (stage == 3)
            {
                tutorialHelpers[2].SetActive(false); // tap the line - off
                tutorialHelpers[4].SetActive(true); // tap again
            }
            else if (stage == 4 | stage == 5)
            {
                tutorialHelpers[3].SetActive(false); // arrow and dots 1 - off
                tutorialHelpers[4].SetActive(false); // tap again - off
                tutorialHelpers[5].SetActive(true);
            }
        }
        else if (tutorialLevel == 4)
        {
            if (stage == 0)
            {
                tutorialHelpers[0].SetActive(true); // reach in 2 moves
            }
            else
            {
                tutorialHelpers[0].SetActive(false);
            }
        }
        else
        {
            throw new Exception("Unknown tutorial level");
        }
    }
    
    private void UpdateClickCount(bool increase)
    {
        if (increase)
        {
            clicksOnLine += 1;
        }
        else
        {
            if (tutorialLevel == 4 && emptyClicks > 0)
            {
                clicksOnLine = 1;
            }
            else if (tutorialLevel == 3 && clicksOnLine >= 4)
            {
                clicksOnLine = 4;
            }
            else if (tutorialLevel == 3 && clicksOnLine >= 2)
            {
                clicksOnLine = Math.Max(2, clicksOnLine - 1);
            }
            else if (tutorialLevel == 1 && clicksOnLine >= 2)
            {
                clicksOnLine = 2;
            }
            else
            {
                clicksOnLine = Math.Max(0, clicksOnLine - 1);
            }
        }
    }

    private bool IsEmptyClickNeeded()
    {
        if (tutorialLevel == 1)
        {
            return (clicksOnLine + emptyClicks) == 0  || (clicksOnLine + emptyClicks) == 1 || (clicksOnLine + emptyClicks) == 2;
        }
        else if (tutorialLevel == 3)
        {
            return (clicksOnLine + emptyClicks) == 0  || (clicksOnLine + emptyClicks) == 1;
        }
        else if (tutorialLevel == 4)
        {
            return (clicksOnLine + emptyClicks) == 0;
        }
        else
        {
            throw new Exception("Unknown tutorial level");
        }
    }
}
