using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine. SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private AudioManager audioManager;
    private int firstLevelIndex = 4;
    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime = 1f;
    [SerializeField] private GameObject screenCanvas;

    private Tween fadeTween;
    
    private void Start()
    {
        
        AudioManager.Instance.Play("backGroundSound");
    }
    
    public void NextLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);  
    }
    
    public void NextLevelSelectorButton()
    {
       StartCoroutine(LoadScreen(SceneManager.GetActiveScene().buildIndex + 1));
       
    }

    IEnumerator LoadScreen(int index)
    {
        
        Debug.Log("Transition");

        transition.SetTrigger("start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(index);

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
        LevelSelectorScreen(SceneManager.GetActiveScene().buildIndex);
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
    
    private void LevelSelectorScreen(int buildIndex)
    {
        int index = buildIndex - firstLevelIndex;
        int screenNum = buildIndex / 5;
        Debug.Log($"screen number {screenNum}");
        SceneManager.LoadScene(screenNum);
    }
}
