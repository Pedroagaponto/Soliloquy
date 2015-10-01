using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

public class BehaviourTree : MonoBehaviour {
	public Node behaviour;

	private NarratorController narrator;
	private GameObject gameObject = null;
	private int oldTime, newTime;
	private bool printed, positiveBehaviour;

	public void TriggerNextChoice(int i) {
		if (gameObject != null) {
			gameObject.SendMessage("DeactivateTrigger");
			gameObject = null;
		}
		behaviour = behaviour.ChildNode[i];
		oldTime = newTime = System.DateTime.Now.Second;
		SetTriggers();
		Debug.Log(behaviour.Narrator);
		narrator.playDialog (behaviour.Id);

		for (int j = 0; j < behaviour.objAnims.Count; j++) {
			behaviour.objAnims[j].animateObject();
		}
	}

	void Start()
	{
		narrator = NarratorController.Instance;
		behaviour = null;
		TweeParser parser = TweeParser.getInstance();
		List<Node> nodeList = parser.getNodeList();
		if (nodeList == null) {
			Debug.Break();
			Application.Quit();
		}
		initTree(nodeList);
		oldTime = newTime = System.DateTime.Now.Second;
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
		newTime = System.DateTime.Now.Second;
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
		if (behaviour == null || behaviour.Triggers == null)
			return;
		for (int i = 0; i < behaviour.Triggers.Count; i++) {
			if (behaviour.TriggersNames[i] != null) {
				gameObject = GameObject.Find(behaviour.TriggersNames[i]);
				Debug.Log(behaviour.TriggersNames[i]);
				gameObject.SendMessage ("ActivateTrigger", i);
			}
		}
	}
}
