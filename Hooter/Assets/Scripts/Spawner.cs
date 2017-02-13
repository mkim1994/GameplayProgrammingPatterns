using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {



	// Use this for initialization
	void Start () {
		InvokeRepeating ("spawnEnemies", 0f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	/*IEnumerator spawnEnemies(){

	}*/
}
