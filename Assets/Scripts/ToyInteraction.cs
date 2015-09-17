using UnityEngine;
using System.Collections;

public class ToyInteraction : MonoBehaviour, MyObjectTrigger{
	private int triggerId = -1;
	private bool activated = false, insideCollider = false;

	public void ActivateTrigger(int i) {
		triggerId = i;
		activated = true;
	}

	public void DeactivateTrigger() {
		triggerId = -1;
		activated = false;
	}

	void OnTriggerEnter(Collider other)	{
		insideCollider = true;
	}

	void OnTriggerExit(Collider other) {
		insideCollider = false;
	}

	void Update() {
		if (activated && Input.GetButtonDown("Interact") && insideCollider) {
			GameObject behaviourTree = new GameObject();
			behaviourTree = GameObject.Find("BehaviourTree");
			behaviourTree.SendMessage("TriggerNextChoice", triggerId);
		}
	}
}
