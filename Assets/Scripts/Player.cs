using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    public Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log($"player Collide with {other}");
        if (other.gameObject.CompareTag("Goal"))
        {
            Debug.Log("Win");
            gameManager.SetScreen();
        }
    }
}
