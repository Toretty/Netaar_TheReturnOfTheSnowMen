using UnityEngine;
using System.Collections;

[RequireComponent (typeof (NetworkView))]

public class PlayerNetworkSync : MonoBehaviour {
	
	private Vector3 EndPosition = new Vector3(0.0f,0.0f,0.0f); 
	private Vector3 StartPosition = new Vector3(0.0f,0.0f,0.0f); 

	private float syncDelay = 0.0f;
	private float syncTime = 0.0f;
	private float LastSyncTime = 0.0f;

	void OnSerializeNetworkView (BitStream stream, NetworkMessageInfo info){

		Vector3 syncPosition = Vector3.zero;
		Vector3 syncVelocity = Vector3.zero;
		bool syncFacing = true;
		// If this script is the one sending something.
		if (stream.isWriting) {
			// Make a reference of the position right now.
			syncPosition = transform.position;
			stream.Serialize (ref syncPosition);
			
			syncVelocity = rigidbody2D.velocity;
			stream.Serialize(ref syncVelocity);

			syncFacing = GetComponent<PlayerSync> ().facingRight;
			stream.Serialize(ref syncFacing);

		} 
		// If the script is the one receiving.
		else {
			
			// Receive the message from the sender. 
			stream.Serialize (ref syncPosition);
			stream.Serialize (ref syncVelocity);
			stream.Serialize (ref syncFacing);

			syncTime = 0.0f;
			syncDelay = Time.time - LastSyncTime;
			LastSyncTime = Time.time;

			if(syncFacing == true){
				GetComponent<PlayerSync>().anim.SetInteger ("Direction", 2);
			} else{
				GetComponent<PlayerSync>().anim.SetInteger ("Direction", 1);
			}

			StartPosition = transform.position;
			EndPosition = syncPosition;
			EndPosition = syncPosition + syncVelocity * syncDelay;
		}
	}

	void Update(){
		if(!networkView.isMine) {
			SyncMovement();		
		}
	}

	private void SyncMovement(){

		syncTime += Time.deltaTime;
		transform.position = Vector3.Lerp (StartPosition, EndPosition, syncTime / syncDelay);
	}

}
