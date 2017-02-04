using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB : Enemy {
	
	float angle, speed, radius;

	public EnemyB(){
		hp = 1;
		dmg = 1;
	}

	// Use this for initialization
	void Start () {	
		angle = 0;
		speed = (2 * Mathf.PI) / 5;
		radius = 10;


		PlaySound ("enemyBspawn", 1f);
	}

	public override void Move ()
	{
		angle += speed * Time.deltaTime;
		transform.position = new Vector3 (Mathf.Sin (angle) * radius, 0f,
		Mathf.Cos (angle) * radius);
	}


	
	// Update is called once per frame
	void Update () {
		Move ();
		DestroyEnemyCheck ("B");
		
	}

}
