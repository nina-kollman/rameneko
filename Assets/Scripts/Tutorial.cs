using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private int tutorialLevel;
    [SerializeField] private List<GameObject> tutorialLineList;

    private GameObject lastClickedPart;
    private int tutorialClicksCounter;

    private void Start()
    {
        lastClickedPart = null;
        tutorialClicksCounter = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClickOnScreenTutorial();
        }
    }

    /**
     * when clicking on the screen - analyze the click place and check for on-line-part's click.
     */
    private void ClickOnScreenTutorial()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit.collider)
        {
            GameObject linePartObject = hit.collider.transform.GetChild(0).gameObject;
            // if we clicked on the wanted line in the tutorial -> double click
            string lastClickedLineName =
                lastClickedPart ? lastClickedPart.GetComponentInParent<Line>().transform.name : null;
            string clickedParentLineName = hit.collider.transform.parent.name;
            // tutorial check
            string tutorialWantedLineName = tutorialLineList[tutorialClicksCounter / 2].name;
            bool tutorialLine = tutorialWantedLineName == clickedParentLineName;
            if (lastClickedPart && lastClickedLineName == clickedParentLineName && tutorialLine)
            {
                // after the second time - clear the 'hover' indication
                lastClickedPart.GetComponent<LinePart>().UnClickPart(false);
                // click on the line for the second time
                lastClickedPart.GetComponent<LinePart>().ClickOnPart();
                lastClickedPart = null;
                // advance tutorial click counter
                tutorialClicksCounter += 1;
            }
            // if we clicked on the wonted line - for the first time
            else if (tutorialLine)
            {
                // save the new line, and then click on it
                lastClickedPart = linePartObject;
                lastClickedPart.GetComponent<LinePart>().ClickOnPart();
                // advance tutorial click counter
                tutorialClicksCounter += 1;
            }
            // if we clicked on another line
            else
            {
                if (lastClickedPart)
                {
                    // un-click the previous line
                    lastClickedPart.GetComponent<LinePart>().UnClickPart(true);
                    // Roll the Tutorial one step back
                    tutorialClicksCounter -= 1;
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
            tutorialClicksCounter -= 1;
        }
    }
}
