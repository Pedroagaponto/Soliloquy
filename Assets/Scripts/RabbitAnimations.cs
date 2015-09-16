using UnityEngine;
using System.Collections;

public class RabbitAnimations : MonoBehaviour {
	private Animator anim;
	public AudioClip myClip;
	public AudioClip myClip1;
	// Use this for initialization
	void Awake()
	{
		anim = GetComponent <Animator> ();
	}
	void Start () {
	
	}
	void Interact ()
	{
		GetComponent<AudioSource>().PlayOneShot(myClip);
		
	}
	
	void Walking ()
	{
		GetComponent<AudioSource>().PlayOneShot(myClip1);
		
	}

	void Update()
	{
		if (Input.GetButtonDown("Interact"))
			anim.SetTrigger("Interact");
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
