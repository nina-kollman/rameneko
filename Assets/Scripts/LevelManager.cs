using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine. SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private GameObject levelButtons;
    private GameObject lvlStars;
    [SerializeField] private int[] levelBuildIndex;
    private GameObject arrows;
    private int firstLevelIndex = 4;
    private int numOfLevelSelectorScreens = 3;
    [SerializeField] private Animator leftDoor;
    [SerializeField] private Animator rightDoor;

    
    void Start()
    {
        SetDoorAnimation();
        Debug.Log($"{levelBuildIndex.Length}, Start {SceneManager.GetActiveScene().buildIndex}");
        arrows = GameObject.Find("arrows");
        levelButtons = GameObject.Find("notes");
        lvlStars = GameObject.Find("LevelStars");
        Debug.Log(lvlStars);
        Debug.Log($"{arrows.name}, and {levelButtons.name}");
        SetAllLevelButtons();
        SetArrowPosition();
       
    }

    /*
     * Sets the correct view of the level selector cards:
     * 1. If the level is completed the number will be white
     * 2. If the level is completed the number of stars collected will be presented
     */
    private void SetAllLevelButtons()
    {

        for (int i = 0; i < 5; i++)
        {
            string key = "level_" + levelBuildIndex[i]; 
            // level completeness view
            switch (PlayerPrefs.GetInt(key))
            {
                // First level setting
                case 0:
                    PlayerPrefs.SetInt(key, -1); 
                    break;
                // The level is completed
                case 1:
                case 2:
                case 3:
                    levelButtons.transform.GetChild(i).GetComponent<TextMeshProUGUI>().color =
                        new Color(1f, 1f, 1f, 1f); 
                    break;
            }

            key = "star_" + levelBuildIndex[i];
            GameObject star = lvlStars.transform.GetChild(i).gameObject;
            // star collection view
            switch (PlayerPrefs.GetInt(key))
            {
                case 0:
                    star.SetActive(false);
                    break;
                case 1:
                    ActivateStars(star);
                    star.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    break;
                case 2:
                    ActivateStars(star);
                    star.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    star.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    break;
                case 3:
                    ActivateStars(star);
                    star.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    star.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    star.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    break;
                
            }
            
        }
    }

    /*
     * Activates the LevelStar GameObject
     */
    private void ActivateStars(GameObject star)
    {
        star.SetActive(true);
        for (int j = 0; j < 3; j++)
        {
            star.transform.GetChild(j).gameObject.SetActive(true);
        }
    }

    /*
     * Sets the level indication arrow in the correct position:
     * 1. The arrow will point on the first un completed level in the screen
     * 2. If all the level are completed the arrow will not be presented
     */
    private void SetArrowPosition()
    {
        if (levelBuildIndex[0] == firstLevelIndex && PlayerPrefs.GetInt(("level_" + levelBuildIndex[0])) == -1)
        {
            arrows.transform.GetChild(0).gameObject.SetActive(true);
            return;
        }
        
        for (int i = 0; i < 5; i++)
        {
            string key = "level_" + levelBuildIndex[i];
            if (PlayerPrefs.GetInt(key) == -1)
            {
                string prevKey = "level_" + (levelBuildIndex[i] -1);
                if (PlayerPrefs.GetInt(prevKey) != -1)
                {
                    arrows.transform.GetChild(i).gameObject.SetActive(true);
                    return;
                }
            }
        }
    }

    /*
     * The function is called by the level buttons. The desired level will be loaded if the level was already
     * completed or the level before it was completed, else nothing will happen when clicking on the button
     */
    public void LoadLevel(int levelIndex)
    {
        if (levelIndex == 0)
        {
            if (firstLevelIndex == levelBuildIndex[levelIndex])
            {
                SceneManager.LoadScene(levelBuildIndex[levelIndex]);
                return;
            }
        }
        
        // Else the level before must be completed
        int keyIndex = levelIndex == 0 ? (levelBuildIndex[0] - 1) : levelBuildIndex[levelIndex - 1];
        string key = "level_" + keyIndex;
       // Debug.Log($"{key}, get int: {PlayerPrefs.GetInt(key)}");
        if (PlayerPrefs.GetInt(key) != -1)
        {
            SceneManager.LoadScene(levelBuildIndex[levelIndex]);
        }

    }

    private void SetDoorAnimation()
    {
        Debug.Log("Transition");
        leftDoor.SetTrigger("out");
        rightDoor.SetTrigger("out");
    }
        

   
}
