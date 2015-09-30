using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed = 5f;
	public float smoothing = 8f;
	
	private Vector3 movement;
	//public Animator anim;
	private Rigidbody playerRigidbody;
	
	void Awake()
	{
		playerRigidbody = GetComponent <Rigidbody> ();
		//anim = GetComponent <Animator> ();
	}

	void FixedUpdate()
	{
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");

		Move(h, v);
		//Animating(h, v);
	}
	
	void Move (float h, float v)
	{
		movement.Set(h, 0f, v);
		movement = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up) * movement;
		movement = movement.normalized * speed * Time.deltaTime;
		//playerRigidbody.MovePosition(transform.position + movement);
		transform.position = transform.position + movement;

		if(h != 0 || v != 0)
		{
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movement), smoothing * Time.deltaTime);
		}
	}

	/*void Animating (float h, float v)
	{
		bool walking = h != 0f || v != 0f;
		anim.SetBool ("IsWalking", walking);
	}*/
}