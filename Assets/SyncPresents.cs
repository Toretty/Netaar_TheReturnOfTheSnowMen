using UnityEngine;
using System.Collections;

[RequireComponent (typeof (NetworkView))]

public class SyncPresents : MonoBehaviour {
	
	private Vector3 EndPosition; 
	private Vector3 StartPosition; 
	
	private float syncDelay = 0.0f;
	private float syncTime = 0.0f;
	private float LastSyncTime = 0.0f;
	
	void OnSerializeNetworkView (BitStream stream, NetworkMessageInfo info){
		
		Vector3 syncPosition = Vector3.zero;
		float syncMass = 0.0f;
		Vector3 syncScale = Vector3.zero;
		// If this script is the one sending something.
		if (stream.isWriting) {
			// Make a reference of the position right now.
			syncPosition = transform.position;
			syncMass = rigidbody2D.mass;
			syncScale = transform.localScale;
			
			stream.Serialize (ref syncPosition);
			stream.Serialize (ref syncMass);
			stream.Serialize (ref syncScale);
		} 
		// If the script is the one receiving.
		else {
			
			// Receive the message from the sender. 
			stream.Serialize (ref syncPosition);
			stream.Serialize (ref syncMass);
			stream.Serialize (ref syncScale);
			
			syncTime = 0.0f;
			syncDelay = Time.time - LastSyncTime;
			LastSyncTime = Time.time;
			
			rigidbody2D.mass = syncMass;
			transform.localScale = syncScale;
			
			StartPosition = transform.position;
			EndPosition = syncPosition;
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
