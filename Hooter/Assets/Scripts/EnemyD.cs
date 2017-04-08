using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyD : Enemy {

	private FSM<EnemyD> _fsm;

	private float angle, radius;
	public float speed;
	private float speedfactor;

	private Player player;



	//private UnityAction someListener;



	// Use this for initialization
	void Start () {	
		//someListener = new UnityAction ();

		player = GameObject.FindWithTag ("Player").GetComponent<Player> ();

		_fsm = new FSM<EnemyD> (this);
		_fsm.TransitionTo<Seeking> ();


		type = "D";
		hp = 2;
		dmg = 1;




		PlaySound ("enemyDspawn", 1f);

	}

	void Update(){
		_fsm.Update ();
		//Move ();
		DestroyEnemyCheck ("D");
	}

	public override void Move (){
		transform.LookAt (player.transform);
		if (Vector3.Distance (transform.position, player.transform.position) >= 0f) {
			transform.position += transform.forward * 4f * Time.deltaTime;
			if (Vector3.Distance (transform.position, player.transform.position) <= 10f) {
				_fsm.TransitionTo<Preparing> ();
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

	private class EnemyDState : FSM<EnemyD>.State{
		
	}

	private class Seeking : EnemyDState{

		public override void Update(){
			Context.Move ();
		}
	}

	private class Preparing : EnemyDState{
		private int count;

		public override void Init(){
			count = 0;
		}

		public override void Update(){
			if (count > 5) {
				TransitionTo<Attacking> (); //still need to transitionto fleeing when it's damaged while pulsing
				return;
			} else {
				count++;
				Context.Pulse (); //make it more obvious that it's pulsing, give more time in-between
				if(Context.dmg < 2) {
					TransitionTo<Fleeing> ();
					return;
				}
			}
		}
	}

	private class Attacking : EnemyDState{
		
		private GameObject destination;

		public override void Init(){
			destination = new GameObject ("destination");
			destination.transform.position = GameObject.FindWithTag("Player").transform.position;
		}

		public override void Update(){
			Context.MoveStraight (destination.transform);
		}
		public override void CleanUp(){
			Destroy (destination);
		}
	}

	private class Fleeing : EnemyDState {
		private GameObject destination;

		public override void Init(){
			destination = new GameObject ("fleeingdestination");
			Vector3 playerPos = GameObject.FindWithTag ("Player").transform.position;
			float randomXa = playerPos.x+Random.Range (-30f, -10f);
			float randomXb = playerPos.x+Random.Range (10f, 30f);
			float randomYa = playerPos.y+Random.Range (-30f, -10f);
			float randomYb = playerPos.y+ Random.Range (10f, 30f);
			int r = Random.Range (0, 4);
			if (r == 0) {
				destination.transform.position = new Vector3 (randomXa, randomYa, playerPos.z);
			} else if (r == 1) {
				destination.transform.position = new Vector3 (randomXb, randomYa, playerPos.z);
			} else if (r == 2) {
				destination.transform.position = new Vector3 (randomXa, randomYb, playerPos.z);
			} else if (r == 3) {
				destination.transform.position = new Vector3 (randomXb, randomYb, playerPos.z);
			}
		}

		public override void Update(){
			if (Vector3.Distance (Context.transform.position, destination.transform.position) >= 0f) {
				Context.MoveStraight (destination.transform);
			} else {
				TransitionTo<Seeking> ();
			}
		}

		public override void CleanUp(){
			Destroy (destination);
		}
	}

}
