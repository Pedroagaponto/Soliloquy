using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour
{
	public float smoothing = 15f;

	private Transform door;
	private Vector3 origin;
	private bool rotating = false, open = false;
	private float angle = 0;

	void Start()
	{
		door = GetComponent <Transform> ();
		origin = door.transform.position - (door.transform.localScale.x/2 * Vector3.right);
	}
	
	void OnTriggerStay(Collider other)
	{
		rotating = true;
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
			}
		}
	}

	void Open()
	{
		rotating = true;
	}
}
