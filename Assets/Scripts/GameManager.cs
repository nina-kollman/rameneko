using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private lineManager lineMng;
    private int levelNum;

    private void Start()
    {
        levelNum = 1; 
        lineMng.SetLevel(levelNum);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Home))
        {
            lineMng.Restart(levelNum);
        }
    }
    
    
}
