using UnityEngine;
using System.Collections;

public class RandomMachine : MonoBehaviour {

	public Animator anim;

	public GameObject WhereToSpawn;
	public GameObject[] SpawnObjects;

	private bool ActivateStatus = false;
	private bool Spawn = false;

	private float resetTimer = 0.0f;

	void OnTriggerEnter2D (Collider2D col){

		if(!ActivateStatus){
			ActivateStatus = true;
			if(col.gameObject.tag == "Player1" || col.gameObject.tag == "Player2"){
				anim.SetBool("Activated", true);
				Spawn = true;
				
			}
		}
	}

	void Update(){
		if(Spawn){
			Spawn = false;
			int amountofRand = 1;
			print (SpawnObjects.Length);
			for(int i = 0; i<amountofRand;i++){
				int randomNumber = Random.Range(0,SpawnObjects.Length);
				GameObject RandomElement = Network.Instantiate (SpawnObjects[randomNumber], WhereToSpawn.transform.position, Quaternion.identity, 0) as GameObject;
				RandomElement.gameObject.rigidbody2D.AddForce(Vector2.right*10*SpawnObjects[randomNumber].rigidbody2D.mass,ForceMode2D.Impulse);
			}
		}

		if(ActivateStatus){
			resetTimer += Time.deltaTime;
			if(resetTimer > 10.0f){
				resetTimer = 0.0f;
				ActivateStatus = false;
				anim.SetBool("Activated", false);
			}
		}
	}
}
