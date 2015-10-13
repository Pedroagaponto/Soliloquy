using UnityEngine;
using System.Collections;

public class MirrorInteraction : MonoBehaviour, MyObjectTrigger {
	private int triggerId = -1, triggerType = -1;
	private bool activated = false;
	
	public void ActivateTrigger(int[] args) {
		triggerId = args[0];
		triggerType = args[1];
		activated = true;
	}
	
	public void DeactivateTrigger() {
		triggerId = -1;
		activated = false;
	}

	void OnTriggerStay(Collider other)
	{
		if (activated) {
			if (triggerType == (int)Trigger.autointeract ||
			    (Input.GetButtonDown ("Interact") &&
			 	triggerType == (int)Trigger.buttoninteract)) {
				GetComponent<Animator>().enabled = true;
				NextChoice ();
			}
		}
	}

	private void NextChoice() {
		GameObject behaviourTree = GameObject.Find("BehaviourTree");
		behaviourTree.SendMessage("TriggerNextChoice", triggerId);
	}
}
