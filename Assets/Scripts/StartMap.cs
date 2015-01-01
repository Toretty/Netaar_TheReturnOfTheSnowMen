using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartMap : MonoBehaviour {
	
	public bool EnableMap = false; 
	private float counter = 0.0f;
	public Text TextInWindow;
	public Canvas MainCanvas;
	public int TimeToFinish = 60;

	private GameObject Player1Object;
	private GameObject Player2Object;

	void Update(){
		if (Input.GetKey (KeyCode.R) && Network.isServer) {
			EnableMap = true;
		}

		if (EnableMap) {
			counter += Time.deltaTime;
		} 

		if(EnableMap && Player1Object == null){
			Player1Object = GameObject.FindGameObjectWithTag("Player1");
		}
		if(EnableMap && Player2Object == null){
			Player2Object = GameObject.FindGameObjectWithTag("Player2");
		}
	}

	void OnSerializeNetworkView (BitStream stream, NetworkMessageInfo info){

		int syncEnable = 0;
		if (stream.isWriting) {
			if(EnableMap == false){
				syncEnable = 1;
			}
			if(EnableMap == true){
				syncEnable = 2;
			}

			stream.Serialize (ref syncEnable);
		} 

		else {
			stream.Serialize (ref syncEnable);
			if(syncEnable == 1){
				EnableMap = false;
			}
			if(syncEnable == 2){
				EnableMap = true;
			}
		}
	}

	void OnGUI(){

		if(EnableMap){
			int timeOnCounter = (int)(TimeToFinish-counter);
			if(timeOnCounter > 0){
				if (GUI.Button (new Rect (10, Screen.height-30, Screen.width/6, Screen.height/15 ), "TIME LEFT: "+timeOnCounter)) {
					
				}
			}
		}

		if(counter>TimeToFinish){

			MainCanvas.enabled = true;

			int player1 = GameObject.Find("AreaL").GetComponent<AreaDetection>().Presents;
			int player2 = GameObject.Find("AreaR").GetComponent<AreaDetection>().Presents;

			if(player1 > player2){
				TextInWindow.text = "Player 1 Wins!";
			}

			if(player2 > player1){
				TextInWindow.text = "Player 2 Wins!";
			}

			if(player2 == player1){
				TextInWindow.text = "ITS A TIE!";
			}
			Player1Object.GetComponent<PlayerSync>().disableControls = true;
			Player2Object.GetComponent<PlayerSync>().disableControls = true;


		}
	}
}