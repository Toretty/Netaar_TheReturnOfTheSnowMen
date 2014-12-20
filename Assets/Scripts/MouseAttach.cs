using UnityEngine;
using System.Collections;

public class MouseAttach : MonoBehaviour {
	
	public GameObject Pointer;
	public GameObject ShootingLine;
	public float ShotPower = 50;

	public GameObject Snowballs;

	private Vector3 PushIn = new Vector3(0.0f,0.0f,1.0f);

	public Transform start;
	private Vector3 end;
	private Vector3 endpoint;
	
	private float factor = 1.0f;
	private float range = 2.0f;
	private Vector3 dotPoint = new Vector3(0.0f, 0.0f, 0.0f);
	
	private Vector2 directionOutput;

	private bool loadingshot = false;

	private float strength = 0.2f;

	public AudioSource ChargeShotSound;
	
	void Update () {


		if (Input.GetMouseButton (0)) {

			if(!ChargeShotSound.isPlaying){
				ChargeShotSound.Play();
			}

			if(strength<1.0f){
				strength += Time.deltaTime;
			}


			loadingshot = true;
			Pointer.gameObject.renderer.enabled = true;
			ShootingLine.gameObject.particleSystem.enableEmission = true;
			RaycastHit2D hitInfo = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			if (Physics2D.Raycast (new Vector2 (camera.ScreenToWorldPoint (Input.mousePosition).x, camera.ScreenToWorldPoint (Input.mousePosition).y), Vector2.zero)) {
					Debug.DrawLine (transform.position, Camera.main.ScreenToWorldPoint (Input.mousePosition));
			}

			endpoint = camera.ScreenToWorldPoint (Input.mousePosition) + PushIn;
			SetPos (start.position + PushIn, endpoint + PushIn);

			Pointer.transform.position = dotPoint + PushIn;
			Pointer.transform.localScale = new Vector3(strength,strength,strength);
		} else {
			Pointer.gameObject.renderer.enabled = false;
			ShootingLine.gameObject.particleSystem.enableEmission = false;
			ChargeShotSound.Stop();

			if(loadingshot == true){
				loadingshot = false;
				GameObject newSnowball = Instantiate(Snowballs, Pointer.transform.position, transform.rotation) as GameObject;
				newSnowball.transform.localScale = new Vector2(strength,strength);
				newSnowball.rigidbody2D.mass = strength*2;
				newSnowball.rigidbody2D.AddForce (ShootingLine.transform.up * ShotPower, ForceMode2D.Impulse);
			}
			strength = 0.2f;
		}

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
