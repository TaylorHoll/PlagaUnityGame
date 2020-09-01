using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	//Script Calls
	private PlayerController player;
	private EnemyMovementController enemy;

    
	//Health
	public float maxHealth;
    public float currHealth;

	//Animator
	private Animator anim;

	//Invicibility
	public bool invincible;
	public float invincibilityLength;
	private float invincibilityCounter; 

	//Audio
	public AudioClip enemySound;
	public AudioSource soundSourceTwo;
	//public AudioClip dobWhine;
	//public AudioSource soundSourceThree;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        currHealth = maxHealth;
		anim = GetComponent<Animator> ();
		soundSourceTwo.clip = enemySound;
		//soundSourceThree.clip = dobWhine;
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter2D(Collider2D col)
	{

		if (col.CompareTag("Player") && !player.invincible)
		{
			player.Damage (1);
			player.Knockback ();
		}
	}

    public void addDamage(float damage)
    {
		anim.SetTrigger ("isHit");
		soundSourceTwo.Play();
        currHealth -= damage;
	
		if (currHealth <= 0) {
			soundSourceTwo.Play ();
			anim.SetTrigger ("isDead");
			makeDead ();
		}
    }

    void makeDead ()
    {
		
		Destroy (gameObject, .8f);
    }
}
