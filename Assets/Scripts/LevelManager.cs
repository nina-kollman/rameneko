using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int numOfLevels;
    [SerializeField] private GameObject levelButtons;
    void Start()
    {
        Debug.Log("LevelManagerStart");
        SetAllLevelButtons();
        levelButtons.transform.GetChild(0).GetComponent<Image>().color = new Color(0.1f, 0.3f, 0.2f, 1f);
    }

    private void SetAllLevelButtons()
    {

        for (int i = 1; i <= numOfLevels; i++)
        {
            string key = "level_" + i;
            Debug.Log($"{key}, value {PlayerPrefs.GetInt(key)}");
            switch (PlayerPrefs.GetInt(key))
            {
                case 0:
                    PlayerPrefs.SetInt(key, -1);
                    break;
                // The level is completed
                case 1:
                    levelButtons.transform.GetChild(i - 1).GetComponent<Image>().color =
                        new Color(0.1f, 0.3f, 0.2f, 1f);
                    continue;
                // The level is not completed
                case -1:
                    // if the level before is completed then allow to select it -> color green
                    if (PlayerPrefs.GetInt("level_" + (i - 1)) == 1)
                    {
                        levelButtons.transform.GetChild(i - 1).GetComponent<Image>().color =
                            new Color(0.1f, 0.3f, 0.2f, 1f);
                    }
                    break;
            }
        }
    }
}
