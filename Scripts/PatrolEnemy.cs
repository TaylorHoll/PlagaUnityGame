using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour {

	private Skeleton skel;

	// Use this for initialization
	void Start () {
		skel = GameObject.FindGameObjectWithTag("Skeleton").GetComponent<Skeleton>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.tag == "PatrolBlock") {
			skel.enemySpeed *= -1;
			skel.enemyRB.transform.localScale = new Vector3 (skel.enemySpeed * -1f, 1f, 1f);  
			//enemyRB.transform.localScale 

		}
	}


}


