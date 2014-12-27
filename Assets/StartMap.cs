using UnityEngine;
using System.Collections;

public class StartMap : MonoBehaviour {
	
	public bool EnableMap = false; 

	void Update(){
		if (Input.GetKey (KeyCode.R)) {
			EnableMap = true;
		}
	}

	void OnSerializeNetworkView (BitStream stream, NetworkMessageInfo info){

		bool syncEnable = false;
		if (stream.isWriting) {

			syncEnable = EnableMap;
			stream.Serialize (ref syncEnable);
		} 

		else {
			stream.Serialize (ref syncEnable);
			EnableMap = syncEnable;
		}
	}
}