﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyC : Enemy {
	
	float angle, radius;
	float speed;
	float speedfactor;

	//private UnityAction someListener;

	// Use this for initialization
	void Start () {	
		//someListener = new UnityAction ();

		type = "C";
		hp = 1;
		dmg = 1;

		angle = 0f;

		speed = (2f * Mathf.PI) / 20f;
		radius = 20f;


		PlaySound ("enemyCspawn", 1f);

		print (GetType ());
	}

	public override void Move ()
	{
		

		//Debug.Log (speed);
		angle += speed * Time.deltaTime;
		transform.position = new Vector3 (Mathf.Sin (angle) * radius, 0f,
		Mathf.Cos (angle) * radius);
		radius -= 0.1f;
	}


	
	// Update is called once per frame
	void Update () {
		Move ();
		DestroyEnemyCheck ("C");
	}

	public void increaseSpeed(float factor){
		//Debug.Log (speedfactor);
		//print ("factor: " + factor + ", speedfactor: " + speedfactor);
		if (factor < 20f) {

		
			speed = (2f * Mathf.PI) / (20f - factor);
			//Debug.Log (speed);
		}

	}
}
