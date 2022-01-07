using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform mPlayer;
    [SerializeField] private float mMoveSpeed = 1;
    [SerializeField] private GameManager gameManager;


    void Update()
    {
        // TODO: stop the player movement if it's low
        
        // TODO: do we want to clear the arrows? well, yes.
        if (Input.GetKey(KeyCode.UpArrow))
        {
            mPlayer.Translate(Vector2.up * mMoveSpeed * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.DownArrow))
        {
            mPlayer.Translate(Vector2.down * mMoveSpeed * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            mPlayer.Translate(Vector2.right * mMoveSpeed * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            mPlayer.Translate(Vector2.left * mMoveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"player Triger with {other.tag}");
        if (other.tag == "Goal")
        {
            Debug.Log("Win");
            gameManager.SetScreen();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log($"player Collide with {other}");
        if (other.gameObject.tag == "Goal")
        {
            Debug.Log("Win");
            gameManager.SetScreen();
        }
    }
}
