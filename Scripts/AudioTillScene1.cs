using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioTillScene1 : MonoBehaviour {

	public AudioSource soundSource;
	public AudioClip IntroAudio;


	// Use this for initialization
	void Start () {
		soundSource.clip = IntroAudio;
		soundSource.Play ();

		Scene currentScene = SceneManager.GetActiveScene ();
		string sceneName = currentScene.name;

		if (sceneName == "Scene_1") {
			Destroy (this.gameObject);
		}

	}
	
	// Update is called once per frame
	void Update () {


		
	}

	void Awake()
	{
		if (FindObjectsOfType (typeof(AudioTillScene1)).Length > 1) {
			Destroy (gameObject);
			return;
		}


		GameObject[] objs = GameObject.FindGameObjectsWithTag ("IntroAudio");
		if (objs.Length > 1)
			Destroy (this.gameObject);

		DontDestroyOnLoad (this.gameObject);
	}
}
