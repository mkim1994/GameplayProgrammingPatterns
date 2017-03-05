using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour {

	public int numOfEnemyWaves;
	public int minNumOfEnemiesPerWave, maxNumOfEnemiesPerWave;
	public float minEnemyWaveTime,maxEnemyWaveTime;
	public float minEnemySpawnTime, maxEnemySpawnTime;
	public GameObject[] enemytypeprefabs;

	private int currentWave;
	private bool waveComplete;

	[HideInInspector]
	public int currentEnemyCount;
	public int numOfEnemiesDestroyed;
	[HideInInspector]
	public List<EnemyWave> enemyWaves;
	[HideInInspector]
	public List<float> timeBetweenWaves;

	private float enemyCspeed;

	void Awake(){
		EventManager.StartListening ("AnEnemyDestroyed", AnEnemyDestroyed);
		enemyWaves = new List<EnemyWave> ();
		makeEnemyWaves ();
		StartCoroutine (spawnWave ());
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//should only go over to the next wave when current number of enemies is 0
		//for circular behavior use mod
		if (waveComplete && currentEnemyCount == 0 && currentWave != numOfEnemyWaves) {
			waveComplete = false;
			StartCoroutine (spawnNextWave ());
		} else if (currentWave > numOfEnemyWaves) {
			EventManager.StopListening ("AnEnemyDestroyed", AnEnemyDestroyed);
		}
	}

	IEnumerator spawnNextWave(){
		yield return new WaitForSeconds (timeBetweenWaves [currentWave]);
		StartCoroutine(spawnWave ());
	}

	IEnumerator spawnWave(){
		/*spawns enemies in a wave*/
		for(int i = 0; i < enemyWaves[currentWave].enemyList.Count; i++){
			//enemyWaves[currentWave].enemyList[i].GetComponent<Enemy> ().increaseSpeed (numOfEnemiesDestroyed * 10f);

			//enemyWaves[currentWave].enemyList[i].GetComponent<Enemy>().speed = (2f * Mathf.PI) / (20f - factor);

			spawnEnemy (enemyWaves[currentWave].enemyList[i]);
			yield return new WaitForSeconds (enemyWaves[currentWave].spawnRates[i]);
		}

		//current wave has been fully iterated

		currentWave++;
		waveComplete = true;

	}

	void spawnEnemy(GameObject enemy){
		Instantiate (enemy, enemy.transform.position, Quaternion.identity);
	}

	void makeEnemyWaves(){
		/*makes enemy waves*/
		for (int i = 0; i < numOfEnemyWaves; i++) {
			makeWave (); //generates and adds an enemy wave to the list
			timeBetweenWaves.Add(Random.Range(minEnemyWaveTime,maxEnemyWaveTime));
		}
	}

	void generateEnemyPattern(EnemyWave wave){
		int numOfEnemies = Random.Range (minNumOfEnemiesPerWave,maxNumOfEnemiesPerWave);
		for (int i = 0; i < numOfEnemies; i++) {
			wave.spawnRates.Add (Random.Range (minEnemySpawnTime, maxEnemySpawnTime));
			wave.enemyList.Add(enemytypeprefabs[Random.Range(0,enemytypeprefabs.Length)]);
		}

	}

	void makeWave(){
		EnemyWave wave = new EnemyWave ();
		generateEnemyPattern (wave);
		enemyWaves.Add (wave);

	}

	void AnEnemyDestroyed(){
		numOfEnemiesDestroyed++;
		/*foreach (GameObject enemy in enemyWaves[currentWave].enemyList) {
			//enemy.GetComponent<Enemy> ().increaseSpeed (numOfEnemiesDestroyed * 1f);

		}*/
		//enemyCspeed = (2f * Mathf.PI) / (20f - );
	}


}
