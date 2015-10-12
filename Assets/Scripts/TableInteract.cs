using UnityEngine;
using System.Collections;

public class TableInteract : MonoBehaviour {
	private int triggerId = -1;
	private bool activated = false;

	public void ActivateTrigger(int[] args) {
		triggerId = args[0];
		activated = true;
	}
	
	public void DeactivateTrigger() {
		triggerId = -1;
		activated = false;
	}

	void OnTriggerEnter(Collider other) {
		if (activated){
			GameObject behaviourTree = new GameObject();
			behaviourTree = GameObject.Find("BehaviourTree");
			behaviourTree.SendMessage("TriggerNextChoice", triggerId);
		}
	}

	void FixedUpdate() {
		var speed = 1.0f;

		if (activated) {
			transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time * speed), transform.position.z);
		}
	}
}