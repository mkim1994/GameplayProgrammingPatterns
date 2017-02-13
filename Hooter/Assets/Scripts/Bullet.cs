using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed;
	public float duration;

	public int damage;

	// Use this for initialization
	void Start () {
		//damage = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider){
		if (collider.GetComponent<Enemy> () != null) {
			collider.GetComponent<Enemy> ().takeDamage (damage);
			Destroy (gameObject);
		}
	}
}
