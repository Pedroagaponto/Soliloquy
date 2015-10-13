using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour, MyObjectTrigger
{
	public AudioSource openDoorAudio;

	private Vector3 origin;
	private int triggerId = -1, triggerType = -1;
	private bool lockedDoor = true;
	private bool rotating = false, open = false;
	private float angle = 0;
	private float smoothing = 120f;

	void Awake() {
		openDoorAudio = GetComponent<AudioSource> ();
		
		origin = this.transform.position;
		origin -= Quaternion.Euler(this.transform.parent.rotation.eulerAngles) *
			(this.transform.localScale.x/2 * Vector3.right);
	}

	public void ActivateTrigger(int[] args) {
		triggerId = args[0];
		triggerType = args[1];
		if (triggerType == (int)Trigger.autointeract) {
			lockedDoor = false;
		}

		GetComponent<EllipsoidParticleEmitter>().enabled = true;
		GetComponent<ParticleRenderer>().enabled = true;
	}

	public void DeactivateTrigger() {
		GetComponent<ParticleRenderer>().enabled = false;
		GetComponent<EllipsoidParticleEmitter>().enabled = false;
		triggerId = -1;
		triggerType = -1;
	}

	void OnTriggerStay(Collider other)
	{
		if (!lockedDoor && triggerType == (int)Trigger.autointeract) {
			Open();
		} else if (Input.GetButtonDown("Interact") && triggerType == (int)Trigger.buttoninteract) {
			NextChoice();
		} 
	}

	void FixedUpdate()
	{
		if (rotating && !open) {
			this.transform.RotateAround (origin, Vector3.up, -smoothing * Time.deltaTime);
			angle += -smoothing * Time.deltaTime;
			if (angle <= -95) {
				rotating = false;
				open = true;
				if (!lockedDoor) {
					NextChoice ();
				}
			}
		} 
	}

	void Open()
	{
		rotating = true;
		openDoorAudio.Play();
	}

	private void NextChoice() {
		GameObject behaviourTree = GameObject.Find("BehaviourTree");
		behaviourTree.SendMessage("TriggerNextChoice", triggerId);
	}
}
