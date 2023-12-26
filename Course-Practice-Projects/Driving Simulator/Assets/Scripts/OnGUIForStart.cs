using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGUIForStart : MonoBehaviour
{
    public Camera Camera;

    // Took help from internet to understand how i can do this by code instead of 
    // using Canvas and UI elements as i had problem exporting them by only copying Assets
    // and Project settings folder. so used this old method of rendering text on screen
    // which is now used rarely due to use of canvas and Ui elements.

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.alignment = TextAnchor.MiddleCenter;
        style.fontSize = 30;
        style.normal.textColor = Color.red;

        Vector3 screenPos = Camera.WorldToScreenPoint(transform.position);
        Rect labelRect = new Rect(Screen.width / 2 - 150, Screen.height/2 - 50, 500, 200);
        // Display the text
        GUI.Label(labelRect, "Press Enter To Start\nRemember!!!\nPress R to Reset Your Car\nTo Starting Position\nPress G To Change Camera", style);
    }
}
