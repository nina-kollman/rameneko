using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine. SceneManagement;
using Object = System.Object;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LineManager lineMng;
    [SerializeField] private Player player;
    [SerializeField] private int levelNum;
    [SerializeField] private int maxClicksInLevel;
    private int clickCounter;

    private void Awake()
    {
        //Gravity Down
        Physics2D.gravity = new Vector2(0, -9.81f);
    }

    private void Start()
    {
        clickCounter = 0;
    }

    private void Update()
    {
        PlayTestKeyPress();
    }
    
    public void AddClick()
    {
        clickCounter++;
        if (clickCounter > maxClicksInLevel)
        {
            // Lose game / raise warning
        }
    }

    public void ChangeGravityDirection(Transform transform, bool isVertical)
    {
        if (isVertical)
        {
            if (transform.position.x > player.transform.position.x)
            {
                // gravity to the right
                Physics2D.gravity = new Vector2(9.81f, 0);
            }
            else
            {
                // gravity to the left
                Physics2D.gravity = new Vector2(-9.81f, 0);

            }
        }
        else
        {
            if (transform.position.y > player.transform.position.y)
            {
                // gravity up
                Physics2D.gravity = new Vector2(0, 9.81f);

            }
            else
            {
                // gravity down
                Physics2D.gravity = new Vector2(0, -9.81f);
            }
        }
    }

    /**
     * Change the level by number keys - for a quick play-test feel
     */
    private void PlayTestKeyPress()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Physics2D.gravity = new Vector2(0, -9.81f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SceneManager.LoadScene(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SceneManager.LoadScene(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SceneManager.LoadScene(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SceneManager.LoadScene(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SceneManager.LoadScene(8);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SceneManager.LoadScene(9);
        }
        
    }
    
    // for (int i = 0; i < 10; ++i)
        // {
        //     if (Input.GetKeyDown(i.ToString()))
        //     {
        //         Debug.Log(i);
        //         // Physics2D.gravity = new Vector2(0, -9.81f);
        //         // SceneManager.LoadScene(i);
        //     }
        // }
    
    
}
