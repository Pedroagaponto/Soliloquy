using UnityEngine;
using System.Collections;

public class ElectricScript : MonoBehaviour {
	public GameObject electricStuff;
	public AudioClip myClip;
	// Use this for initialization
	void Electrocute () {
		electricStuff.SetActive(true);
		Debug.Log ("Working");
	}
	
	// Update is called once per frame
	void noElectroctue () {
		electricStuff.SetActive(false);
	}

	void electricSound(){
		GetComponent<AudioSource>().PlayOneShot(myClip);
}
}
