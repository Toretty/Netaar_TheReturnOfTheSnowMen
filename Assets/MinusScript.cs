using UnityEngine;
using System.Collections;

public class MinusScript : MonoBehaviour {

	private string SetOpinionName;
	public GameObject PresentObject;

	void Awake()
	{
				if (GetComponentInParent<PlayerSync>().tag == "Player1") {

			SetOpinionName = "Player2";
			print ("Opponent is 2");

				} else if (GetComponentInParent<PlayerSync>().tag == "Player2") {
		
			SetOpinionName = "Player1";
			print ("Opponent is 1");

		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if(col.gameObject.tag == SetOpinionName){
			print ("Loose Point!");
			GetComponentInParent<PresentsCounter> ().PresentCount--;

		}

	}
}
