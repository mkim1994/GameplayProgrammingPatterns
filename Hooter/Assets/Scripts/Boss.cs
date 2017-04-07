using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

	public int hp;
	private EnemyManager enemymanager;

	public GameObject bulletPrefab;

	private Player player;

	// Use this for initialization
	void Start () {
		
		hp = 6; //hp = 5, hp = 2

		enemymanager = GameObject.FindWithTag ("EnemyManager").GetComponent<EnemyManager> ();
		player = GameObject.FindWithTag ("Player").GetComponent<Player> ();



		enemymanager.numOfEnemyWaves = hp;
		enemymanager.resetWaves ();



		AppearTask appear = new AppearTask (this, enemymanager);
		SpawnTask spawn = new SpawnTask (this, enemymanager);
		FireTask fire = new FireTask (this, enemymanager);
		ChaseTask chase = new ChaseTask (this, enemymanager);

		appear.Then (spawn).Then (fire).Then (chase);

		Services.TaskManager.AddTask (appear);


	
	}

	void Update(){

		hp = enemymanager.numOfEnemyWaves - enemymanager.currentWave;
	}

	public void Fire(){
		GameObject bullet = Instantiate (bulletPrefab, new Vector3(0,0,0),Quaternion.identity) as GameObject;
		bullet.transform.position = new Vector3 (Random.Range (-100, 100), Random.Range (-100, 100), bullet.transform.position.z);
		bullet.transform.LookAt (GameObject.FindWithTag ("Player").transform);
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bullet.GetComponent<BossBullet>().speed;

		Destroy (bullet, bullet.GetComponent<BossBullet>().duration);
	}

	public void FireBullet(){
		InvokeRepeating ("Fire", 1f, 0.5f);
	}
	public void StopFiring(){
		CancelInvoke ("Fire");
	}

	public void ChasePlayer(){
		transform.LookAt (player.transform);
		if (Vector3.Distance (transform.position, player.transform.position) >= 0f) {
			transform.position += transform.forward * 4f * Time.deltaTime;
			if (Vector3.Distance (transform.position, player.transform.position) <= 10f) {

			}
		}
	}

}

public class AppearTask : Task {
	private readonly Boss _boss;
	private readonly EnemyManager _enemymanager;
	public AppearTask(Boss boss, EnemyManager enemymanager){
		_boss = boss;
		_enemymanager = enemymanager;

	}

	internal override void Update(){
		//Debug.Log ("appear");
		_boss.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
		if (_boss.transform.localScale.x >= 1f) {
			SetStatus (TaskStatus.Success);
		}
	}
}

public class SpawnTask : Task {
	private readonly Boss _boss;
	private readonly EnemyManager _enemymanager;
	public SpawnTask(Boss boss, EnemyManager enemymanager){
		_boss = boss;
		_enemymanager = enemymanager;


	}

	internal override void Update(){
		if (_boss.hp <= 5) {
			
			SetStatus (TaskStatus.Success);
		}
			

	}
}

public class FireTask : Task {
	private readonly Boss _boss;
	private readonly EnemyManager _enemymanager;


	public FireTask(Boss boss, EnemyManager enemymanager){
		_boss = boss;
		_enemymanager = enemymanager;

	}
	protected override void Init ()
	{
		_boss.FireBullet ();
	}

	internal override void Update(){
		if (_boss.hp <= 2) {
			_boss.StopFiring ();
			SetStatus (TaskStatus.Success);
		}


	}
}

public class ChaseTask : Task {
	private readonly Boss _boss;
	private readonly EnemyManager _enemymanager;
	public ChaseTask(Boss boss, EnemyManager enemymanager){
		_boss = boss;
		_enemymanager = enemymanager;

	}

	protected override void Init(){
		Debug.Log ("timetochase");
		_boss.transform.localScale = new Vector3(1f, 1f, 1f);
	}

	internal override void Update(){
		if (_boss.hp <= 0) {
			SetStatus (TaskStatus.Success);
		}
		_boss.ChasePlayer ();
	}
}
