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

    public void SetLevelButton(int levelNum)
    {
        if (PlayerPrefs.GetInt(("level_" + (levelNum - 1).ToString())) == 1 || levelNum == 1)
        {
            SceneManager.LoadScene(levelNum+2);  
        }
    }

    public void NextLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);  
    }

    public void PreviousLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);  
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LevelSelectorScreen()
    {
        SceneManager.LoadScene(1);
    }

    public void StartSceneButton()
    {
        SceneManager.LoadScene(0);
    }

    public void EndGameButton()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
