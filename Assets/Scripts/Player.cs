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
    [SerializeField] private int star0Clicks;
    [SerializeField] private int star1Clicks;
    [SerializeField] private int star2Clicks;
    [SerializeField] private int star3Clicks;
    [SerializeField] private GameObject[] starScreens;


    private void OnCollisionEnter2D(Collision2D other)
    {
       // Debug.Log($"player Collide with {other}");
        if (other.gameObject.CompareTag("Goal") && !win)
        {
            win = true;
            Debug.Log("Win");
            AudioManager.Instance.Play("winLevelSound");
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            string key = "level_" + (sceneIndex - 1);
            Debug.Log(key);
            PlayerPrefs.SetInt(key,1);
            if (sceneIndex == lastLevelBuildIndex)
                gameManager.SetScene(lastLevelBuildIndex + 1); // End Game screen
            else
                SetNextLevelScreen(); //Set StarScreen 
        }
    }

    public void ChangeMovementConstraints(bool isVertical)
    {
        GetComponent<Rigidbody2D>().constraints = isVertical ?  RigidbodyConstraints2D.FreezePositionX : RigidbodyConstraints2D.FreezePositionY;
    }

    /**
     * This function sets the correct NextLevelScreen according to the number of stars the player deserves 
     */
    public void SetNextLevelScreen()
    {
        Debug.Log($"StarScreen, clicks {gameManager.clickCounter.ToString()}");
        if (gameManager.clickCounter <= star3Clicks)
        {
            starScreens[3].SetActive(true);
            gameManager.UpdateStarCounter(3);
        }else if (gameManager.clickCounter <= star2Clicks)
        {
            starScreens[2].SetActive(true);   
            gameManager.UpdateStarCounter(2);
        }else if (gameManager.clickCounter <= star1Clicks)
        {
            starScreens[1].SetActive(true);
            gameManager.UpdateStarCounter(1);
        }
        else // Zero stars
        {
            starScreens[0].SetActive(true);
        }
        
    }
}
