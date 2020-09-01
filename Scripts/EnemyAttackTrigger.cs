using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackTrigger : MonoBehaviour {

		public int dmg = 1;
		private PlayerController player;



		/*void OnTriggerEnter2D(Collider2D col)
	{
		if (col.isTrigger != true && col.CompareTag ("Enemy")) {
			col.SendMessageUpwards ("Damage", dmg);
		}
	}
    */

		//put this into the enemy script for it to take damage
		//public void Damage(int damage)
		//{
		//curHealth -= damage;
		//gameObject.GetComponent<Animation>.Play("Player_RedFlash")
		//}

		//put in enemy script to destroy when killed
		//if (curHealth <= 0)
		//{
		//Destroy(gameObject);
		//}

		// Use this for initialization
		void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		}

		// Update is called once per frame
		void Update () {

		}


		void OnTriggerEnter2D(Collider2D other)
		{
		if (other.CompareTag("Player") && !player.invincible) {
				player.Damage(dmg);
				player.Knockback ();
		
			}

		}
	}
