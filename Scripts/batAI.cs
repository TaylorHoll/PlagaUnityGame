using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using Pathfinding; 

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Seeker))]


public class batAI : MonoBehaviour {

	/*//facing
	public GameObject enemyGraphic;
	bool canFlip = true;
	bool facingRight = false;
	float flipTime = 5f;
	float nextFlipChance = 0f;
	*/

	//Script Calls
	private PlayerController player;
	//private EnemyMovementController enemy;

	//Health
	public float maxHealth;
	public float currHealth;

	//Animator
	private Animator anim;

	//what to chase?
	public Transform target;

	//how many times each second we will update our path
	public float updateRate = 2f; 

	//Catching
	private Seeker seeker; 
	private Rigidbody2D rb;

	//the calculated path
	public Path path;

	//Ai's speed per second
	public float speed = 300f;
	public ForceMode2D fMode;

	//Audio
	public AudioSource soundSource;
	public AudioClip enemySound;



	[HideInInspector]
	public bool pathIsEnded = false;

	//The max distance from the AI to a waypoint for it to continue to the next waypoint
	public float nextWaypointDistance = 3; 

	private int currentWaypoint = 0;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		currHealth = maxHealth;
		anim = GetComponent<Animator> ();
		soundSource.clip = enemySound;

		seeker = GetComponent<Seeker> ();
		rb = GetComponent <Rigidbody2D> ();

		if(target == null) {
			Debug.LogError ("No player found? PANIC!");
			return;
		}

		seeker.StartPath (transform.position, target.position, OnPathComplete);
	
		StartCoroutine (UpdatePath ());
	}

	void Update() {
		/*if (Time.time > nextFlipChance) {
			if (Random.Range (0, 10) >= 5) {
				flipFacing ();
			}
			nextFlipChance = Time.time + flipTime;
		}*/
	}

	IEnumerator UpdatePath() {
		if (target == null) {
			//TODO: insert a player search here. 
			yield return null;
		}

		seeker.StartPath (transform.position, target.position, OnPathComplete);

		yield return new WaitForSeconds (1f/updateRate);
		StartCoroutine (UpdatePath());
	}

	public void OnPathComplete (Path p) {
		Debug.Log ("We got a path. Did it have an error? " + p.error);
		if (!p.error) {
			path = p;
			currentWaypoint = 0;
		}
	}

	void FixedUpdate() {
		if (target == null) {
			//TODO: insert a player search here. 
			return;
		}

		//TODO: //always look at player?

		if (path == null)
			return;

		if (currentWaypoint >= path.vectorPath.Count) {
			if (pathIsEnded)
				return;

			Debug.Log ("End of path reached.");
			pathIsEnded = true;
			return;
		}
		pathIsEnded = false;

		//direction to the next waypoint
		Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position ).normalized;
		dir *= speed * Time.fixedDeltaTime;

		//move the AI
		rb.AddForce (dir, fMode);

		float dist = Vector3.Distance (transform.position, path.vectorPath[currentWaypoint]);
		if (dist < nextWaypointDistance) {
			currentWaypoint++;
			return;
		}
	}

	public void OnTriggerEnter2D(Collider2D col)
	{
		/*if (col.tag == "Player") {
			if (facingRight && col.transform.position.x < transform.position.x) {
				flipFacing ();
			} else if(!facingRight && col.transform.position.x > transform.position.x) {
				flipFacing ();
			}
			canFlip = false;
		}
		*/

		if (col.CompareTag("Player") && !player.invincible)
		{
			player.Damage (1);
			player.Knockback ();
		}


	}

	public void addDamage(float damage)
	{
		soundSource.Play();
		anim.SetTrigger ("isHit");

		currHealth -= damage;

		if (currHealth <= 0) {
			makeDead ();
		}
	}

	void makeDead () {
		Destroy(gameObject);
	}

	/*void OnTriggerEnter2D(Collider2D other)
	{
		
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			canFlip = true;
		}
	}

	void flipFacing() {
		if (!canFlip) {
			return;
		}
		float facingX = enemyGraphic.transform.localScale.x;
		facingX *= -1f;
		enemyGraphic.transform.localScale = new Vector3 (facingX, enemyGraphic.transform.localScale.y, enemyGraphic.transform.localScale.z);
		facingRight = !facingRight;

	}*/

}