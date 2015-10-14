using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomsManager : MonoBehaviour {
	private Dictionary <string, float> roomsSize = new Dictionary<string, float>();
	private static float fixedOffSet = 0.25f;
	private float offSet = 66.0f;

	void Start () {
		roomsSize.Add("FifthRoom", 15f);
     	roomsSize.Add("SixthRoom", 15f);
        roomsSize.Add("SeventhRoom", 25f);
		roomsSize.Add("InvisibleRoom", 25f);  
	}

	void SpawnRoom(string name) {
		if (roomsSize.ContainsKey(name)) {
			GameObject room = GameObject.Find(name);
			room.transform.position = new Vector3 (0.0f, 5.0f, offSet);
			offSet += fixedOffSet + roomsSize[name];
		} else {
			Debug.Log(name + " not found");
		}
	}

	void RemoveRoom(string name) {
		if (roomsSize.ContainsKey(name)) {
			GameObject room = GameObject.Find(name);
			room.transform.position = new Vector3 (0.0f, 5.0f, -60);
			offSet -= fixedOffSet + roomsSize[name];
		} else {
			Debug.Log(name + " not found");
		}
	}
}
