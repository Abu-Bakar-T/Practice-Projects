using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]

public class LevelText : MonoBehaviour {


	public static int Level = 0;
	public Text text;
	// Use this for initialization
	void Start () {
		text.text = "Level: " + Level;
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "Level: " + Level;
	}
}
