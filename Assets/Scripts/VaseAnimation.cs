using UnityEngine;
using System.Collections;

public class VaseAnimation : MonoBehaviour {
	void VaseBreak() {
		GetComponent<Animator>().enabled = true;
	}
}
