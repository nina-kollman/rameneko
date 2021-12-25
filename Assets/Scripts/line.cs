using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class line : MonoBehaviour
{
    [SerializeField] private SpriteRenderer rend;
    [SerializeField] private bool active;
    [SerializeField] private BoxCollider2D myCollider;
    [SerializeField] private lineManager lineMng;
    [SerializeField] private bool isVertical;
    [SerializeField] public int index;
    [SerializeField] private Vector3 basePos;
    [SerializeField] private line myLine1;
    [SerializeField] private line myLine2;
    

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
    }

    public void SetPosition(int x, int y)
    {
        Vector3 pos = new Vector3(x, y, 0);
        this.transform.position = pos;
    }
    
}
