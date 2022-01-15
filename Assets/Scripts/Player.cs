using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private Animator myAnimator;
    private int lastLevelBuildIndex = 9; 


    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log($"player Collide with {other}");
        if (other.gameObject.CompareTag("Goal"))
        {
            Debug.Log("Win");
            AudioManager.Instance.Play("winLevelSound");
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            string key = "level_" + (sceneIndex - 1).ToString();
            Debug.Log(key);
            PlayerPrefs.SetInt(key,1);
            if (sceneIndex == lastLevelBuildIndex)
                gameManager.SetScene(lastLevelBuildIndex + 1);
            else
                gameManager.SetNextLevelScreen();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Goal")
        {
            AudioManager.Instance.Play("winLevelSound");
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            string key = "level_" + (sceneIndex - 1).ToString();
            Debug.Log(key);
            PlayerPrefs.SetInt(key,1);
            if (sceneIndex == lastLevelBuildIndex)
                gameManager.SetScene(lastLevelBuildIndex + 1);
            else
                gameManager.SetNextLevelScreen();
        }
    }

    public void ChangeMovementConstraints(bool isVertical)
    {
        GetComponent<Rigidbody2D>().constraints = isVertical ?  RigidbodyConstraints2D.FreezePositionX : RigidbodyConstraints2D.FreezePositionY;
    }
}
