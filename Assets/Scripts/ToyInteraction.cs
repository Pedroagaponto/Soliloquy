using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToyInteraction : MonoBehaviour, MyObjectTrigger{
	private int triggerId = -1;
	private bool activated = false;

	void Awake() {
		GetComponent<EllipsoidParticleEmitter>().enabled = false;
		GetComponent<ParticleRenderer>().enabled = false;
	}

	public void ActivateTrigger(int[] args) {
		triggerId = args[0];
		activated = true;
		GetComponent<EllipsoidParticleEmitter>().enabled = true;
		GetComponent<ParticleRenderer>().enabled = true;
	}

	public void DeactivateTrigger() {
		triggerId = -1;
		activated = false;
		GetComponent<EllipsoidParticleEmitter>().enabled = false;
		GetComponent<ParticleRenderer>().enabled = false;
	}

	void OnTriggerStay() {
		if (activated && Input.GetButtonDown ("Interact")) {
			GameObject behaviourTree = new GameObject ();
			behaviourTree = GameObject.Find ("BehaviourTree");
			behaviourTree.SendMessage ("TriggerNextChoice", triggerId);
		}
	}
}
