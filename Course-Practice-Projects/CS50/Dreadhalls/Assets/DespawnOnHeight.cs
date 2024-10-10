using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DespawnOnHeight : MonoBehaviour {
	public GameObject characterController;
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < -5) {
			LevelText.Level = 0;
			SceneManager.LoadScene("GameOver");
		}
	}
}
