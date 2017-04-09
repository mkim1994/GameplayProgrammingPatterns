using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BehaviorTree; //importing namespace

public class EnemyE : Enemy {

	private Tree<EnemyE> _tree;
	private float angle, radius;
	public float speed;
	private float speedfactor;

	private Player player;


	// Use this for initialization
	void Start () {	
		//define the tree and use a selector at the root to pick the high level behavior
		_tree = new Tree<EnemyE> (new Selector<EnemyE> (
			//highest priority
			//Flee behavior
			new Sequence<EnemyE> (
				new IsHitWhilePulsing (),
				new Flee ()
			),
			//fight behavior
			new Sequence<EnemyE> (
				new Not<EnemyE>(new IsHitWhilePulsing()),
				new Not<EnemyE>(new FinishedPreparing()),
				new Attack ()
			),

			//prepare to attack
			new Sequence<EnemyE>(
				new IsNearPlayer(),
				new Not<EnemyE>(new IsHitWhilePulsing()),
				new PrepareToAttack()
			),
			//seek behavior
			new Sequence<EnemyE>(
				new Not<EnemyE>(new IsNearPlayer()),
				new Not<EnemyE>(new IsFleeing()),
				new Seek()
			)
		));

		//someListener = new UnityAction ();

		player = GameObject.FindWithTag ("Player").GetComponent<Player> ();



		type = "E";
		hp = 2;
		dmg = 1;




		PlaySound ("enemyDspawn", 1f);

	}

	void Update(){
		//Move ();
		_tree.Update(this);
		DestroyEnemyCheck ("E");
	}

	public override void Move (){
		transform.LookAt (player.transform);
		if (Vector3.Distance (transform.position, player.transform.position) >= 0f) {
			transform.position += transform.forward * 4f * Time.deltaTime;
			if (Vector3.Distance (transform.position, player.transform.position) <= 10f) {
			}
		}
	}

	public void MoveStraight(Transform dest){
		transform.LookAt (dest);
		if (Vector3.Distance (transform.position, dest.position) >= 0f) {
			transform.position += transform.forward * 10f * Time.deltaTime;
		}
	}

	public void Pulse(){
		
		transform.position += new Vector3 (Random.Range (-0.1f, 0.1f), Random.Range (-0.1f, 0.1f), 0f);
	}




	public override void increaseSpeed(float f){
	}



	/* NODES */

	//conditions
	private class IsNearPlayer : Node<EnemyE>{
		public override bool Update(EnemyE enemy){
			return true;
		}

	}

	private class IsHitWhilePulsing : Node<EnemyE>{
		public override bool Update(EnemyE enemy){
			return true;
		}
	}

	private class FinishedPreparing : Node<EnemyE>{
		public override bool Update(EnemyE enemy){
			return true;
		}
	}

	private class IsFleeing : Node<EnemyE>{
		public override bool Update(EnemyE enemy){
			return true;
		}
	}

	//actions: true means keep going, false means stop; so kind of like a while loop

	private class Flee : Node<EnemyE>{
		public override bool Update(EnemyE enemy){
			return true;
		}
	}

	private class Seek : Node<EnemyE>{
		public override bool Update(EnemyE enemy){
			return true;
		}
	}

	private class PrepareToAttack : Node<EnemyE>{
		public override bool Update(EnemyE enemy){
			return true;
		}
	}

	private class Attack : Node<EnemyE>{
		public override bool Update(EnemyE enemy){
			return true;
		}
	}
}
