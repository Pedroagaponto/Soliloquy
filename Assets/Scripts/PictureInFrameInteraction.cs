using UnityEngine;
using System.Collections;

public class PictureInFrameInteraction : MonoBehaviour, MyObjectTrigger {
	private int triggerId = -1, triggerType = -1;
	private bool activated = false, insideCollider = false;

	void Start() {
		GetComponent<EllipsoidParticleEmitter>().enabled = false;
		GetComponent<ParticleRenderer>().enabled = false;
	}

	public void ActivateTrigger(int[] args) {
		GetComponent<EllipsoidParticleEmitter>().enabled = true;
		GetComponent<ParticleRenderer>().enabled = true;
		triggerId = args[0];
		triggerType = args[1];
		activated = true;
	}
	
	public void DeactivateTrigger() {
		GetComponent<EllipsoidParticleEmitter>().enabled = false;
		GetComponent<ParticleRenderer>().enabled = false;
		triggerId = -1;
		triggerType = -1;
		activated = false;
	}

	void OnTriggerEnter(Collider other)	{
		if (triggerType == (int)Trigger.autointeract) {
			GameObject behaviourTree = new GameObject ();
			behaviourTree = GameObject.Find ("BehaviourTree");
			behaviourTree.SendMessage ("TriggerNextChoice", triggerId);
		}
		insideCollider = true;
	}
	
	void OnTriggerExit(Collider other) {
		insideCollider = false;
	}

	void OnTriggerStay(Collider other) {
		if (activated && Input.GetButtonDown("Interact") &&
		    insideCollider && triggerType == (int)Trigger.buttoninteract) {
			GameObject behaviourTree = new GameObject();
			behaviourTree = GameObject.Find("BehaviourTree");
			behaviourTree.SendMessage("TriggerNextChoice", triggerId);
		}
	}
}
