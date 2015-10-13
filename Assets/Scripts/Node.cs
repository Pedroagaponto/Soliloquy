using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

public class Node{	
	public string Id { get; set; }
	public string Narrator { get; set; }
	public string Room { get; set; }

	public int WaitTime { get; set; }
	public int PlayerSpeed { get; set; }

	public List<string> ChildId { get; set; }
	public List<string> TriggersNames { get; set; }
	public List<int> Triggers { get; set; }
	public List<Node> ChildNode { get; set; }
	public List<ObjectAnimation> objAnims = new List<ObjectAnimation>();
}
