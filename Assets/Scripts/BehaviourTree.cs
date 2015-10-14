using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

public class BehaviourTree : MonoBehaviour {
	public Node behaviour;
	public AudioSource soundSource;
	
	private AudioClip soundClip;
	private NarratorController narrator;
	private List<GameObject> objList = new List<GameObject>();
	private float oldTime, newTime;
	private bool printed, positiveBehaviour;
	private GameObject roomsManager;

	public void TriggerNextChoice(int i) {
		foreach (GameObject obj in objList) {
			obj.SendMessage("DeactivateTrigger");
		}
		objList = new List<GameObject>();

		behaviour = behaviour.ChildNode[i];
		if (behaviour.RemoveRoom != null) {
			roomsManager.SendMessage("RemoveRoom", behaviour.RemoveRoom);
		}
		if (behaviour.Room != null) {
			roomsManager.SendMessage("SpawnRoom", behaviour.Room);
		}

		oldTime = newTime = Time.realtimeSinceStartup;
		SetTriggers();
		Debug.Log(behaviour.Narrator);
		narrator.playDialog (behaviour.Id);
		for (int j = 0; j < behaviour.objAnims.Count; j++) {
			behaviour.objAnims[j].animateObject();
		}
	}

	void Start()
	{
		soundSource = GetComponent<AudioSource> ();
		narrator = NarratorController.Instance;
		behaviour = null;
		roomsManager = GameObject.Find("Abstract/RoomsManager");
		TweeParser parser = TweeParser.getInstance();
		List<Node> nodeList = parser.getNodeList();
		if (nodeList == null) {
			Debug.Break();
			Application.Quit();
		}
		initTree(nodeList);
		oldTime = newTime = Time.realtimeSinceStartup;
		SetTriggers();
		Debug.Log(behaviour.Narrator);
		narrator.playDialog(behaviour.Id);
	}

	void initTree(List<Node> list)
	{
		foreach (Node element in list)
		{
			for (int i = 0; i < element.ChildId.Count; i++) {
				element.ChildNode.Insert(i, FindNodeById(element.ChildId[i], list));
			}
		}
		behaviour = list[0];
	}

	void Update()
	{
		if (!Input.GetButtonDown ("Interact"))
			return;

		for(int i = 0; i < behaviour.Triggers.Count; i++) {
			if (behaviour.Triggers[i] == (int) Trigger.actionbutton) {
				TriggerNextChoice(i);
			}
		}
	}

	void FixedUpdate()
	{
		newTime = Time.realtimeSinceStartup;
		for(int i = 0; i < behaviour.Triggers.Count; i++) {
			if (behaviour.Triggers[i] == (int) Trigger.wait
			    && newTime - oldTime >= behaviour.WaitTime) {
				TriggerNextChoice(i);
				break;
			}
		}
	}

	private Node FindNodeById(string id, List<Node> nodeList) {
		foreach (Node node in nodeList) {
			if (id != null && id.Equals(node.Id))
				return node;
		}
		return null;
	}

	private void SetTriggers() {
		GameObject obj;
		if (behaviour == null || behaviour.Triggers == null)
			return;
		for (int i = 0; i < behaviour.Triggers.Count; i++) {
			if (behaviour.TriggersNames[i] != null) {
				obj = GameObject.Find(behaviour.TriggersNames[i]);
				if (obj != null) {
					objList.Add(obj);
					int[] args = {i, behaviour.Triggers[i]};
					obj.SendMessage("ActivateTrigger", args);
				} else {
					Debug.Log(behaviour.TriggersNames[i] + " object not found");
				}
			}
		}
	}
}
