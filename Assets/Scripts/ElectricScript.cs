using UnityEngine;
using System.Collections;

public class ElectricScript : MonoBehaviour {
	public GameObject electricStuff;
	public AudioClip myClip;

	void Electrocute () {
		electricStuff.SetActive(true);
		Debug.Log ("Working");
	}

	void noElectroctue () {
		electricStuff.SetActive(false);
	}

	void electricSound(){
		GetComponent<AudioSource>().PlayOneShot(myClip);
}
}
