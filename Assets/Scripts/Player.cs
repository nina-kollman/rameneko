using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private bool win = false;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Goal") && !win)
        {
            win = true;
            Debug.Log("Win");
            AudioManager.Instance.Play("winLevelSound");
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            string key = "level_" + sceneIndex;
            Debug.Log(key);
            PlayerPrefs.SetInt(key,1);
            // BUG: jumping to the winning scene
            gameManager.SetStarScreen(); // Set StarScreen 
        }
    }

    public void ChangeMovementConstraints(bool isVertical)
    {
        GetComponent<Rigidbody2D>().constraints = isVertical ?  RigidbodyConstraints2D.FreezePositionX : RigidbodyConstraints2D.FreezePositionY;
    }

    
}
