using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

public class BehaviourTree : MonoBehaviour {
	public Node behaviour;
	private int oldTime, newTime;
	private bool printed, positiveBehaviour;

	public void TriggerNextChoice(int i) {
		behaviour = behaviour.ChildNode[i];
		oldTime = newTime = System.DateTime.Now.Second;
		Debug.Log(behaviour.Narrator);
	}

	void Awake()
	{
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
		if (!Input.GetKeyDown (KeyCode.Space))
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
			    && newTime - oldTime > behaviour.WaitTime) {
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
		for(int i = 0; i < behaviour.Triggers.Count; i++) {
			if (behaviour.Triggers[i] == (int) Trigger.autointeract ||
			    behaviour.Triggers[i] == (int) Trigger.buttoninteract) {
				behaviour.TriggeredObjects[i].SendMessage("activateTrigger", i);
			}
		}
	}
}
