using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine. SceneManagement;

public class ButtonManager : MonoBehaviour
{

    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject selectLevelScreen;
    [SerializeField] private GameObject levelButtons;

    private AudioManager audioManager;

    private void Start()
    {
        
        AudioManager.Instance.Play("backGroundSound");
    }

    public void StartButton()
    {
        Debug.Log("hiiiii");
       startScreen.SetActive(false);
       selectLevelScreen.SetActive(true);
       this.gameObject.SetActive(false);
       levelButtons.SetActive(true);
    }

    public void SetLevelButton(int levelNum)
    {
        SceneManager.LoadScene(levelNum);
    }
    
    
}
