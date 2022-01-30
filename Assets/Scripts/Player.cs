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

    /**
     * Defines the player behavior when colliding with a trigger component:
     * 1. Goal - Sets the winning conditions
     * 2. BoardSide - Sets the loosing condition
     */
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

    /**
     * Changes the movement condition according to the isVertical parameter
     */
    public void ChangeMovementConstraints(bool isVertical)
    {
        GetComponent<Rigidbody2D>().constraints = isVertical ?  RigidbodyConstraints2D.FreezePositionX : RigidbodyConstraints2D.FreezePositionY;
    }

    
}
