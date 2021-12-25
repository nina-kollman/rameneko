using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hLine : MonoBehaviour
{
    private bool isActive; 
    [SerializeField] private BoxCollider2D myCollider;

    
    public void SetIsActive(bool val)
    {
        isActive = val;
        
    }
}
