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
		List<Renderer> rendererList;
		rendererList = new List<Renderer>(transform.parent.gameObject.GetComponentsInChildren<Renderer>());
		foreach (Renderer render in rendererList)
		{
			render.enabled = renderState;
		}
	}
}
