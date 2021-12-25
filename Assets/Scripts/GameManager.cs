using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LineManager lineMng;
    [SerializeField] private Player player;
    private int levelNum;

    [SerializeField] private Transform FullBoardObject;

    private void Start()
    {
        levelNum = 1; 
        lineMng.SetLevel(levelNum);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Home))
        {
            lineMng.Restart(levelNum);
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
                // FullBoardObject.Rotate(0, 0, 270f);
            }
            else
            {
                // gravity to the left
                Physics2D.gravity = new Vector2(-9.81f, 0);
                // FullBoardObject.Rotate(0, 0, 90f);

            }
        }
        else
        {
            if (transform.position.y > player.transform.position.y)
            {
                // gravity up
                Physics2D.gravity = new Vector2(0, 9.81f);
                // FullBoardObject.Rotate(0, 0, 180f);

            }
            else
            {
                // gravity down
                Physics2D.gravity = new Vector2(0, -9.81f);
                // FullBoardObject.Rotate(0, 0, 0f);

            }
        }
    }
    
}
