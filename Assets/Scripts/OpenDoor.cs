using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour, MyObjectTrigger
{
	public float smoothing = 15f;

	private Transform door;
	private Vector3 origin;
	private int triggerId = -1;
	private bool lockedDoor = true;
	private bool rotating = false, open = false;
	private float angle = 0;

	public void ActivateTrigger(int i) {
		triggerId = i;
		lockedDoor = false;
	}

	public void DeactivateTrigger() {
		triggerId = -1;
		lockedDoor = true;
		rotating = false;
		open = true;
	}

	void Start()
	{
		door = GetComponent <Transform> ();
		origin = door.transform.position - (door.transform.localScale.x/2 * Vector3.right);
	}
	
	void OnTriggerStay(Collider other)
	{
		if (!lockedDoor){
			rotating = true;
		}
	}

	void FixedUpdate()
	{
		if (rotating && !open)
		{
			door.RotateAround(origin, Vector3.up, -smoothing * Time.deltaTime);
			angle += -smoothing * Time.deltaTime;
			if (angle <= -95)
			{
				rotating = false;
				open = true;
				if (!lockedDoor){
					GameObject behaviourTree = new GameObject();
					behaviourTree = GameObject.Find("BehaviourTree");
					behaviourTree.SendMessage("TriggerNextChoice", triggerId);
				}
			}
		}
	}

	void Open()
	{
		rotating = true;
	}
}
