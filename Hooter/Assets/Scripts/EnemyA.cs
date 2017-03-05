using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : Enemy {

	private float angle, speed, radius;

	// Use this for initialization
	void Start () {


		type = "A";

		hp = 1;
		dmg = 2;


		angle = 0;
		speed = (2 * Mathf.PI) / 20f;
		radius = 20;

		PlaySound ("enemyAspawn", 1f);
	}



	public override void Move ()
	{
		angle += speed * Time.deltaTime;
		transform.position = new Vector3 (Mathf.Cos (angle) * radius, 0f,
			Mathf.Sin (angle) * radius);
		radius -= 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
		DestroyEnemyCheck ("A");
	}

	public override void increaseSpeed(float f){
	//	Debug.Log (type);
	}

}
