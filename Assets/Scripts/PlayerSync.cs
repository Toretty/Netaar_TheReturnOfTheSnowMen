using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]

public class PlayerSync : MonoBehaviour {
	
	public float LeftBoarder;
	public float RightBoarder;
	private Vector3 newPosition;
	public PolygonCollider2D LeftCollider;
	public PolygonCollider2D RightCollider;
	public Animator anim;
	private float accSpeed = 100.0f;
	private float jumpHeight = 40.0f;
	private bool landedAfterJump = false;
	private GameObject FindCamera;
	public bool facingRight = true;
	public bool activateControls = false;
	public bool disableControls = false;

	private Vector3 PushEntireMap = new Vector3 (50.0f, 0.0f, 0.0f);
	
	void Start () {
		LeftCollider.enabled = false;
		RightCollider.enabled = true;
	}

	void Update () {

		if(!activateControls){
			if(networkView.isMine && gameObject.tag == "Player1"){
				FindCamera = GameObject.Find ("Main Camera(Clone)");
				FindCamera.camera.orthographicSize = 11;
				FindCamera.transform.position = newPosition + PushEntireMap + new Vector3 (14.0f, 4.0f, -10.0f);
			} else if(networkView.isMine && gameObject.tag == "Player2"){
				FindCamera = GameObject.Find ("Main Camera_2(Clone)");
				FindCamera.camera.orthographicSize = 11;	
				FindCamera.transform.position = newPosition + PushEntireMap + new Vector3 (14.0f, 4.0f, -10.0f);
			}


			activateControls = GameObject.FindGameObjectWithTag("StartMapManager").GetComponent<StartMap>().EnableMap;
		}

		if(networkView.isMine && activateControls && !disableControls){
			if (FindCamera == null) {
				if(gameObject.tag == "Player1"){
					FindCamera = GameObject.Find ("Main Camera(Clone)");
				}else{
					FindCamera = GameObject.Find ("Main Camera_2(Clone)");
				}

			} else {
				if (transform.position.x > LeftBoarder && transform.position.x < RightBoarder) {
					newPosition = transform.position;
				} else {
					newPosition.y = transform.position.y;
				}

				FindCamera.camera.orthographicSize = 5;
				FindCamera.transform.position = newPosition + new Vector3 (0.0f, 2.0f, -10.0f);


				if (Input.GetKeyDown (KeyCode.Space)) 
				{
					if(!landedAfterJump){
						anim.SetBool("Jump", true);
						rigidbody2D.AddForce(new Vector2(0.0f, jumpHeight), ForceMode2D.Impulse);
						landedAfterJump = true;
					}
				}
				
				////////////////////////////////////////////////////////////////////////////////////////
				////// WALK RIGHT //////////////////////////////////////////////////////////////////////
				////////////////////////////////////////////////////////////////////////////////////////
				
				if (Input.GetKey (KeyCode.D)) 
				{	
					anim.SetInteger ("Direction", 2);
					LeftCollider.enabled = false;
					RightCollider.enabled = true;
					facingRight = true;
					rigidbody2D.AddForce(new Vector2(accSpeed, 0.0f));
				}	
				
				////////////////////////////////////////////////////////////////////////////////////////
				////// WALK LEFT ///////////////////////////////////////////////////////////////////////
				////////////////////////////////////////////////////////////////////////////////////////
				
				if (Input.GetKey (KeyCode.A)) 
				{
					anim.SetInteger ("Direction", 1);
					LeftCollider.enabled = true;
					RightCollider.enabled = false;
					facingRight = false;
					rigidbody2D.AddForce(new Vector2(-accSpeed, 0.0f));
				}	
			}
		}
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		anim.SetBool("Jump", false);
		landedAfterJump = false;
	}
	
}
