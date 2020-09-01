using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

   

    private PlayerController player;



	// Use this for initialization
	void Start () {
        //enemyAnimator

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
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

      
    }

