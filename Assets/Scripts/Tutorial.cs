using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private int tutorialLevel;
    [SerializeField] private List<GameObject> tutorialLineList;

    [SerializeField] private GameObject firstHighlightLine;
    [SerializeField] private GameObject secondHighlightLine;

    [SerializeField] private GameObject firstUIPost;
    [SerializeField] private GameObject secondUIPost;

    private GameObject lastClickedPart;
    public int tutorialClicksCounter;

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
        PlayTutorialAnimations();
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
                UpdateClickCount(true);
            }
            // if we clicked on the wonted line - for the first time
            else if (tutorialLine)
            {
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
        if (tutorialLevel == 1)
        {
            if (tutorialClicksCounter < 1)
            {
                firstHighlightLine.SetActive(true);
                firstUIPost.SetActive(true);
                secondUIPost.SetActive(false);
            }
            else if (tutorialClicksCounter < 2)
            {
                firstHighlightLine.SetActive(false);
                secondUIPost.SetActive(true);
            }
            else
            {
                firstHighlightLine.SetActive(false);
                firstUIPost.SetActive(false);
                secondUIPost.SetActive(false);
            }
        }
        else if (tutorialLevel == 2)
        {
            if (tutorialClicksCounter < 1)
            {
                firstHighlightLine.SetActive(true);
                firstUIPost.SetActive(true);
                secondHighlightLine.SetActive(false);
                secondUIPost.SetActive(false);
            }
            else if (tutorialClicksCounter < 2)
            {
                firstHighlightLine.SetActive(true);
                firstUIPost.SetActive(false);
                secondHighlightLine.SetActive(false);
                secondUIPost.SetActive(true);
            }
            else if (tutorialClicksCounter < 4)
            {
                firstHighlightLine.SetActive(false);
                firstHighlightLine.SetActive(false);
                secondHighlightLine.SetActive(true);
                secondUIPost.SetActive(true);
            }
            else
            {
                firstHighlightLine.SetActive(false);
                firstHighlightLine.SetActive(false);
                secondHighlightLine.SetActive(false);
                secondUIPost.SetActive(false);
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
            tutorialClicksCounter += 1;
        }
        else
        {
            if (tutorialLevel == 2 && tutorialClicksCounter >= 2)
            {
                tutorialClicksCounter = Math.Max(2, tutorialClicksCounter - 1);
            }
            else
            {
                tutorialClicksCounter = Math.Max(0, tutorialClicksCounter - 1);
            }
        }
    }
}
