using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private bool win = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Goal") && !win)
        {
            win = true;
            Debug.Log("Win");
            transform.GetChild(0).GetComponent<Animator>().Play("win");
            //other.gameObject.transform.GetChild(1).gameObject.SetActive(true); // star particle system
            other.gameObject.GetComponentInChildren<Animator>().Play("wingoal");
            gameManager.SetStarScreen(other.gameObject.transform.GetChild(1).gameObject); //Set StarScreen 
        }
        else if (other.CompareTag("BoardSide"))
        {
            Debug.Log("Lose");
            gameManager.SetStarScreen(null, true);
        }
    }

    public void ChangeMovementConstraints(bool isVertical)
    {
        GetComponent<Rigidbody2D>().constraints = isVertical ?  RigidbodyConstraints2D.FreezePositionX : RigidbodyConstraints2D.FreezePositionY;
    }

    
}
