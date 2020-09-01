using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class HUD : MonoBehaviour {

    public Sprite[] HealthBar;

    public Image HealthUI;

    private PlayerController player;

	void Start () {

		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
}


	void Update () {
	
		HealthUI.sprite = HealthBar[player.currHealth];
	}
}