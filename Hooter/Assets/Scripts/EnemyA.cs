using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : Enemy {

	float angle, speed, radius;

	public EnemyA(){
		hp = 3;
		dmg = 2;
	}

	// Use this for initialization
	void Start () {
		angle = 0;
		speed = (2 * Mathf.PI) / 5;
		radius = 5;



		PlaySound ("enemyAspawn", 1f);
	}



	public override void Move ()
	{
		angle += speed * Time.deltaTime;
		transform.position = new Vector3 (Mathf.Cos (angle) * radius, 0f,
			Mathf.Sin (angle) * radius);
		//Debug.Log ("i'm moving");
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
		DestroyEnemyCheck ("A");
	}

}
