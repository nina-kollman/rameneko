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
    private GameObject lvlLocks;
    [SerializeField] private int[] levelBuildIndex;
    private GameObject arrows;
    private int firstLevelIndex = 4;
    private int numOfLevelSelectorScreens = 3;
    

    
    void Start()
    {
        Debug.Log($"{levelBuildIndex.Length}, Start {SceneManager.GetActiveScene().buildIndex}");
        arrows = GameObject.Find("arrows");
        levelButtons = GameObject.Find("notes");
        lvlStars = GameObject.Find("LevelStars");
        lvlLocks = GameObject.Find("locks");
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
    public void SetAllLevelButtons()
    {
        
        for (int i = 0; i < 5; i++)
        {
            string key = "star_" + levelBuildIndex[i];
            GameObject star = lvlStars.transform.GetChild(i).gameObject;
            // star collection view
            switch (PlayerPrefs.GetInt(key))
            {
                case 0:
                    star.SetActive(false);
                    lvlLocks.transform.GetChild(i).gameObject.SetActive(true);
                    break;
                case 1:
                    levelButtons.transform.GetChild(i).GetComponent<TextMeshProUGUI>().color = Color.white;
                    ActivateStars(star);
                    star.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    break;
                case 2:
                    levelButtons.transform.GetChild(i).GetComponent<TextMeshProUGUI>().color = Color.white;
                    ActivateStars(star);
                    star.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    star.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    break;
                case 3:
                    levelButtons.transform.GetChild(i).GetComponent<TextMeshProUGUI>().color = Color.white;
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
    public void SetArrowPosition()
    {
        if (levelBuildIndex[0] == firstLevelIndex && PlayerPrefs.GetInt(("star_" + levelBuildIndex[0])) == 0)
        {
            lvlLocks.transform.GetChild(0).gameObject.SetActive(false);
            arrows.transform.GetChild(0).gameObject.SetActive(true);
            return;
        }
        
        for (int i = 0; i < 5; i++)
        {
            string key = "star_" + levelBuildIndex[i];
            if (PlayerPrefs.GetInt(key) == 0)
            {
                string prevKey = "star_" + (levelBuildIndex[i] -1);
                if (PlayerPrefs.GetInt(prevKey) != 0)
                {
                    lvlLocks.transform.GetChild(i).gameObject.SetActive(false);
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
        string key = "star_" + keyIndex;
       // Debug.Log($"{key}, get int: {PlayerPrefs.GetInt(key)}");
        if (PlayerPrefs.GetInt(key) != 0)
        {
            SceneManager.LoadScene(levelBuildIndex[levelIndex]);
        }

    }
    
    public void RestartGameButton()
    { 
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);
    }
   
}
