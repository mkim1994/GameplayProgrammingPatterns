using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB : Enemy {
	
	float angle, speed, radius;

	// Use this for initialization
	void Start () {	

		//realname = this.name;

		hp = 1;
		dmg = 1;

		angle = 0;
		speed = (2 * Mathf.PI) / 10;
		radius = 20;


		PlaySound ("enemyBspawn", 1f);
	}

	public override void Move ()
	{
		angle += speed * Time.deltaTime;
		transform.position = new Vector3 (Mathf.Sin (angle) * radius, 0f,
		Mathf.Cos (angle) * radius);
		radius -= 0.1f;
	}


	
	// Update is called once per frame
	void Update () {
		Move ();
		DestroyEnemyCheck ("B");
		
	}

}
