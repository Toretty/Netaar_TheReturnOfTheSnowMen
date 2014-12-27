using UnityEngine;
using System.Collections;

public class PresentSpawn : MonoBehaviour {
	
	public GameObject Present;

	public float SpawnInterval;
	public int SpawnAmount;
	private int SpawnCounter = 0;
	private float counter = 0.0f;

	// Update is called once per frame
	void Update () {
	
		if(GameObject.FindGameObjectWithTag("StartMapManager").GetComponent<StartMap>().EnableMap){
			if(SpawnCounter < SpawnAmount){
				if (counter > SpawnInterval) {

					if(Network.isServer){
						Network.Instantiate (Present, transform.position, Quaternion.identity,0);
					}

					counter = 0.0f;
					SpawnCounter += 1;
				} else {
					counter += Time.deltaTime;
				}
			}
		}

	}
}
