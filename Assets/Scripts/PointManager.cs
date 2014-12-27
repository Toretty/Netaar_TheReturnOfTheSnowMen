using UnityEngine;
using System.Collections;

public class PointManager : MonoBehaviour {

	public int Points = 1;
	private int refPoint;

	void Update(){
		if (Points == refPoint) {

		} else {
			refPoint = Points;
			//print (Points);
		}
	}

	void OnGUI (){
		string pointsString = "" + Points;

		if(Points != 0)
		{
			if (GUI.Button (new Rect (10, 10, 100, 50), pointsString)) {
				print ("You have "+Points+" Points.");
			}
		}
	}

}
