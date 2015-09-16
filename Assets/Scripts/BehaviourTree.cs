using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

public class BehaviourTree : MonoBehaviour {
	public Node behaviour;
	private int oldTime, newTime;
	private bool printed, positiveBehaviour;

	void Start()
	{
		TweeParser parser = TweeParser.getInstance ();
		List<Node> nodeList = parser.getNodeList (); 
		initTree (nodeList);
		printNode(behaviour);
	}

	void initTree(List<Node> list)
	{
		foreach (Node element in list)
		{
			element.LeftChild = findNodeById(element.LChildId, list);
			element.RightChild = findNodeById(element.RChildId, list);
		}
		behaviour = list[0];
	}

	void Update()
	{
		//TODO
	}

	void FixedUpdate()
	{
		//TODO
	}

	private Node findNodeById(string id, List<Node> nodeList) {
		foreach (Node node in nodeList) {
			if (id != null && id.StartsWith(node.Id))
				return node;
		}
		return null;
	}

	private void printNode(Node node) {
		Debug.Log("Node: " + node.Id);
		Debug.Log("left child: " + node.LChildId + ", right child: " + node.RChildId);
		Debug.Log("Narrator: " + node.Narrator);
		Debug.Log("-----------");
		if (node.LeftChild != null) {
			printNode(node.LeftChild);
		}
		if (node.RightChild != null) {
			printNode(node.RightChild);
		}
	}
}
