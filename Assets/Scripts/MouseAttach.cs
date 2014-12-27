using UnityEngine;
using System.Collections;

public class MouseAttach : MonoBehaviour {

	private GameObject SnowballPointer;
	private GameObject PresentPointer;
	private GameObject BombPointer;
	private GameObject Pointer;
	private GameObject ShootingLine;
	public float ShotPower = 50;

	public GameObject SnowBallObject;
	public GameObject PresentObject;
	public GameObject BombObject;
	private GameObject ShootingObject;

	private Vector3 PushIn = new Vector3(0.0f,0.0f,1.0f);

	private Transform start;
	private Vector3 end;
	private Vector3 endpoint;
	
	private float factor = 1.0f;
	private float range = 2.0f;
	private Vector3 dotPoint = new Vector3(0.0f, 0.0f, 0.0f);
	
	private Vector2 directionOutput;

	private bool loadingshot = false;

	private float strength = 0.8f;

	public AudioSource ChargeShotSound;
	

	void Start(){
		PresentPointer = GameObject.Find ("PresentPoint");
		SnowballPointer = GameObject.Find ("SnowBallPoint");
		ShootingLine = GameObject.Find("ShootingLineParticles");
	}

	void Update () {

		if (start != null) {

			if (Input.GetMouseButton (1) || Input.GetMouseButton (0)) {


					if (Input.GetMouseButton (0)) 
					{		
							Pointer = SnowballPointer;	
							ShootingObject = SnowBallObject; 	
					} else if (Input.GetMouseButton (1)) 
					{
							Pointer = PresentPointer;	
							ShootingObject = PresentObject;		
					} else 
					{								
							Pointer = SnowballPointer;	
							ShootingObject = BombObject;		
					}
	
					RayCastSettings ();
	
					if (strength < 1.0f) {
							strength += Time.deltaTime;
					}
	
					RayCastSettings ();
	
					SetLine ();
			}

			if (Input.GetMouseButtonUp (0) || Input.GetMouseButtonUp (1)) {
					ResetLineAndHide ();
			}
		} else {
			if(GameObject.Find("NetworkManager").GetComponent<NetworkscriptContentManager>().MapSpawned){
				if(Network.isServer){
					start = GameObject.Find ("PlayerObject(Clone)").transform;
					print ("Pointing!");
				}
				else{
					start = GameObject.Find ("PlayerObject2(Clone)").transform;
					print ("Pointing!");
				}

			}

		}

	}

	void SetLine(){
		endpoint = camera.ScreenToWorldPoint (Input.mousePosition) + PushIn;
		SetPos (start.position + PushIn, endpoint + PushIn);

		if(Pointer == PresentPointer && GetComponent<PointManager>().Points < 1){

		}else{
			Pointer.gameObject.renderer.enabled = true;
			Pointer.transform.position = dotPoint + PushIn;
			Pointer.transform.localScale = new Vector3(strength,strength,strength);
		}

	}

	void RayCastSettings(){
		loadingshot = true;
		ShootingLine.gameObject.particleSystem.enableEmission = true;
		if (Physics2D.Raycast (new Vector2 (camera.ScreenToWorldPoint (Input.mousePosition).x, camera.ScreenToWorldPoint (Input.mousePosition).y), Vector2.zero)) {
			Debug.DrawLine (transform.position, Camera.main.ScreenToWorldPoint (Input.mousePosition));
		}
	}

	void ResetLineAndHide(){
		Pointer.gameObject.renderer.enabled = false;
		ShootingLine.gameObject.particleSystem.enableEmission = false;

		if(loadingshot == true){
			loadingshot = false;

			if(Pointer == PresentPointer){
				if(GetComponent<PointManager>().Points > 0)
				{
					GetComponent<PointManager>().Points --;
					GameObject newSnowball = Network.Instantiate(ShootingObject, Pointer.transform.position, transform.rotation,0) as GameObject;
					newSnowball.transform.localScale = new Vector2(strength,strength);
					newSnowball.rigidbody2D.mass = strength*2;
					newSnowball.rigidbody2D.AddForce (ShootingLine.transform.up * ShotPower, ForceMode2D.Impulse);
				}

			} else{
				GameObject newSnowball = Network.Instantiate(ShootingObject, Pointer.transform.position, transform.rotation,0) as GameObject;
				newSnowball.renderer.enabled = true;
				newSnowball.transform.localScale = new Vector2(strength,strength);
				newSnowball.rigidbody2D.mass = strength*2;
				newSnowball.rigidbody2D.AddForce (ShootingLine.transform.up * ShotPower, ForceMode2D.Impulse);
			}


		}
		strength = 0.8f;
	}
	
	void SetPos(Vector3 start, Vector3 end) {
		Vector3 dir = end - start;
		Vector3 scale = ShootingLine.transform.localScale;

		if(dir.magnitude < range){;
			Vector3 mid = (dir) / 2.0f + start;
			ShootingLine.transform.position = mid;
			ShootingLine.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
			scale.y = dir.magnitude * factor;
			ShootingLine.transform.localScale = scale;
			dotPoint = endpoint;
			directionOutput = mid;
		}else{
			Vector3 mid = (dir.normalized) * range/2 + start;
			ShootingLine.transform.position = mid;
			ShootingLine.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
			scale.y = range;
			ShootingLine.transform.localScale = scale;
			dotPoint = (dir.normalized) * range + start;
			directionOutput = mid;
		}

	}
}
