using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private int lastLevelBuildIndex = 9;
    private bool win = false;


    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log($"player Collide with {other}");
        if (other.gameObject.CompareTag("Goal") && !win)
        {
            win = true;
            Debug.Log("Win");
            AudioManager.Instance.Play("winLevelSound");
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            string key = "level_" + sceneIndex;
            Debug.Log(key);
            PlayerPrefs.SetInt(key,1);
            if (sceneIndex == lastLevelBuildIndex)
                gameManager.SetScene(lastLevelBuildIndex + 1); // End Game screen
            else
                gameManager.SetStarScreen(); //Set StarScreen 
        }
    }

    public void ChangeMovementConstraints(bool isVertical)
    {
        GetComponent<Rigidbody2D>().constraints = isVertical ?  RigidbodyConstraints2D.FreezePositionX : RigidbodyConstraints2D.FreezePositionY;
    }

    
}
