using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour, MyObjectTrigger
{
	public AudioSource openDoorAudio;

	private Vector3 origin;
	private int triggerId = -1;
	private bool lockedDoor = true;
	private bool rotating = false, open = false;
	private float angle = 0;
	private float smoothing = 120f;

	void Awake() {
		GetComponent<EllipsoidParticleEmitter>().enabled = false;
		GetComponent<ParticleRenderer>().enabled = false;
		openDoorAudio = GetComponent<AudioSource> ();
	}

	public void ActivateTrigger(int[] args) {
		triggerId = args[0];
		lockedDoor = false;
		GetComponent<EllipsoidParticleEmitter>().enabled = true;
		GetComponent<ParticleRenderer>().enabled = true;
	}

	public void DeactivateTrigger() {
		GetComponent<EllipsoidParticleEmitter>().enabled = false;
		GetComponent<ParticleRenderer>().enabled = false;
		triggerId = -1;
		lockedDoor = true;
		rotating = false;
		open = true;
	}

	void Start()
	{
		origin = this.transform.position;
		origin -= Quaternion.Euler( this.transform.parent.rotation.eulerAngles) * (this.transform.localScale.x/2 * Vector3.right);
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (!lockedDoor){
			TriggerOpen();
		}
	}

	void FixedUpdate()
	{
		if (rotating && !open)
		{
			this.transform.RotateAround(origin, Vector3.up, -smoothing * Time.deltaTime);
			angle += -smoothing * Time.deltaTime;
			if (angle <= -95)
			{
				rotating = false;
				open = true;
				if (!lockedDoor){
					GameObject behaviourTree = new GameObject();
					behaviourTree = GameObject.Find("BehaviourTree");
					behaviourTree.SendMessage("TriggerNextChoice", triggerId);
				}
			}
		}
	}

	void Open()
	{
		TriggerOpen();
	}

	void TriggerOpen() {
		rotating = true;
		openDoorAudio.Play();
	}
}
