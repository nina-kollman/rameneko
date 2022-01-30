using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine. SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private AudioManager audioManager;
    private int firstLevelIndex = 4;
    private Animator transition;
    private float transitionTime = 0.2f;
    private LevelManager lvlManager;

    private Tween fadeTween;
    
    private void Start()
    {
        AudioManager.Instance.Stop("opening");
        AudioManager.Instance.Play("gameTrack");
        transition = GameObject.Find("FadeImage").GetComponent<Animator>();
    }
    
    /**
     * Sets the next scene
     */
    public void NextLevelButton()
    {
        StartCoroutine(LoadScreen(SceneManager.GetActiveScene().buildIndex+1));
    }

    /**
     * Sets the previous scene
     */
    public void PrevLevelSelectorButton()
    {
        StartCoroutine(LoadScreen(SceneManager.GetActiveScene().buildIndex - 1));
       
    }

    /**
     * Triggers the Load scene animation and sets the next scene after it
     */
    IEnumerator LoadScreen(int index)
    {
        
        Debug.Log("Transition");

        transition.SetTrigger("start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(index);

    }

    /**
     * Restarts the current scene
     */
    public void RestartButton()
    {
        StartCoroutine(LoadScreen(SceneManager.GetActiveScene().buildIndex));
    }

    /**
     * Home button on each level - loads the correct level selector screen  
     */
    public void LevelSelectorScreen()
    {
        ChooseLevelSelectorScreen(SceneManager.GetActiveScene().buildIndex);
    }
    

    /**
     * Stops the game 
     */
    public void EndGameButton()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
    
    /**
     * Sets the correct level selector screen according to the game level it was directed from.
     * The level selector screen will show the last level that was played
     */
    private void ChooseLevelSelectorScreen(int buildIndex)
    {
        int screenNum = (buildIndex - firstLevelIndex) / 5;
        Debug.Log($"screen number {screenNum}, build index: {buildIndex}");
        SceneManager.LoadScene(screenNum + 1);
    }
    
}
