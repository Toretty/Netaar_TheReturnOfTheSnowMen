using UnityEngine;
using System.Collections;

public class PresentScript : MonoBehaviour {
	
	private GameObject PointManager;

	private bool inArea = false;
	private Vector3 AreaCenter;

	void Update(){
		if(inArea){
			transform.position = Vector3.Lerp(transform.position, AreaCenter, 5*Time.deltaTime);
		}

	}

	void OnCollisionEnter2D (Collision2D col) 
	{
		if(col.gameObject.tag == "Player1"){

			PointManager = GameObject.Find("Main Camera(Clone)");
			if(PointManager != null){
				PointManager.GetComponent<PointManager>().Points ++;

				if(col.gameObject.networkView.isMine){
					Network.Destroy(this.gameObject);
				}

			}

		}
		if(col.gameObject.tag == "Player2"){
			
			PointManager = GameObject.Find("Main Camera_2(Clone)");
			if(PointManager != null){
				PointManager.GetComponent<PointManager>().Points ++;

				if(col.gameObject.networkView.isMine){
					Network.Destroy(this.gameObject);
				}
			}
			
		}

	}

	void OnTriggerEnter2D (Collider2D col){
		if(col.gameObject.tag == "AreaDetection"){

			AreaCenter = col.gameObject.transform.position;
			inArea = true;
		}
	}
}
