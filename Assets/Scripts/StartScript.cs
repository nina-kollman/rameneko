using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine. SceneManagement;

public class StartScript : MonoBehaviour
{
    [SerializeField] private Animator doorLeft;
    [SerializeField] private Animator doorRight;

    private void Awake()
    {
        PlayerPrefs.DeleteAll();
    }
    
    public void NextLevelButton()
    {
        doorLeft.Play("slide inB");
        doorRight.Play("slide inA");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);  
    }
}
