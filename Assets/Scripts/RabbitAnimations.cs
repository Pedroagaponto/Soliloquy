using UnityEngine;
using System.Collections;

public class RabbitAnimations : MonoBehaviour {
	private Animator anim;
	// Use this for initialization
	void Awake()
	{
		anim = GetComponent <Animator> ();
	}
	void Start () {
	
	}
	void FixedUpdate()
	{
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");
	
		Animating(h, v);
	}
	void Animating (float h, float v)
	{
		bool walking = h != 0f || v != 0f;
		anim.SetBool ("IsWalking", walking);
	}
}
