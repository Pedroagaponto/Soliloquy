using UnityEngine;
using System.Collections;

public class FifthRoomInteraction : MonoBehaviour, MyObjectTrigger {
	private int triggerId = -1;
	private bool activated = false;
	
	public void ActivateTrigger(int i) {
		triggerId = i;
		activated = true;
	}
	
	public void DeactivateTrigger() {
		triggerId = -1;
		activated = false;
	}
	
	void OnTriggerStay(Collider other)
	{
		if (activated){
			GameObject behaviourTree = new GameObject();
			behaviourTree = GameObject.Find("BehaviourTree");
			behaviourTree.SendMessage("TriggerNextChoice", triggerId);
		}
	}
}
