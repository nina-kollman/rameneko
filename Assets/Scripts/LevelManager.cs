using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine. SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject levelButtons;
    [SerializeField] private int[] levelBuildIndex;
    [SerializeField] private GameObject arrows;
    private int firstLevelIndex = 4;
    private int numOfLevelSelectorScreens = 3;
    
    
    
    void Start()
    {
        Debug.Log($"{levelBuildIndex.Length}, Start {SceneManager.GetActiveScene().buildIndex}");
       // Debug.Log("LevelManagerStart");
        SetAllLevelButtons();
        SetArrowPosition();
    }

    private void SetAllLevelButtons()
    {

        for (int i = 0; i < 5; i++)
        {
            string key = "level_" + levelBuildIndex[i]; // The index is the build index of the level
           // Debug.Log($"{key}, value {PlayerPrefs.GetInt(key)}");
            switch (PlayerPrefs.GetInt(key))
            {
                // First level setting
                case 0:
                    PlayerPrefs.SetInt(key, -1); 
                    break;
                // The level is completed
                case 1:
                    levelButtons.transform.GetChild(i).GetComponent<TextMeshProUGUI>().color =
                        new Color(1f, 1f, 1f, 1f); 
                    break;
            }
        }
    }

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
                if (PlayerPrefs.GetInt(prevKey) == 1)
                {
                    arrows.transform.GetChild(i).gameObject.SetActive(true);
                    return;
                }
            }
        }
    }

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
        if (PlayerPrefs.GetInt(key) == 1)
        {
            SceneManager.LoadScene(levelBuildIndex[levelIndex]);
        }

    }

   
}
