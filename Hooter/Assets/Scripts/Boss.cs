using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

	public int hp;
	private EnemyManager enemymanager;

	// Use this for initialization
	void Start () {
		enemymanager = GameObject.FindWithTag ("EnemyManager").GetComponent<EnemyManager> ();

		AppearTask appear = new AppearTask (this, enemymanager);
		SpawnTask spawn = new SpawnTask (this, enemymanager);
		FireTask fire = new FireTask (this, enemymanager);
		ChaseTask chase = new ChaseTask (this, enemymanager);

		appear.Then (spawn).Then (fire).Then (chase);

		Services.TaskManager.AddTask (appear);

		Debug.Log ("Boss");
	
	}
	
	// Update is called once per frame
	void Update () {
		
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
		Debug.Log ("spawn");
		SetStatus (TaskStatus.Success);
	}
}

public class FireTask : Task {
	private readonly Boss _boss;
	private readonly EnemyManager _enemymanager;
	public FireTask(Boss boss, EnemyManager enemymanager){
		_boss = boss;
		_enemymanager = enemymanager;

	}

	internal override void Update(){
		Debug.Log ("fire");
		SetStatus (TaskStatus.Success);
	}
}

public class ChaseTask : Task {
	private readonly Boss _boss;
	private readonly EnemyManager _enemymanager;
	public ChaseTask(Boss boss, EnemyManager enemymanager){
		_boss = boss;
		_enemymanager = enemymanager;

	}

	internal override void Update(){
		Debug.Log ("chase");
		SetStatus (TaskStatus.Success);
	}
}
