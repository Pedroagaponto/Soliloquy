using UnityEngine;
using System.Collections;

public class VaseAnimation : MonoBehaviour {
	public AudioSource vaseShattersSound;

	void VaseBreak() {
		vaseShattersSound = GetComponent<AudioSource> ();
		GetComponent<Animator>().enabled = true;
		StartCoroutine("PlayEffect");
	}

	IEnumerator PlayEffect() {
		yield return new WaitForSeconds(1.10f);
		vaseShattersSound.Play();
	}
}
