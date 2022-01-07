using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void OnMouseDown()
    {
        gameManager.NextLevel(SceneManager.GetActiveScene().buildIndex);
    }
    
    
}
