using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartMap : MonoBehaviour {
	
	public bool EnableMap = false; 
	private float counter = 0.0f;
	public Text TextInWindow;
	public Canvas MainCanvas;
	public int TimeToFinish = 60;

	void Update(){
		if (Input.GetKey (KeyCode.R)) {
			EnableMap = true;
		}

		if (EnableMap) {
			counter += Time.deltaTime;
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

		}
	}
}