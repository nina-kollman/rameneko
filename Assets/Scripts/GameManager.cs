using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = System.Object;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LineManager lineMng;
    [SerializeField] private TextMeshPro stepsCounterUI;
    [SerializeField] private Player player;
    [SerializeField] private int levelNum;
    [SerializeField] private int maxClicksInLevel;
    [SerializeField] private GameObject nextLevelScreen;

    private int clickCounter;
    private GameObject lastClickedLine;

    private void Awake()
    {
        // Gravity Down
        Physics2D.gravity = new Vector2(0, -9.81f);
    }

    private void Start()
    {
        lastClickedLine = null;
        clickCounter = 0;
        stepsCounterUI.text = (maxClicksInLevel - clickCounter).ToString();
        nextLevelScreen.SetActive(false);
    }

    private void Update()
    {
        PlayTestKeyPress();

        if (Input.GetMouseButtonDown(0))
        {
            ClickOnScreen();
        }
    }

    public void AddClick()
    {
        clickCounter++;
        stepsCounterUI.text = (maxClicksInLevel - clickCounter).ToString();
        if (clickCounter > maxClicksInLevel)
        {
            Debug.Log("YOU LOST!");
        }
    }

    public void ChangeGravityDirection(Transform transform, bool isVertical)
    {
        if (isVertical)
        {
            if (transform.position.x > player.transform.position.x)
            {
                // gravity to the right
                Physics2D.gravity = new Vector2(9.81f, 0);
                player.ChangeMovementConstraints(false);
            }
            else
            {
                // gravity to the left
                Physics2D.gravity = new Vector2(-9.81f, 0);
                player.ChangeMovementConstraints(false);
            }
        }
        else
        {
            if (transform.position.y > player.transform.position.y)
            {
                // gravity up
                Physics2D.gravity = new Vector2(0, 9.81f);
                player.ChangeMovementConstraints(true);
            }
            else
            {
                // gravity down
                Physics2D.gravity = new Vector2(0, -9.81f);
                player.ChangeMovementConstraints(true);
            }
        }
    }

    /**
     * when clicking on the screen - analyze the click place and check for on-line-part's click.
     */
    private void ClickOnScreen()
    {
        Debug.Log("Click");
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit.collider)
        {
            // if we clicked on the same line as before = double click
            if (lastClickedLine && lastClickedLine.name == hit.collider.gameObject.name)
            {
                // click on the line for the second time
                lastClickedLine.GetComponent<LinePart>().ClickOnPart();
                // after the second time - clear the 'hover' indication
                lastClickedLine.GetComponent<LinePart>().UnClickPart();
                lastClickedLine = null;
            }
            // if we clicked on another line
            else
            {
                if (lastClickedLine)
                {
                    // un-click the previous line
                    lastClickedLine.GetComponent<LinePart>().UnClickPart();
                }
                if (hit.collider.gameObject.GetComponent<LinePart>())
                {
                    // save the new line, and then click on it
                    lastClickedLine = hit.collider.gameObject;
                    lastClickedLine.GetComponent<LinePart>().ClickOnPart();
                }
            }
        }
        // if we clicked on another part of the screen
        else
        {
            if (lastClickedLine)
            {
                // un-click the previous line
                lastClickedLine.GetComponent<LinePart>().UnClickPart();
            }
            // clear the previous line
            lastClickedLine = null;
        }
    }

    /**
     * Change the level by number keys - for a quick play-test feel
     */
    private void PlayTestKeyPress()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Physics2D.gravity = new Vector2(0, -300f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SceneManager.LoadScene(4);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SceneManager.LoadScene(5);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SceneManager.LoadScene(6);
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SceneManager.LoadScene(7);
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SceneManager.LoadScene(8);
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SceneManager.LoadScene(9);
        }

        if (Input.GetKeyDown(KeyCode.Minus))
        {
            SceneManager.LoadScene(10);
        }

        if (Input.GetKeyDown(KeyCode.Plus))
        {
            SceneManager.LoadScene(11);
        }
    }

    public void SetScreen()
    {
        nextLevelScreen.SetActive(true);
    }
}