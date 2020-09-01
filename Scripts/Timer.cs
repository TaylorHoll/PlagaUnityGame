using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer : MonoBehaviour {

	private int timecount = 0;
	public float startTime;
	public float endTime;
	public String textTime;
	public GUISkin guiSkin;
	public int xpos;
	public int ypos;
	public int width;
	public int height;

	void Start ()
	{
		//this will send the Counter function every second
		InvokeRepeating("Counter", 1, 1);
	}

	void Awake()
	{
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("Timer");
		if (objs.Length > 1)
			Destroy (this.gameObject);

		DontDestroyOnLoad (this.gameObject);
	}

	void Counter ()
	{
		//This will add 1 second to the timecount variable, so everysecond it will add 1
		timecount++;
	}

	void OnGUI ()
	{

		var guiTime = Time.time - startTime; 

		float minutes = guiTime / 60;
		float seconds = guiTime % 60;
		float fraction = (guiTime * 100) % 100;

		textTime = String.Format ("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction); 

		GUI.skin = guiSkin;
		GUI.Label(new Rect((Screen.width) - xpos, ypos, width, height), "TIME: " + textTime);



		//this displays the time
		// GUI.Label(new Rect(10, 10, 150, 50), "Time: " +timecount);
	}

}
