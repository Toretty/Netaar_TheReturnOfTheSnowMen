using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]

public class PlayerSync : MonoBehaviour {

	public PolygonCollider2D LeftCollider;
	public PolygonCollider2D RightCollider;


	public Animator anim;

	private float maxSpeed = 13.0f; 
	private float accSpeed = 100.0f;
	private float jumpHeight = 40.0f;
	private bool landedAfterJump = false;	

	private GameObject FindCamera;

	// Use this for initialization
	void Start () {
		FindCamera = GameObject.Find("Main Camera");

		LeftCollider.enabled = false;
		RightCollider.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {

		FindCamera.transform.position = transform.position + new Vector3 (0.0f, 2.0f, -10.0f);

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
//			if(rigidbody2D.velocity.x < maxSpeed)
//			{
				print ("Going Right");
				rigidbody2D.AddForce(new Vector2(accSpeed, 0.0f));
//			}
		}	

		////////////////////////////////////////////////////////////////////////////////////////
		////// WALK LEFT ///////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////

		if (Input.GetKey (KeyCode.A)) 
		{
			anim.SetInteger ("Direction", 1);
			LeftCollider.enabled = true;
			RightCollider.enabled = false;
//			if(rigidbody2D.velocity.x > -maxSpeed)
//			{
				print ("Going Left");
				rigidbody2D.AddForce(new Vector2(-accSpeed, 0.0f));
//			}
		}
	}

	void OnCollisionEnter2D (Collision2D col)
	{
			anim.SetBool("Jump", false);
			landedAfterJump = false;
	}
}
