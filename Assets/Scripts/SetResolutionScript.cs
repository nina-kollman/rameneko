using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetResolutionScript : MonoBehaviour
{
        
    private const float landscapeRatio =  1920f/1080f;

    /**
     * Adapts the screen resolution to the current device playing the game
     */
    void Start()
    {
        Debug.Log("Resolution, width: " + Screen.width + ", height: " + Screen.height);

        // Get the real ratio
        float ratio = (float)Screen.width / (float)Screen.height;

        // Camera settings to landscape
        if (ratio >= landscapeRatio)
        {
            Camera.main.orthographicSize = 1080f / 200f;
        }
        else
        {
            float scaledHeight = 1920f / ratio;
            Camera.main.orthographicSize = scaledHeight / 200f;
        }
    }
}
