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
    
    void Start()
    {
        Debug.Log("LevelManagerStart");
        SetAllLevelButtons();
        SetArrowPosition();
    }

    private void SetAllLevelButtons()
    {

        for (int i = 0; i < 5; i++)
        {
            string key = "level_" + levelBuildIndex[i]; // The index is the build index of the level
            Debug.Log($"{key}, value {PlayerPrefs.GetInt(key)}");
            switch (PlayerPrefs.GetInt(key))
            {
                // First level setting
                case 0:
                    PlayerPrefs.SetInt(key, -1); 
                    break;
                // The level is completed
                case 1:
                    levelButtons.transform.GetChild(i).GetComponent<TextMeshPro>().color =
                        new Color(1f, 1f, 1f, 1f); 
                    break;
            }
        }
    }

    private void SetArrowPosition()
    {
        for (int i = 0; i < 5; i++)
        {
            string key = "level_" + levelBuildIndex[i];
            if (PlayerPrefs.GetInt(key) == -1)
            {
                arrows.transform.GetChild(i).gameObject.SetActive(true);
                return;
            }
        }
    }

    public void LoadLevel(int levelIndex)
    {
        if (levelIndex == 0)
        {
            SceneManager.LoadScene(levelBuildIndex[levelIndex]);
        }
        else
        {
            string key = "level_" + levelBuildIndex[levelIndex-1];
            if (PlayerPrefs.GetInt(key) == 1)
            {
                SceneManager.LoadScene(levelBuildIndex[levelIndex]);
            }
        }
    }
}
