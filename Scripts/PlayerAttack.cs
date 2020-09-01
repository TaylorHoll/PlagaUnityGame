using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

	private PlayerController player;
	public bool attacking = false; 

	private float attackTimer = 0;
	private float attackCD = 0.3f;

	public Collider2D attackTrigger;

	private Animator anim;

	//audio
	public AudioClip swordSwoosh;
	public AudioSource soundSource;

	void Start() {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		soundSource.clip = swordSwoosh;
	}

	void Awake(){
		anim = gameObject.GetComponent<Animator> ();
		attackTrigger.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown ("f") && !attacking && !player.isGrounded) {

			//player.myRigidbody.transform.localScale = new Vector3 (player.moveSpeed, 1f, 1f); 
			attackCD = 0.6f;
			attacking = true;
			attackTimer = attackCD;

			attackTrigger.enabled = true;
			soundSource.Play ();

		}
		else if (Input.GetKeyDown ("f") && !attacking && player.isGrounded) {
			//player.myRigidbody.transform.localScale = new Vector3 (player.moveSpeed, 1f, 1f); 
			attackCD = 0.3f;
			attacking = true;
			attackTimer = attackCD;

			attackTrigger.enabled = true;
			soundSource.Play ();



		}

		if (attacking) {
			if (attackTimer > 0) {
				attackTimer -= Time.deltaTime;
			} else {
				attacking = false;
				attackTrigger.enabled = false;
			}
		}


	
		anim.SetBool ("Attacking", attacking);
	
	}
}
