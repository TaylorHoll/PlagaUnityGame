using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeToScene_1 : MonoBehaviour {

		public float delay = 440;
		void Start()
		{
			StartCoroutine(LoadLevelAfterDelay(delay));
		}

		IEnumerator LoadLevelAfterDelay(float delay)
		{
			yield return new WaitForSeconds(delay);
			SceneManager.LoadScene("Scene_1");
		}
	}