﻿using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {

    public Texture2D crosshairTexture;
    public float crosshairScale = 1;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //lock the cursor to the center of the screen (i hope)
    }

    void OnGUI()
    {
        //if not paused
        if (Time.timeScale != 0)
        {
            if (crosshairTexture != null)
                GUI.DrawTexture(new Rect((Screen.width - crosshairTexture.width * crosshairScale) / 2, (Screen.height - crosshairTexture.height * crosshairScale) / 2, crosshairTexture.width * crosshairScale, crosshairTexture.height * crosshairScale), crosshairTexture);
            else{ }
        }
    }
}