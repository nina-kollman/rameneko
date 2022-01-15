using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine. SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private AudioManager audioManager;

    private void Start()
    {
        
        AudioManager.Instance.Play("backGroundSound");
    }

    public void StartButton()
    {
        Debug.Log("hiiiii");
        SceneManager.LoadScene(1); // loading LevelSelector scene
    }

    public void SetLevelButton(int levelNum)
    {
        if (PlayerPrefs.GetInt(("level_" + (levelNum - 1).ToString())) == 1 || levelNum == 1)
        {
            SceneManager.LoadScene(levelNum+1);  
        }
    }

    public void NextLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);  
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LevelSelectorScreen()
    {
        SceneManager.LoadScene(1);
    }

    public void EndGameButton()
    {
        Application.Quit();
    }


}
