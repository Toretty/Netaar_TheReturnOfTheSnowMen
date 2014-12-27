using UnityEngine;
using System.Collections;

public class NetworkscriptContentManager : MonoBehaviour {
	
	/// <summary>
	/// The map prefab.
	/// </summary>
	public GameObject MapPrefab;

	public GameObject CameraPrefab_Player1;
	public GameObject CameraPrefab_Player2;

	/// <summary>
	/// The prefab of the player.
	/// </summary>
	public GameObject player1;

	public GameObject player2;

	public Vector3 PlayerPosition;
	public Vector3 PlayerPosition2;

	/// <summary>
	/// Whether the map is spawned.
	/// </summary>
	public bool MapSpawned = false; 

	/// <summary>
	/// Manager of spawning.
	/// </summary>
	public void ContentManager(){

		if (Network.isServer) {
			MapSpawned = true;
			Instantiate (MapPrefab, transform.position, Quaternion.identity);
			Network.Instantiate (player1, 	PlayerPosition, Quaternion.identity, 0);	
			Instantiate(CameraPrefab_Player1, PlayerPosition + new Vector3(10.0f,0.0f,-10.0f), Quaternion.identity);

		} 
		if (Network.isClient) {
			MapSpawned = true;
			Instantiate (MapPrefab, transform.position, Quaternion.identity);
			Network.Instantiate (player2, 	PlayerPosition2, Quaternion.identity,0);
			Instantiate (CameraPrefab_Player2, PlayerPosition + new Vector3(10.0f,0.0f,-10.0f), Quaternion.identity);
		}
	}

}
