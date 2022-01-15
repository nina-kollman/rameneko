using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine. SceneManagement;
using UnityEngine.UI;
using Object = System.Object;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LineManager lineMng;
    [SerializeField] private TextMeshPro stepsCounterUI;
    [SerializeField] private Player player;
    [SerializeField] private int levelNum;
    [SerializeField] private int maxClicksInLevel;
    [SerializeField] private GameObject nextLevelScreen;
    [SerializeField] private GameObject looseScreen;
    [SerializeField] private float duration;
    
    

    private Vector3 nextLevelPosition = new Vector3(-1, 0, 0);
    private int clickCounter;
    // the saved gameObject is a LinePart (and not Line)
    private GameObject lastClickedLinePart;

    private void Awake()
    {
        // Gravity Down
        Physics2D.gravity = new Vector2(0, -300f);
    }

    private void Start()
    {
        lastClickedLinePart = null;
        clickCounter = 0;
        stepsCounterUI.text = (maxClicksInLevel - clickCounter).ToString();
       // nextLevelScreen.SetActive(false);
       
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
            looseScreen.SetActive(true);
        }
    }

    public void ChangeGravityDirection(Direction direction)
    {
        if (direction == Direction.Up)
        {
            // gravity up
            Physics2D.gravity = new Vector2(0, 300f);
            AudioManager.Instance.Play("upDown");
            player.ChangeMovementConstraints(true);
            player.transform.DORotate(new Vector3(0, 0, 180), 0.25f, RotateMode.Fast);
        }
        else if (direction == Direction.Down)
        {
            // gravity down
            Physics2D.gravity = new Vector2(0, -300f);
            AudioManager.Instance.Play("upDown");
            player.ChangeMovementConstraints(true);
            player.transform.DORotate(new Vector3(0, 0, 0), 0.25f, RotateMode.Fast);

        }
        else if (direction == Direction.Left)
        {
            // gravity to the left
            Physics2D.gravity = new Vector2(-300f, 0);
            AudioManager.Instance.Play("sideMovement");
            player.ChangeMovementConstraints(false);
            player.transform.DORotate(new Vector3(0, 0, 270), 0.25f, RotateMode.Fast);

        }
        else if (direction == Direction.Right)
        {
            // gravity to the right
            Physics2D.gravity = new Vector2(300f, 0);
            AudioManager.Instance.Play("sideMovement");
            player.ChangeMovementConstraints(false);
            player.transform.DORotate(new Vector3(0, 0, 90), 0.25f, RotateMode.Fast);
        }
        else
        {
            throw new Exception("Invalid direction in gravity change");
        }
    }

    public Direction GetJumpDirection(Transform transform, bool isVertical)
    {
        if (isVertical)
        {
            if (transform.position.x > player.transform.position.x)
            {
                return Direction.Right;
            }
            else
            {
                return Direction.Left;
            }
        }
        else
        {
            if (transform.position.y > player.transform.position.y)
            {
                return Direction.Up;
            }
            else
            {
                return Direction.Down;
            }
        }
        throw new Exception("Invalid direction calculation");
    }

    /**
     * when clicking on the screen - analyze the click place and check for on-line-part's click.
     */
    private void ClickOnScreen()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit.collider)
        {
            GameObject linePartObject = hit.collider.transform.GetChild(0).gameObject;
            // if we clicked on the same line as before = double click
            string lastClickedLinePartParentLineName = linePartObject.GetComponentInParent<Line>().transform.name;
            string clickedParentLineName = hit.collider.transform.parent.name;
            if (lastClickedLinePart && lastClickedLinePartParentLineName == clickedParentLineName)
            {
                // after the second time - clear the 'hover' indication
                lastClickedLinePart.GetComponent<LinePart>().UnClickPart(false);
                // click on the line for the second time
                lastClickedLinePart.GetComponent<LinePart>().ClickOnPart();
                lastClickedLinePart = null;
            }
            // if we clicked on another line
            else
            {
                if (lastClickedLinePart)
                {
                    // un-click the previous line
                    lastClickedLinePart.GetComponent<LinePart>().UnClickPart(true);
                }
                if (linePartObject.GetComponent<LinePart>())
                {
                    // save the new line, and then click on it
                    lastClickedLinePart = linePartObject;
                    lastClickedLinePart.GetComponent<LinePart>().ClickOnPart();
                }
            }
        }
        // if we clicked on another part of the screen
        else
        {
            if (lastClickedLinePart)
            {
                // un-click the previous line
                lastClickedLinePart.GetComponent<LinePart>().UnClickPart(true);
            }
            // clear the previous line
            lastClickedLinePart = null;
        }
    }

    public void ResetLevel()
    {
        Physics2D.gravity = new Vector2(0, -300f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadHome()
    {
        Physics2D.gravity = new Vector2(0, -300f);
        SceneManager.LoadScene(0);
    }

    /**
     * Change the level by number keys - for a quick play-test feel
     */
    private void PlayTestKeyPress()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetLevel();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene(1);
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
        nextLevelScreen.transform.DOMove(nextLevelPosition, duration).SetEase(Ease.InOutFlash);
    }
}