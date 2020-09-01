using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

	public int dmg = 10;



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
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.tag == "Enemy") {
			
			EnemyHealth hurtEnemy = other.gameObject.GetComponent<EnemyHealth> ();
			hurtEnemy.addDamage (dmg);

		
		} else if (other.tag == "Bat") {

			batAI hurtBat = other.gameObject.GetComponent<batAI> ();
			hurtBat.addDamage (dmg);

		} else if (other.tag == "Skeleton") {
			
			Skeleton hurtEnem = other.gameObject.GetComponent<Skeleton> ();
			hurtEnem.addDamage (dmg);

		} else if (other.tag == "Andromailus") {

			BossGHead hurtBoss = other.gameObject.GetComponent<BossGHead> ();
			hurtBoss.addDamage (dmg);

		}
    }
}

