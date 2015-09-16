using UnityEngine;
using System.Collections;

public class ObjectAnimation{
	public GameObject gameObject;
	public string animTrigger;
	
	public ObjectAnimation(string objectName, string animTrigger) {
		gameObject = GameObject.Find(objectName);
		this.animTrigger = animTrigger;
	}

	public void animateObject() {
		gameObject.GetComponent<Animation>().Play (animTrigger);
	}
}
