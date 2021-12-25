using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vLine : MonoBehaviour
{
    [SerializeField] private verticalLineManager vMng;
    [SerializeField] private SpriteRenderer rend;
    [SerializeField] public bool isActive;
    [SerializeField] private BoxCollider2D myCollider;
    private bool vertical;
    private bool gameStart;

    public void SetIsActive(bool val)
    {
        isActive = val;
        if (isActive)
        {
            rend.color = Color.white;
            myCollider.isTrigger = false;
        }
        else // "not active"
        {
            rend.color = Color.gray;
            myCollider.isTrigger = true;
        }
    }
    
    
}
