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

	private Vector3 PushEntireMap = new Vector3 (50.0f, 0.0f, 0.0f);

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
			Instantiate (MapPrefab, transform.position + PushEntireMap, Quaternion.identity);
			Network.Instantiate (player1, PlayerPosition + PushEntireMap, Quaternion.identity, 0);	
			Instantiate(CameraPrefab_Player1, PlayerPosition + PushEntireMap + new Vector3(10.0f,0.0f,-10.0f), Quaternion.identity);

		} 
		if (Network.isClient) {
			MapSpawned = true;
			Instantiate (MapPrefab, transform.position + PushEntireMap, Quaternion.identity);
			Network.Instantiate (player2, 	PlayerPosition2 + PushEntireMap, Quaternion.identity,0);
			Instantiate (CameraPrefab_Player2, PlayerPosition2 + PushEntireMap + new Vector3(10.0f,0.0f,-10.0f) , Quaternion.identity);
		}
	}

}
