using UnityEngine;
using System.Collections;

public class ElectricScript : MonoBehaviour {
	public GameObject electricStuff;
	public AudioClip myClip;
	private Animator anim;

	void Awake()
	{
		anim = GetComponent <Animator> ();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Lamp") {
			anim.SetTrigger("Zap");
		Debug.Log("yas");
		}
	}

	void Electrocute () {
		electricStuff.SetActive(true);
		//electricStuff.GetComponent<Animator>().enabled = true;
		Debug.Log ("Working");
	}

	void noElectroctute () {
		electricStuff.SetActive(false);
		//electricStuff.GetComponent<Animator>().enabled = false;
		Debug.Log ("No Ele");
	}

	void electricSound(){
		GetComponent<AudioSource>().PlayOneShot(myClip);
	}
}
