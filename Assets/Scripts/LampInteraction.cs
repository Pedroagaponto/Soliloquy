using UnityEngine;
using System.Collections;

public class LampInteraction : MonoBehaviour, MyObjectTrigger {
	private int triggerId = -1;

	public void ActivateTrigger(int[] args) {
		triggerId = args[0];
	}
	
	public void DeactivateTrigger() {
		triggerId = -1;
	}
	
	void OnTriggerEnter(Collider other)	{
		GameObject behaviourTree = GameObject.Find("BehaviourTree");
		behaviourTree.SendMessage("TriggerNextChoice", triggerId);
	}
}
