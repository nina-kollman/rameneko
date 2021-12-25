using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private SpriteRenderer rend;
    [SerializeField] private bool active;
    [SerializeField] private BoxCollider2D myCollider;
    [SerializeField] private LineManager lineMng;
    [SerializeField] private bool isVertical;
    [SerializeField] public int index;
    [SerializeField] private Vector3 basePos;
    [SerializeField] private Line myLine1;
    [SerializeField] private Line myLine2;
    

    public void SetBasePosition()
    {
        this.transform.position = basePos;
    }

    public void SetActive(bool val)
    {
        active = val;

        if (active)
        {
            rend.color = Color.white;
            SetBasePosition();
            myCollider.isTrigger = false;
        }
        else
        {
            rend.color = Color.gray;
            myCollider.isTrigger = true;
        }
    }

    private void OnMouseDown()
    {
        lineMng.PlusClick();
        SetActive(true);
        myLine1.SetActive(true);
        myLine2.SetActive(true);
        lineMng.SetScreen(isVertical, index);
        lineMng.ChangeGravityDirection(transform, isVertical);
    }

    public void SetPosition(int x, int y)
    {
        Vector3 pos = new Vector3(x, y, 0);
        this.transform.position = pos;
    }
    
}
