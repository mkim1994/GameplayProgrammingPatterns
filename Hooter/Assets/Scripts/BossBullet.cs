using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour {

	public float speed;
	public float duration;

	public int damage;

	// Use this for initialization
	void Awake () {
		//damage = 1;
		transform.position = new Vector3 (Random.Range (0, 300), Random.Range (0, 300), transform.position.z);
		GetComponent<Rigidbody>().velocity = transform.forward * speed;
		transform.LookAt (GameObject.FindWithTag ("Player").transform);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider){
	/*	if (collider.GetComponent<Enemy> () != null) {
			collider.GetComponent<Enemy> ().takeDamage (damage);
			Destroy (gameObject);
		}*/
		//decrease player health
	}
}
