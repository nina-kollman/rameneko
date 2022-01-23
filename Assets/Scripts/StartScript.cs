using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine. SceneManagement;

public class StartScript : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.DeleteAll();
    }
    
    public void NextLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);  
    }
}
