using UnityEngine;
using System.Collections;

public class PresentScript : MonoBehaviour {


	void OnCollisionEnter2D (Collision2D col) 
	{
		if(col.gameObject.tag == "Player1"){

			Instantiate(this.gameObject, new Vector2(0.0f,6.0f), Quaternion.identity);

			Destroy(this.gameObject);
		}
	}
}
