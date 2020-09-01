using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour {


	public float enemySpeed;

	//animation
	private Animator enemyAnimator;
//	private float waitTime = 0f;
	//private PlayerController player;
	//private bool hit = false;

	//attacking
	private bool attacking = false; 
	private float attackTimer = 0;
	private float attackCD = 1f;
	public Collider2D skeletonAttTrigger;
	public Collider2D skeletonTrigger;
	//public Collider2D patrol;

	//facing
	public GameObject enemyGraphic;

	//Health
	public float maxHealth;
	public float currHealth;

	//attacking 
	//public float chargeTime;
	//float startChargeTime;
	//bool charging;
	public Rigidbody2D enemyRB;

	//Audio 
	public AudioClip skeletonAttack;
	public AudioSource soundSource;
	public AudioClip skeletonHit;
	public AudioSource soundSourceTwo;
	public AudioClip skeletonDead;
	public AudioSource soundSourceThree;



	//knockback
	//public float knockbackForce = 5f;
	//public float knockbackLength = 0.2f;
	//private float knockbackCount;

	//Sound
	//public AudioClip dogBark;
	//public AudioSource soundSource;

	// Use this for initialization
	void Start () {
		//enemyAnimator = GetComponentInChildren<Animator> ();
		enemyRB = GetComponent<Rigidbody2D> ();
		//player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		//soundSource.clip = dogBark;
		currHealth = maxHealth;
		soundSource.clip = skeletonAttack;
		soundSourceTwo.clip = skeletonHit;
		soundSourceThree.clip = skeletonDead;
	}

	void Awake(){
		enemyAnimator = gameObject.GetComponentInChildren<Animator> ();
		skeletonAttTrigger.enabled = false;


	}

	// Update is called once per frame
	void Update () {

		transform.Translate (new Vector3 (enemySpeed, 0, 0) * Time.deltaTime);

		if (attacking) {
			if (attackTimer > 0) {
				attackTimer -= Time.deltaTime;

				//soundSourceTwo.Play ();
				//soundSource.Play ();
			//	enemyAnimator.SetBool ("isAttacking", attacking);
			} else {
				attacking = false;
				skeletonAttTrigger.enabled = false;
			}
		}

		//enemyAnimator.SetFloat ("Speed", Mathf.Abs (enemyRB.velocity.x));
		enemyAnimator.SetBool ("isAttacking", attacking);







	}


			
	void OnTriggerEnter2D (Collider2D other) {

		if (other.tag == "PatrolBlock") {
			enemySpeed *= -1;
			enemyRB.transform.localScale = new Vector3 (enemySpeed * -1f, 1f, 1f);  
			//enemyRB.transform.localScale 

		}
			
		if (other.tag == "Player") {
			//attackCD = 3f;

			attacking = true;
			attackTimer = attackCD;

			skeletonAttTrigger.enabled = true;
			soundSource.Play();
			//soundSourceThree.Play();
			//startChargeTime = Time.time + chargeTime;
			//soundSource.Play ();
		}

		/*if (other.CompareTag("Player") && !player.invincible)
		{
			player.Damage (1);
			player.Knockback ();
		}*/
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.tag == "Player") {
			//attackCD = 3f;
			attacking = true;
			attackTimer = attackCD;

			skeletonAttTrigger.enabled = true;
			//soundSourceThree.Play();
			//startChargeTime = Time.time + chargeTime;
			//soundSource.Play ();
		}
		//enemyAnimator.SetBool ("isAttacking", attacking);	
	}	

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			
			attacking = true;
			//enemyRB.velocity = new Vector2 (0f, 0f);
			//enemyAnimator.SetBool ("isAttacking", attacking);
		}
	}


	public void addDamage(float damage)
	{
		
		//hit = true;
		//enemyAnimator.SetBool ("Hit", hit);
		//soundSourceTwo.Play();

		enemyAnimator.SetTrigger ("isHit");
		soundSourceTwo.Play ();
		attacking = false;
		skeletonAttTrigger.enabled = false;
		currHealth -= damage;
	
		if (currHealth <= 0) {
			//hit = false;
			skeletonTrigger.enabled = false;
			enemyAnimator.SetTrigger ("isDead");
			//skeletonAttTrigger.enabled = false;
			//yield return new WaitForSeconds(waitTime);
			//StartCoroutine(makeDead(enemyAnimator.GetCurrentAnimatorStateInfo(0).length));
			soundSourceThree.Play();
			makeDead ();
		} 
	}

	/*public IEnumerator makeDead (float waitTime = 1000) {
	//	yield return new WaitForSeconds(waitTime);
	//	Destroy(gameObject);
	//}
	//*/

	void makeDead(){
		Destroy (gameObject, 1.2f);
	}





}
	/*public Animator anim;
	public Transform[] patrolPoints;
	public float speed;
	Transform currentPatrolPoint;
	int currentPatrolIndex;

	public Transform target;
	public float chaseRange;

	public float attackRange;
	public int damage;
	private float lastAttackTime;
	public float attackDelay;

	public GameObject projectile;
	public float webForce;

	public float awarenessRange;

	void Start () {
		currentPatrolIndex = 0;
		currentPatrolPoint = patrolPoints [currentPatrolIndex];
	}

	void Update () {
		float distanceToTarget = Vector3.Distance (transform.position, target.position);
		if (distanceToTarget < chaseRange) {
			Vector3 targetDir = target.position = transform.position;
			float angle = Mathf.Atan2 (targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;
			//	Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
			//	transform.rotation = Quaternion.RotateTowards (transform.rotation, q, 180);

			transform.Translate (Vector3.up * Time.deltaTime * speed);
		}
	}*/

