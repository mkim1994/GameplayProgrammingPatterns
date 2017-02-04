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

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.GetComponent<Enemy> () != null) {
			collision.gameObject.GetComponent<Enemy> ().takeDamage (damage);
		}
	}
}
