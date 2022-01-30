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
    [SerializeField] private GameObject stepsCounterObject;
    [SerializeField] private Player player;
    [SerializeField] private GameObject[] starScreens;
    [SerializeField] private int[] starClicks;
    
    private TextMeshProUGUI stepsCounterUI;
    private ParticleSystem clickCounterPoof;
    private Animator stepsCounterAnimator;

    private int clickCounter;
    // the saved gameObject is a LinePart (and not Line)
    private GameObject lastClickedPart;
    // saves the tutorial gameObject
    private bool isTutorialActivated;
    private bool win;
    private Tutorial tutorial;
    
    private void Awake()
    {
        // Set gravity Down
        Physics2D.gravity = new Vector2(0, -300f);
    }

    private void Start()
    {
        stepsCounterUI = stepsCounterObject.GetComponentInChildren<TextMeshProUGUI>();
        clickCounterPoof = stepsCounterObject.GetComponentInChildren<ParticleSystem>();
        stepsCounterAnimator = stepsCounterObject.GetComponent<Animator>();
        lastClickedPart = null;
        clickCounter = 0;
        if (stepsCounterUI)
        {
            stepsCounterUI.text = clickCounter.ToString();
        }
        // handle Tutorial clause and script
        isTutorialActivated = GameObject.Find("Tutorial") != null;
        tutorial = isTutorialActivated ? GameObject.Find("Tutorial").GetComponent<Tutorial>() : null;
        win = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !win)
        {
            // check for Tutorial click activation
            if (isTutorialActivated)
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

    /**
     * Adds a screen click to the click counter, if the UI click element is activates it updates it accordingly
     */
    public void AddClick()
    {
        clickCounter++;
        if (stepsCounterUI)
        {
            stepsCounterUI.text = clickCounter.ToString();
            clickCounterPoof.Play();
            stepsCounterAnimator.Play("move");
        }
    }

    /**
     * Changes the game gravity direction according to the given direction
     */
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

    /**
     * Returns the required jump direction of the player according to the selected bamboo in respect to the current
     * player position
     */
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
            // if we clicked on the same line as before = double click
            string lastClickedLineName = lastClickedPart ? lastClickedPart.GetComponentInParent<Line>().transform.name: null;
            string clickedParentLineName = partHit.parent.parent.name;
            if (lastClickedPart && lastClickedLineName == clickedParentLineName)
            {
                // after the second time - clear the 'hover' indication
                lastClickedPart.GetComponent<LinePart>().UnClickPart(false);
                // click on the line for the second time
                lastClickedPart.GetComponent<LinePart>().ClickOnPart();
                lastClickedPart = null;
            }
            // if we clicked on a line for the first time, or another line
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
                    lastClickedPart = linePartObject.GetComponent<LinePart>().IsParentUnClickable() ? null : linePartObject;
                    linePartObject.GetComponent<LinePart>().ClickOnPart();
                }
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
        }
    }

   /**
    * Updates the star counter according to the stars collected in the game level
    */
   private void UpdateStarCounter(int stars)
    {
        int current = PlayerPrefs.GetInt("starCounter");
        PlayerPrefs.SetInt("StarCounter", current + stars);
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        string key = "star_" + buildIndex;
        stars = Math.Max(PlayerPrefs.GetInt(key, 0), stars);
        PlayerPrefs.SetInt(key, stars);
    }

   /**
     * Sets the correct NextLevelScreen according to the number of stars the player deserves 
     */
   public void SetStarScreen(GameObject starParticle, bool lose = false)
   {
       win = true;

       // In case of win - Turn on star particle system 
       if (clickCounter <= starClicks[0] && !lose)
       { 
           starParticle.SetActive(true); 
           AudioManager.Instance.Play("winLevelSound");
       }

        if (clickCounter <= starClicks[2] && !lose)
        {
            starScreens[3].SetActive(true);
            UpdateStarCounter(3);
        }
        else if (clickCounter <= starClicks[1] && !lose)
        {
            starScreens[2].SetActive(true);
            UpdateStarCounter(2);
        }
        else if (clickCounter <= starClicks[0] && !lose)
        {
            starScreens[1].SetActive(true);
            UpdateStarCounter(1);
        }
        else // Lose - Zero stars
        {
            starScreens[0].SetActive(true);
        }
        
    }
}