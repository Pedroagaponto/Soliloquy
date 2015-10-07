﻿using UnityEngine;
using System.Collections;

public class MirrorInteraction : MonoBehaviour, MyObjectTrigger {
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

	public void Explode() {
		//TODO
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
