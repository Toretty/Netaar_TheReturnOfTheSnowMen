using UnityEngine;
using System.Collections;

public class AreaDetection : MonoBehaviour {

	public bool isPlayer1 = false;
	public bool isPlayer2 = false;
	public Animator pointAnim;

	public int Presents = 0;
	private int ChangeNumber = 10;

	void Update(){
		if(Presents != ChangeNumber){

			pointAnim.SetInteger("Points", Presents);
			ChangeNumber = Presents;
		}

	}

	void OnTriggerEnter2D (Collider2D col){
		if(col.gameObject.tag == "Present"){
			Presents++;
		}

		if(col.gameObject.tag == "Player1" || col.gameObject.tag == "Player2"){
			Presents = 0;
		}
	}

	void OnTriggerStay2D (Collider2D col){
		
		if(col.gameObject.tag == "Player1" || col.gameObject.tag == "Player2"){
			Presents = 0;
		}
	}

	void OnTriggerExit2D (Collider2D col){
		if (col.gameObject.tag == "Present") {
				Presents--;
		}

	}

}
