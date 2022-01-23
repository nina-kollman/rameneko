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
    private Animator transition;
    private float transitionTime = 0.3f;
    //[SerializeField] private GameObject screenCanvas;

    private Tween fadeTween;
    
    private void Start()
    {
        
        AudioManager.Instance.Play("backGroundSound");
        transition = GameObject.Find("FadeImage").GetComponent<Animator>();
        Debug.Log($"BManager {transition.name}");
    }
    
    public void NextLevelButton()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);  
        StartCoroutine(LoadScreen(SceneManager.GetActiveScene().buildIndex+1));

    }
    
    public void NextLevelSelectorButton()
    {
       StartCoroutine(LoadScreen(SceneManager.GetActiveScene().buildIndex + 1));
       
    }
    
    public void PrevLevelSelectorButton()
    {
        StartCoroutine(LoadScreen(SceneManager.GetActiveScene().buildIndex - 1));
       
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
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        StartCoroutine(LoadScreen(SceneManager.GetActiveScene().buildIndex));
    }

    public void LevelSelectorScreen()
    {
        ChooseLevelSelectorScreen(SceneManager.GetActiveScene().buildIndex);
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
    
    private void ChooseLevelSelectorScreen(int buildIndex)
    {
        int screenNum = (buildIndex - firstLevelIndex) / 5;
        Debug.Log($"screen number {screenNum}, build index: {buildIndex}");
        SceneManager.LoadScene(screenNum + 1);
    }
}
