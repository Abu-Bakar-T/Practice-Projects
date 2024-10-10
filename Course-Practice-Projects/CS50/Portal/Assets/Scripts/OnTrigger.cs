using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrigger : MonoBehaviour {

	public GameObject player;

	// For Creating new Camera
	public Camera newCamera;
	void Start()
	{
		newCamera.enabled = false;
	}
	void OnTriggerEnter(Collider other) {
		newCamera.enabled = true;
		Destroy(player);
	}
}