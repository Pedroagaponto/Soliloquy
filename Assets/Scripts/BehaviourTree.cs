using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

public class BehaviourTree : MonoBehaviour {
	private Node behaviour;
	private int oldTime, newTime;
	private bool printed, positiveBehaviour;

	void Start()
	{
		var behaviourContainer = BehaviourContainer.Load();
		initTree(behaviourContainer.nodes);
		resetBehaviour();
	}

	void initTree(List<Node> list)
	{
		foreach (Node element in list)
		{
			element.setNegativeChild((element.nChild >= 0) ? list[element.nChild] : null);
			element.setPositiveChild((element.pChild >= 0) ? list[element.pChild] : null);
		}
		behaviour = list[0];
	}

	void resetBehaviour()
	{
		oldTime = newTime = System.DateTime.Now.Second;
		printed = positiveBehaviour = false;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			positiveBehaviour = true;
	}

	void FixedUpdate()
	{
		if (behaviour == null)
			return;

		if (!printed)
		{
			print(behaviour.data);
			printed = true;
		}

		newTime = System.DateTime.Now.Second;
		if (positiveBehaviour) {
			behaviour = (behaviour.getPositiveChild() != null) ?
				behaviour.getPositiveChild() : behaviour.getNegativeChild();
			resetBehaviour();
		}
		else if (newTime - oldTime > 2)
		{
			behaviour = (behaviour.getNegativeChild() != null) ?
				behaviour.getNegativeChild() : behaviour.getPositiveChild();
			resetBehaviour();
		}
	}
}
