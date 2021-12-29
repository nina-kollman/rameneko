using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform mPlayer;
    [SerializeField] private float mMoveSpeed = 1;


    void Update()
    {
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
        if (other.tag == "Goal")
        {
            Debug.Log("Win");
            Time.timeScale = 0;
        }
    }
}
