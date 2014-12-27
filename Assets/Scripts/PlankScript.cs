using UnityEngine;
using System.Collections;

public class PlankScript : MonoBehaviour {

	public Animator anim;

	public float Health;
	public float Damaged;
	private float damageCounter = 0.0f;

	void OnCollisionEnter2D (Collision2D col)
	{
		if(col.gameObject.tag == "SnowBall"){

			if(damageCounter > Damaged){
				anim.SetBool("broken", true);

				if(damageCounter > Health){
					if(networkView.isMine){
						Network.Destroy(this.gameObject);
					}
				}
			}
			damageCounter += col.gameObject.rigidbody2D.mass;
		}

	}
}
