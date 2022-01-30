using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine. SceneManagement;

public class StartScript : MonoBehaviour
{
    [SerializeField] private Animator leftDoor;
    [SerializeField] private Animator rightDoor;

    private void Start()
    {
        AudioManager.Instance.Play("opening");
        Debug.Log("OPen");
    }

    public void NextLevelButton()
    {
        StartCoroutine(LoadScreen(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadScreen(int index)
    {
        
        Debug.Log("Transition");
        leftDoor.SetTrigger("move");
        rightDoor.SetTrigger("move");

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(index);

    }
}
