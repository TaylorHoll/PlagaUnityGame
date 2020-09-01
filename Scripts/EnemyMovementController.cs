using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour {

	public float enemySpeed;

	Animator enemyAnimator;

	//facing
	public GameObject enemyGraphic;
	bool canFlip = true;
	bool facingRight = false;
	float flipTime = 5f;
	float nextFlipChance = 0f;

	//attacking 
	public float chargeTime;
	float startChargeTime;
	bool charging;
	Rigidbody2D enemyRB;

	//knockback
	public float knockbackForce = 100f;
	public float knockbackLength = 20f;
	private float knockbackCount;

	//Sound
	public AudioClip dogBark;
	public AudioSource soundSource;

	// Use this for initialization
	void Start () {
		enemyAnimator = GetComponentInChildren<Animator> ();
		enemyRB = GetComponent<Rigidbody2D> ();
		soundSource.clip = dogBark;
	}
	
	// Update is called once per frame
	void Update () {

		if (knockbackCount > 0) 
		{
			knockbackCount -= Time.deltaTime;
			//knockbackForce = 5f;
			//knockbackLength = 0.2f;
			if(transform.localScale.x >0)
				enemyRB.velocity = new Vector3(-knockbackForce, knockbackForce, 0f);
			else 
				enemyRB.velocity = new Vector3(knockbackForce, knockbackForce, 0f);
		}

			if (Time.time > nextFlipChance) {
				if (Random.Range (0, 10) >= 5) {
					flipFacing ();
				}
				nextFlipChance = Time.time + flipTime;
			}
				
		}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {
			if (facingRight && other.transform.position.x < transform.position.x) {
				flipFacing ();
			} else if(!facingRight && other.transform.position.x > transform.position.x) {
				flipFacing ();
			}
			canFlip = false;
			charging = true;
			startChargeTime = Time.time + chargeTime;
			soundSource.Play ();
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.tag == "Player") {
			if (startChargeTime < Time.time) {
				if (!facingRight) {
					enemyRB.AddForce (new Vector2 (-enemySpeed, 0));
					enemyAnimator.SetBool ("isCharging", charging);

				} else {
					enemyRB.AddForce (new Vector2 (enemySpeed, 0));
					enemyAnimator.SetBool ("isCharging", charging);

				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			canFlip = true;
			charging = false;
			enemyRB.velocity = new Vector2 (0f, 0f);
			enemyAnimator.SetBool ("isCharging", charging);
		}
	}

	public void Knockback()
	{
		knockbackCount = knockbackLength;
		//invincibilityCounter = invincibilityLength;
		//invincible = true;

	}

	void flipFacing() {
		if (!canFlip) {
			return;
		}
		float facingX = enemyGraphic.transform.localScale.x;
		facingX *= -1f;
		enemyGraphic.transform.localScale = new Vector3 (facingX, enemyGraphic.transform.localScale.y, enemyGraphic.transform.localScale.z);
		facingRight = !facingRight;
			
	}
		
}
