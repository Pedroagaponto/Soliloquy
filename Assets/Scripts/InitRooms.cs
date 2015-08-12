using UnityEngine;
using System.Collections;

public class InitRooms : MonoBehaviour {
	public GameObject nextRoom;
	public GameObject sideRoom;

	void Start()
	{
		Instantiate (nextRoom, new Vector3(0, 0, 10), Quaternion.identity);
		Instantiate (sideRoom, new Vector3(-10, 0, 10), Quaternion.identity);
	}
}
