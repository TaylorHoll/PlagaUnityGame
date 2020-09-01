using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatTree : MonoBehaviour {

	public AudioClip AndroBelow;
	public AudioSource soundSource;

	// Use this for initialization
	void Start () {
		soundSource.clip = AndroBelow;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D other) {
		soundSource.Play ();

	}
}
