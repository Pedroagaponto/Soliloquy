using UnityEngine;
using System.Collections;

public class PictureInFrameInteraction : MonoBehaviour, MyObjectTrigger {
	private int triggerId = -1, triggerType = -1;
	private bool activated = false;

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
			NextChoice ();
		}
	}
	
	void OnTriggerExit(Collider other) {
	}

	void OnTriggerStay() {
		if (activated) {
			if (triggerType == (int)Trigger.autointeract ||
				(Input.GetButtonDown ("Interact") &&
				triggerType == (int)Trigger.buttoninteract)) {
				NextChoice ();
			}
		}
	}

	private void NextChoice() {
		GameObject behaviourTree = GameObject.Find("BehaviourTree");
		behaviourTree.SendMessage("TriggerNextChoice", triggerId);
	}
}
