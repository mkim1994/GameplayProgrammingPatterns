using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject[] enemytypeprefabs;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("spawnEnemies", 0f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void spawnEnemies(){
		GameObject enemy = enemytypeprefabs [Random.Range (0, 2)];
		Instantiate (enemy, enemy.transform.position, Quaternion.identity);

	}

	/*IEnumerator spawnEnemies(){

	}*/
}
