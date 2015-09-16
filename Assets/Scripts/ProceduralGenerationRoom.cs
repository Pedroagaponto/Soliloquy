using UnityEngine;
using System.Collections;

public class ProceduralGenerationRoom : MonoBehaviour
{
	public GameObject nextRoom;

	private BoxCollider door;
	private bool instantiated = false;

	void Start()
	{
		door = GetComponent <BoxCollider> ();
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (!instantiated)
		{
			float z = door.transform.position.z + 5; 

			Instantiate (nextRoom, new Vector3(0, 0, z), Quaternion.identity);
			instantiated = true;
		}
	}
}
