using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

	/*
	 * abstract: all the functions in the class have to be abstract or have to be defined
	 * 
	 * An abstract function cannot have functionality.
	 * You're basically saying, any child class MUST give their own version of this method,
	 * however it's too general to even try to implement in the parent class.
	 * 
	 * A virtual function, is basically saying look,
	 * here's the functionality that may or may not be good enough for the child class.
	 * So if it is good enough, use this method, if not, then override me,
	 * and provide your own functionality.
	 * 
	 * virtual: takes subclasses and automatically uses functions that are appropriate if you use the sandbox class
	 * 
	 * base. works with: public, public virtual, and protected
	 * protected: can only be accessed/used from within the script + subclasses
	 * */

	protected int hp;
	protected int dmg;
	//protected string realname;

	protected AudioClip audioclip;

	public abstract void Move ();

	protected bool destroyed;

	protected EnemyManager enemymanager;

	void Awake(){
		enemymanager = GameObject.FindWithTag ("EnemyManager").GetComponent<EnemyManager> ();
		enemymanager.currentEnemyCount++;


	}

	protected void PlaySound(string clip, float volume){

		if (!GetComponent<AudioSource> ().isPlaying) {
			audioclip = Resources.Load<AudioClip>("Sounds/"+clip);
			GetComponent<AudioSource> ().PlayOneShot (audioclip, volume);
		}
	}

	public void takeDamage(int dealt){
		hp = hp - dealt;
	}

	public int enemyDamage(){
		return dmg;
	}

	public void SetHP(int health){
		hp = health;
	}
		

	protected void DestroyEnemyCheck(string enemyname){
		if (hp < 0 && !destroyed) {
			destroyed = true;
			enemymanager.currentEnemyCount--;
			PlaySound("enemy"+enemyname+"destroyed",1f);
			transform.GetChild (0).gameObject.GetComponent<MeshRenderer> ().enabled = false;
			Destroy(gameObject, audioclip.length);
		}
	}


}
