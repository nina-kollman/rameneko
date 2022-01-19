using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine. SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private AudioManager audioManager;
    [SerializeField] private GameObject[] levelSelectorButtons;
    [SerializeField] private GameObject backGround;
    [SerializeField] private GameObject screen2;
    private GameObject currenButtons;
    private int buttonIndex;
    [SerializeField] private Vector3 shakeStrength;
    
    private void Start()
    {
        buttonIndex = 0;
        currenButtons = levelSelectorButtons[buttonIndex];
        AudioManager.Instance.Play("backGroundSound");
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

    public void StartSceneButton()
    {
        SceneManager.LoadScene(0);
    }

    public void EndGameButton()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void NextPageButton()
    {
        Debug.Log("NextPage");
        backGround.transform.DOMove(new Vector3(50f, 0, 0), 5f);
        screen2.SetActive(true);
        screen2.transform.DOMove(new Vector3(0, -540, 0),5f);
       // backGround.transform.DOShakePosition(3f, shakeStrength);
        currenButtons.transform.DOShakeRotation(2f, shakeStrength);
        currenButtons.SetActive(false);
        currenButtons = levelSelectorButtons[++buttonIndex];
        currenButtons.SetActive(true);
        // screenIndex++;
        // currentPage = levelSelectorScreens[screenIndex];
    }
}
