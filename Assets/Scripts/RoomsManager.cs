using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomsManager : MonoBehaviour {
	private Dictionary <string, float> roomsSize = new Dictionary<string, float>();
	private static float fixedOffSet = 0.25f;
	private float offSet = 66.0f;

	void Start () {
		roomsSize.Add("FifthRoom", 15f);
        roomsSize.Add("SeventhRoom", 15f);
     	roomsSize.Add("SixthRoom", 25f);
	}

	void spawnRoom(string name) {
		if (roomsSize.ContainsKey(name)) {
			GameObject room = GameObject.Find(name);
			room.transform.position = new Vector3 (0.0f, 5.0f, offSet);
			offSet += fixedOffSet + roomsSize[name];
		} else {
			Debug.Log(name + " not found");
		}
	}
}
