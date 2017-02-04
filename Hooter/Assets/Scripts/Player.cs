using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class Player : MonoBehaviour {
	//x = x, z is actually y (vertical movement)

	public int hp;

	public float speed;
	public float tilt;

	public GameObject bulletPrefab;
	public Transform bulletSpawn;

	public Boundary boundary;
	Rigidbody rb;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();

		//rb.freezeRotation = true;
	}
	
	// Update is called once per frame

	void Update(){

		if (hp < 0) {
			Debug.Log ("gameover");
		}

		MovePlayer ();
		DongDirection ();
		FireBullet ();


	}

	void FixedUpdate () {
	}

	void MovePlayer(){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;

		//clamping manually b/c Mathf.Clamp isn't really cooperating...?
		if (rb.position.x < boundary.xMin) {
			rb.position = new Vector3 (boundary.xMin, rb.position.y, rb.position.z);
		} else if (rb.position.x > boundary.xMax) {
			rb.position = new Vector3 (boundary.xMax, rb.position.y, rb.position.z);
		}
		if (rb.position.z < boundary.zMin) {
			rb.position = new Vector3 (rb.position.x, rb.position.y, boundary.zMin);
		} else if (rb.position.z > boundary.zMax) {
			rb.position = new Vector3 (rb.position.x, rb.position.y, boundary.zMax);
		}
	}
	void DongDirection(){
		Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		//subtract bulletspawn pos from mouseworldpos to get the distance from the bulletspawn pos to the mouseworldpos
		//
		float angle = Mathf.Atan2 (
			mouseWorldPosition.x - bulletSpawn.position.x,
			mouseWorldPosition.z - bulletSpawn.position.z) * Mathf.Rad2Deg;
		//quaternion.euler returns a rotation that rotates z degrees around the z axis, x degrees around the x axis, etc.
		//euler angle is an angle between 0 degrees and 360 degrees
		//transform.rotation is not a euler angle; it's a quaternion
		//so euler angles are a way to read quaternions as a vector3 value
		//also, bulletspawn point should only be rotated along the y-axis
		bulletSpawn.rotation = Quaternion.Euler (new Vector3 (0f, angle, 0f));
	}

	void FireBullet(){
		if (Input.GetMouseButtonUp(0)) {
			GameObject bullet = Instantiate (bulletPrefab, bulletSpawn.GetChild(0).position, bulletSpawn.rotation) as GameObject;
			bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bullet.GetComponent<Bullet>().speed;
			Destroy (bullet, bullet.GetComponent<Bullet>().duration);
		}
	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.GetComponent<Enemy> () != null) {
			hp -= collision.gameObject.GetComponent<Enemy> ().enemyDamage ();
			collision.gameObject.GetComponent<Enemy> ().SetHP (-1);
		}
	}


}
