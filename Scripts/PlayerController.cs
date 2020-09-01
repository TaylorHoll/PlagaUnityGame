using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	private float moveVelocity;
	public Rigidbody2D myRigidbody;
	private PlayerAttack playerAtt;

	//Grounded Check
	public Transform groundCheck;
	public float groundCheckRadius; 
	public LayerMask whatIsGround;
	public bool isGrounded;

	//knockback
	public float knockbackForce = 5f;
	public float knockbackLength = 0.2f;
	private float knockbackCount;

	//Animations
	private Animator myAnime;
	private Animator anim;
	public AnimationClip playerDeath;

	//temp
	//private bool isDamaged;

	//Audio 
	public AudioClip playerHit;
	public AudioSource soundSource;
	//public AudioClip playerDead;
	//public AudioSource soundSource2;

	//Player Health
	public int currHealth;
	public int maxHealth = 100;

	//Variables needed for jumping
	public float jumpShortSpeed = 3f;   // Velocity for the lowest jump
	public float jumpSpeed = 35f;          // Velocity for the highest jump
	bool jump = false;
	bool jumpCancel = false;

	//Invicibility
	public bool invincible;
	public float invincibilityLength;
	private float invincibilityCounter; 




	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D>();
		myAnime = GetComponent<Animator> ();
		anim = gameObject.GetComponent<Animator> ();
		playerAtt = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerAttack> ();
		soundSource.clip = playerHit;
		//soundSource2.clip = playerDead;



        currHealth = maxHealth;

		//respawnPosition = transform.position; //default spawning before checkpoints
		//theLevelManager = FindObjectOfType<LevelManager> ();

	}

	// Update is called once per frame
	void Update () {

		//Check if player is off ground to not jump again
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);


		if (knockbackCount <= 0) 
		{

			//Control the movement of character
			if ((Input.GetAxisRaw ("Horizontal") > 0f))  {
				myRigidbody.velocity = new Vector3 (moveSpeed, myRigidbody.velocity.y, 0f);
				if (!playerAtt.attacking) {
					transform.localScale = new Vector3 (1f, 1f, 1f); //Flip player when heading right
				}
			} else if ((Input.GetAxisRaw ("Horizontal") < 0f)) {
				myRigidbody.velocity = new Vector3 (-moveSpeed, myRigidbody.velocity.y, 0f);
				if (!playerAtt.attacking) {
				transform.localScale = new Vector3 (-1f, 1f, 1f); 	//Flip player when heading left
				}
				} else { 
				myRigidbody.velocity = new Vector3 (0f, myRigidbody.velocity.y, 0f);
			}

			if (invincibilityCounter > 0) {
				invincibilityCounter -= Time.deltaTime;
			}

			if (invincibilityCounter <= 0) {
				invincible = false;
			}

			//Make jump available to player if isGrounded is true
			if (Input.GetButtonDown ("Jump") && isGrounded)   // Player starts pressing the button
			jump = true;
			if (Input.GetButtonUp ("Jump") && !isGrounded)     // Player stops pressing the button
			jumpCancel = true;



		}

		if (knockbackCount > 0) 
		{
			knockbackCount -= Time.deltaTime;
			//knockbackForce = 5f;
			//knockbackLength = 0.2f;
			if(transform.localScale.x >0)
				myRigidbody.velocity = new Vector3(-knockbackForce, knockbackForce, 0f);
			else 
				myRigidbody.velocity = new Vector3(knockbackForce, knockbackForce, 0f);
		}

        //Health if functions 
        if (currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }

        if (currHealth <= 0)
        {
		//	anim.SetTrigger ("isDead");
			//soundSource2.Play ();
			//soundSource2.clip = playerDead;
			//soundSource2.Play ();
			StartCoroutine("makeDead");
			//makeDead ();
			//SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);

			//Die();
        }

		//Set Animations made in Unity to whoever holds this script
		myAnime.SetFloat ("Speed", Mathf.Abs (myRigidbody.velocity.x));
		myAnime.SetBool ("Grounded", isGrounded);
		//isDamaged = false;
		//myAnime.SetBool("isDamaged", isDamaged);

	}

	void FixedUpdate()
	{
		// Normal jump (full speed)
		if (jump)
		{
			myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpSpeed);
			jump = false;
		}
		// Cancel the jump when the button is no longer pressed
		if (jumpCancel)
		{
			if (myRigidbody.velocity.y > jumpShortSpeed)
				myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpShortSpeed);
			jumpCancel = false;
		}
	}

    public void Damage(int dmg)
    {
		//isDamaged = true;
        currHealth -= dmg;
		myAnime.SetTrigger("isDamaged");

		soundSource.Play ();
		//isDamaged = false;
		//myAnime.SetBool("isDamaged", isDamaged);

    }

	public void Knockback()
	{
		knockbackCount = knockbackLength;
		invincibilityCounter = invincibilityLength;
		invincible = true;

	}

	/*void makeDead(){
		anim.SetTrigger ("isDead");
		//Destroy (gameObject, playerDeath.length);
		Destroy (gameObject, 2.5f);
		yield return new WaitForSeconds (1.0f);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);

	

	}
*/
	IEnumerator makeDead(){

		//yield return new WaitForSeconds (10f);
	//	soundSource.clip = playerDead;
	//	soundSource.Play ();
		anim.SetTrigger ("isDead");

		//Destroy (gameObject, playerDeath.length);
		//Destroy (gameObject, 2.5f);
		yield return new WaitForSeconds (4f);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);



	}

    //void Die()
    //{
        
    //}


}


	/*void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "KillPlane") {
			//gameObject.SetActive (false); this will deactivate the player 

			//transform.position = respawnPosition; //sends player back to checkpoint if dies

			theLevelManager.Respawn ();
		}

		if (other.tag == "Checkpoint") {
			respawnPosition = other.transform.position;
		}
	}*/


