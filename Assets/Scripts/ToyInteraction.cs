using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToyInteraction : MonoBehaviour, MyObjectTrigger{
	public AudioSource toySqueakSound;

	private int triggerId = -1;
	private bool activated = false;

	void Awake() {
		toySqueakSound = GetComponent<AudioSource> ();
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
		GetComponent<ParticleRenderer>().enabled = false;
		GetComponent<EllipsoidParticleEmitter>().enabled = false;
	}

	void OnTriggerStay() {
		if (activated && Input.GetButtonDown ("Interact")) {
			GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 5.0f, 0.0f);
			toySqueakSound.Play();
			GameObject behaviourTree = GameObject.Find ("BehaviourTree");
			behaviourTree.SendMessage ("TriggerNextChoice", triggerId);
		}
	}
}
