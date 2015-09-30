using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour {
	void Start () {
		if (transform.name.Equals("FirstRoom/Floor")) {
			ChangingAllRenderers(true);
		} else {
			ChangingAllRenderers(false);
		}
	}

	void OnTriggerEnter(Collider other)	{
		ChangingAllRenderers(true);
	}

	void OnTriggerExit(Collider other) {
		ChangingAllRenderers(false);
	}

	void ChangingAllRenderers(bool renderState) 
	{
		GameObject room = transform.parent.gameObject;
		foreach (Renderer render in room.GetComponentsInChildren<Renderer>())
		{
			render.enabled = renderState;
		}
	}
}
