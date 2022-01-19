using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine. SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Object = System.Object;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LineManager lineMng;
    [SerializeField] private TextMeshProUGUI stepsCounterUI;
    [SerializeField] private Player player;
    [SerializeField] private int levelNum;
    [SerializeField] private int maxClicksInLevel;
    [SerializeField] private GameObject nextLevelScreen;
    [SerializeField] private GameObject looseScreen;
    [SerializeField] private float duration;
    
    

    private Vector3 nextLevelPosition = new Vector3(-1, 0, 0);
    public int clickCounter;
    // the saved gameObject is a LinePart (and not Line)
    private GameObject lastClickedPart;
    // saves the tutorial gameObject
    private bool isTutorialActivated;
    private bool win;
    private Tutorial tutorial;
    
    private void Awake()
    {
        // Gravity Down
        Physics2D.gravity = new Vector2(0, -300f);
    }

    private void Start()
    {
        lastClickedPart = null;
        clickCounter = 0;
        stepsCounterUI.text = (maxClicksInLevel - clickCounter).ToString();
        // handle Tutorial clause and script
        isTutorialActivated = GameObject.Find("Tutorial") != null;
        tutorial = isTutorialActivated ? GameObject.Find("Tutorial").GetComponent<Tutorial>() : null;
        win = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !win)
        {
            // check for lose condition
            if (clickCounter - maxClicksInLevel >= 0)
            {
                Debug.Log("YOU LOST!");
                looseScreen.SetActive(true);
            }
            // check for Tutorial click activation
            else if (isTutorialActivated)
            {
                tutorial.TutorialClicksManager();
            }
            // regular click on the screen
            else
            {
                ClickOnScreen();
            }
        }
    }

    public void AddClick()
    {
        clickCounter++;
        stepsCounterUI.text = (maxClicksInLevel - clickCounter).ToString();
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

        RaycastHit2D[] hit = Physics2D.RaycastAll(mousePos2D, Vector2.zero);

        for (int index = 0; index < hit.Length; index++)
        {
            if (hit[index].collider)
            {
                Debug.Log("Found collider");
                /*
                if (hit[index].collider.tag.Equals(("TouchDetect")))
                {
                    GameObject linePartObject = hit.collider.transform.GetChild(0).gameObject;
                    // if we clicked on the same line as before = double click
                    string lastClickedLineName = lastClickedPart ? lastClickedPart.GetComponentInParent<Line>().transform.name: null;
                    string clickedParentLineName = hit.collider.transform.parent.name;
                    if (lastClickedPart && lastClickedLineName == clickedParentLineName)
                    {
                        // after the second time - clear the 'hover' indication
                        lastClickedPart.GetComponent<LinePart>().UnClickPart(false);
                        // click on the line for the second time
                        lastClickedPart.GetComponent<LinePart>().ClickOnPart();
                        lastClickedPart = null;
                    }
                    // if we clicked on another line
                    else
                    {
                        if (lastClickedPart)
                        {
                            // un-click the previous line
                            lastClickedPart.GetComponent<LinePart>().UnClickPart(true);
                        }
                        if (linePartObject.GetComponent<LinePart>())
                        {
                            // save the new line, and then click on it
                            lastClickedPart = linePartObject;
                            lastClickedPart.GetComponent<LinePart>().ClickOnPart();
                        }
                    }
                }
                */
            }
        }

        return;
        // if we clicked on another part of the screen
       // else
        {
            if (lastClickedPart)
            {
                // un-click the previous line
                lastClickedPart.GetComponent<LinePart>().UnClickPart(true);
            }
            // clear the previous line
            lastClickedPart = null;
        }
    }


    public void SetNextLevelScreen()
    {
        win = true;
        nextLevelScreen.SetActive(true);
        nextLevelScreen.transform.DOMove(nextLevelPosition, duration).SetEase(Ease.InOutFlash);
    }

    public void SetScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void UpdateStarCounter(int stars)
    {
        int current = PlayerPrefs.GetInt("starCounter");
        PlayerPrefs.SetInt("StarCounter", current+stars);
    }
}