using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGHead : MonoBehaviour {

	//Objects
	public GameObject projectile; 
	public GameObject projectile2;
	public GameObject projectile3; 
	public GameObject projectile4; 
	public GameObject projectile5;
	public GameObject projectile6; 
	public GameObject bossObject; 
	public GameObject vortex;
	public GameObject bullet;
	public GameObject bullet2;
	public GameObject bullet3;
	public GameObject bullet4;
	public GameObject bullet5;
	public GameObject bullet6;
	public GameObject Door;
	//public AnimationClip animAwake;

	private PlayerController player;

	//AUDIO 
	public AudioClip androHit;
	public AudioClip androAwaken;
	//public AudioClip bossTheme;
	public AudioClip androDead; 
	public AudioSource soundSource;
	public AudioSource soundSource2;
	public AudioSource soundSource3;

	//Speed
	//public float enemySpeed;
	private float speed = 6;
		

	//Array
	public Transform[] spots;
	public Transform[] holes;
	//public Sprite[] sprites;

	//Bool
	public bool isShooting;
	public bool isVortex;

	//Animator
	private Animator enemyAnimator;
	//private Animator vortexAnim;

	//Health
	public float maxHealth;
	public float currHealth;

	//attacking 
	public Rigidbody2D enemyRB;

	//public bool vulnerable; 
	//Vector3 playerPos;
	//Vector3 tempPos;

	// Use this for initialization
	void Start () {
		//player = FindGameObjectsWithTag ("Player");
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		//vortexAnim = GameObject.FindGameObjectWithTag ("Vortex").GetComponent<Animator> ();

		//enemyAnimator = GetComponent<Animator> ();
		currHealth = maxHealth;
		soundSource.clip = androHit;
		soundSource2.clip = androAwaken;
		soundSource3.clip = androDead;
		//soundSource3.clip = bossTheme;

		soundSource2.Play ();


		//StartCoroutine("boss");


	

	}

	void Awake(){
		speed = speed * Time.deltaTime;
		enemyAnimator = gameObject.GetComponent<Animator> ();
		StartCoroutine("boss");
		//skeletonAttTrigger.enabled = false;


	}
	
	// Update is called once per frame
	void Update () {
		//enemyAnimator.SetBool ("isShooting", isShooting);
	}

	public void addDamage(float damage)
	{

		//hit = true;
		//enemyAnimator.SetBool ("Hit", hit);
		//soundSourceTwo.Play();
		soundSource.Play ();
		enemyAnimator.SetTrigger ("isHit");
		//attacking = false;
		//skeletonAttTrigger.enabled = false;

		currHealth -= damage;

		if (currHealth <= 0) {
			
			soundSource3.Play();
			StopCoroutine("boss");
			enemyAnimator.SetTrigger ("isDead");
		
			//hit = false;
			//skeletonTrigger.enabled = false;
			//skeletonAttTrigger.enabled = false;
			//yield return new WaitForSeconds(waitTime);
			//StartCoroutine(makeDead(enemyAnimator.GetCurrentAnimatorStateInfo(0).length));
			makeDead ();
			Door.SetActive (true);
		} 
	}

	void makeDead(){


		Destroy (gameObject, 4.5f);


	}

	public void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Player") && !player.invincible)
		{
			player.Damage (1);
			player.Knockback ();
		}
	}



		IEnumerator boss ()
	{
		
		vortex.SetActive (false);
		yield return new WaitForSeconds (5f);

		while (true) {
			//enemyAnimator.SetTrigger ("isAwoke");




			//enemyAnimator.SetTrigger("androIdle");


			//MOVEMENT PHASE TO TAKE ATTACKS

			while (transform.position.x != spots [3].position.x) {
				//GetComponent<SpriteRenderer> ().sprite = sprites [1];
				transform.position = Vector2.MoveTowards (transform.position, new Vector2 (spots [3].position.x, spots[3].position.y), speed);
				yield return null;
			}

			while (transform.position.x != spots [4].position.x) {
				//GetComponent<SpriteRenderer> ().sprite = sprites [1];
				transform.position = Vector2.MoveTowards (transform.position, new Vector2 (spots [4].position.x, spots[4].position.y), speed);
				yield return null;
			}

			while (transform.position.x != spots [5].position.x) {
				//GetComponent<SpriteRenderer> ().sprite = sprites [1];
				transform.position = Vector2.MoveTowards (transform.position, new Vector2 (spots [5].position.x, spots[5].position.y), speed);
				yield return null;
			}

			while (transform.position.x != spots [6].position.x) {
				//GetComponent<SpriteRenderer> ().sprite = sprites [1];
				transform.position = Vector2.MoveTowards (transform.position, new Vector2 (spots [6].position.x, spots[6].position.y), speed);
				yield return null;

			}

			while (transform.position.x != spots [7].position.x) {
				//GetComponent<SpriteRenderer> ().sprite = sprites [1];
				transform.position = Vector2.MoveTowards (transform.position, new Vector2 (spots [7].position.x, spots[7].position.y), speed);
				yield return null;
			}

			while (transform.position.x != spots [8].position.x) {
				//GetComponent<SpriteRenderer> ().sprite = sprites [1];
				transform.position = Vector2.MoveTowards (transform.position, new Vector2 (spots [8].position.x, spots[8].position.y), speed);
				yield return null;
			}

			yield return new WaitForSeconds (3f);


			//FIRST ATTACK

			while (transform.position.x != spots [0].position.x) {

				transform.position = Vector2.MoveTowards (transform.position, new Vector2 (spots [0].position.x, spots [0].position.y), speed);

				yield return null;

			}
			isShooting = true;
			enemyAnimator.SetBool ("isShooting", isShooting);
			//GetComponent<SpriteRenderer> ().sprite = sprites [0];
			enemyRB.transform.localScale = new Vector2 (-4, 4);	


			yield return new WaitForSeconds (1f);

			int i = 0;
			while (i < 8) {
				//projectile.transform.rotation = (0, 0, 90);
				//bullet = (GameObject)Instantiate (projectile, holes [Random.Range (0, 2)].position, Quaternion.identity);
				bullet = (GameObject)Instantiate (projectile, holes [Random.Range (0, 2)].position, Quaternion.identity);
				bullet.GetComponent<Rigidbody2D> ().velocity = Vector2.left * 7;

				i++;
				yield return new WaitForSeconds (.7f);

			}

			//SECOND ATTACK

			isShooting = false;
			enemyAnimator.SetBool ("isShooting", isShooting);

			while (transform.position.x != spots [2].position.x) {

				//GetComponent<SpriteRenderer> ().sprite = sprites [1];
				transform.position = Vector2.MoveTowards (transform.position, new Vector2 (spots [2].position.x, spots[2].position.y), speed);

				yield return null;

			}
			vortex.SetActive (true);
			//isVortex = true;
			//vortexAnim.SetBool ("isVortex", isVortex);
			//vortexAnim.SetTrigger ("VortexIdle");

			yield return new WaitForSeconds (.3f);

			int o = 0;
			while (o < 25) {

				bullet = (GameObject)Instantiate (projectile2, holes [Random.Range (0, 11)].position, Quaternion.identity);
				bullet3 = (GameObject)Instantiate (projectile6, holes [Random.Range (0, 11)].position, Quaternion.identity);
				bullet4 = (GameObject)Instantiate (projectile4, holes [Random.Range (0, 11)].position, Quaternion.identity);
				bullet5 = (GameObject)Instantiate (projectile5, holes [Random.Range (0, 11)].position, Quaternion.identity);
				//bullet6 = (GameObject)Instantiate (projectile6, holes [Random.Range (0, 11)].position, Quaternion.identity);

				bullet.GetComponent<Rigidbody2D> ().velocity = Vector2.down *  8;
				bullet3.GetComponent<Rigidbody2D> ().velocity = Vector2.down * 8;
				bullet4.GetComponent<Rigidbody2D> ().velocity = Vector2.down * 8;
				bullet5.GetComponent<Rigidbody2D> ().velocity = Vector2.down * 8;
				//bullet6.GetComponent<Rigidbody2D> ().velocity = Vector2.down * 5;

				o++;
				yield return new WaitForSeconds (.3f);

			}
			//isVortex = false;
			//vortexAnim.SetBool ("isVortex", isVortex);
			vortex.SetActive (false);


			//MOVEMENT PHASE TO TAKE ATTACKS

			while (transform.position.x != spots [8].position.x) {
				//GetComponent<SpriteRenderer> ().sprite = sprites [1];
				transform.position = Vector2.MoveTowards (transform.position, new Vector2 (spots [8].position.x, spots[8].position.y), speed);
				yield return null;
			}
			while (transform.position.x != spots [7].position.x) {
				//GetComponent<SpriteRenderer> ().sprite = sprites [1];
				transform.position = Vector2.MoveTowards (transform.position, new Vector2 (spots [7].position.x, spots[7].position.y), speed);
				yield return null;
			}
			while (transform.position.x != spots [6].position.x) {
			//	GetComponent<SpriteRenderer> ().sprite = sprites [1];
				transform.position = Vector2.MoveTowards (transform.position, new Vector2 (spots [6].position.x, spots[6].position.y), speed);
				yield return null;

			}
			while (transform.position.x != spots [5].position.x) {
			//	GetComponent<SpriteRenderer> ().sprite = sprites [1];
				transform.position = Vector2.MoveTowards (transform.position, new Vector2 (spots [5].position.x, spots[5].position.y), speed);
				yield return null;
			}

			while (transform.position.x != spots [4].position.x) {
				//GetComponent<SpriteRenderer> ().sprite = sprites [1];
				transform.position = Vector2.MoveTowards (transform.position, new Vector2 (spots [4].position.x, spots[4].position.y), speed);
				yield return null;
			}
			while (transform.position.x != spots [3].position.x) {
				//GetComponent<SpriteRenderer> ().sprite = sprites [1];
				transform.position = Vector2.MoveTowards (transform.position, new Vector2 (spots [3].position.x, spots[3].position.y), speed);
				yield return null;
			}

		
			yield return new WaitForSeconds (3f);



			//FOURTH ATTACK
		
			enemyRB.transform.localScale = new Vector2 (-4, 4);	

			while (transform.position.x != spots [1].position.x) {
				transform.position = Vector2.MoveTowards (transform.position, new Vector2 (spots [1].position.x, spots [1].position.y), speed);
		
				yield return null;
			}

			isShooting = true;
			enemyAnimator.SetBool ("isShooting", isShooting);
			enemyRB.transform.localScale = new Vector2 (4, 4);	

			yield return new WaitForSeconds (1f);

			int p = 0;
			while (p < 8) {
				//projectile.transform.rotation = (0, 0, 90);
				//bullet = (GameObject)Instantiate (projectile, holes [Random.Range (0, 2)].position, Quaternion.identity);
				bullet = (GameObject)Instantiate (projectile3, holes [Random.Range (0, 2)].position, Quaternion.identity);
				bullet.GetComponent<Rigidbody2D> ().velocity = Vector2.right * 7;

				p++;
				yield return new WaitForSeconds (.7f);

			}

			isShooting = false;
			enemyAnimator.SetBool ("isShooting", isShooting);























	}
}

}






/*Something else I wanted to add, but havent as of yet*/


			/*GetComponent<Rigidbody2D> ().isKinematic = true;
		while (transform.position != spots [2].position)
			transform.position = Vector2.MoveTowards (transform.position, spots[2].position, speed);


		playerPos = player.transform.position;

		yield return new WaitForSeconds (1f);
		GetComponent<Rigidbody2D> ().isKinematic = false;

			while (transform.position.x != playerPos.x) {

				transform.position = Vector2.MoveTowards (transform.position, new Vector2 (playerPos.x, transform.position.y), speed);

				yield return null;


				this.tag = "Untagged";
				GetComponent<SpriteRenderer> ().sprite = sprites [1];
				vulnerable = true; 
				yield return new WaitForSeconds (4);
				this.tag = "Enemy";
				GetComponent<SpriteRenderer> ().sprite = sprites [0];
				vulnerable = false; 

				//THIRD ATTACK
				Transform temp; 
				if (transform.position.x > player.transform.position.x)
					temp = spots [1];
				else
					temp = spots [0];

				while (transform.position.x != spots [0].position.x) {

					transform.position = Vector2.MoveTowards (transform.position, new Vector2 (temp.position.x, transform.position.y), speed);

					yield return null;

				}

			}
	}
}

}*/