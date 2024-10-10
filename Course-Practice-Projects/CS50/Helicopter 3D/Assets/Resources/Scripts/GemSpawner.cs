using UnityEngine;
using System.Collections;

public class GemSpawner : MonoBehaviour {

	public GameObject[] prefabs;

	// Use this for initialization
	void Start () {

		// infinite coin spawning function, asynchronous
		StartCoroutine(SpawnGem());
	}

	// Update is called once per frame
	void Update () {

	}

	IEnumerator SpawnGem() {
		while (true) {

			// instantiate Gems in space
			Instantiate(prefabs[Random.Range(0, prefabs.Length)], new Vector3(26, Random.Range(7, 9), 10), Quaternion.identity);

			// pause 1-5 seconds until the next coin spawns
			yield return new WaitForSeconds(Random.Range(5, 10));
		}
	}
}
