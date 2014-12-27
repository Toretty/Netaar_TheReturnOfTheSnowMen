using UnityEngine;
using System.Collections;

public class AddForceSnowball : MonoBehaviour {

	public bool ActivateStuck = false;

	private float destroyIn = 0.0f;
	public AudioSource snowballHit;

	void Update(){
		destroyIn = destroyIn + Time.deltaTime;
		if(destroyIn > 5.0){
			if(networkView.isMine){
				Network.Destroy(this.gameObject);
			}
		}
	}

	// Use this for initialization
	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.tag == "Player1" || col.gameObject.tag == "Player2") {
				if (col.gameObject.networkView.isMine) {
						Physics2D.IgnoreCollision (col.collider, collider2D);
				} 
		}

		if (col.gameObject.tag == "Obstacle") {
				snowballHit.Play ();
				if (ActivateStuck) {
						rigidbody2D.isKinematic = true;
				}
		}

	}

	void OnCollusionStay2D (Collision2D col){
		if (col.gameObject.tag == "Player1" || col.gameObject.tag == "Player2") {
			if (col.gameObject.networkView.isMine) {
				Physics2D.IgnoreCollision (col.collider, collider2D);
			} 
		}
	}
}
