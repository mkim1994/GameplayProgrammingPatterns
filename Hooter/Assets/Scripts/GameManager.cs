using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	void Awake () {
		Services.TaskManager = new TaskManager ();
	}
	
	// Update is called once per frame
	void Update () {
		Services.TaskManager.Update ();
	}
}
