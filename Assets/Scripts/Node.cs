using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

public class Node{	
	public string Id { get; set; }
	public string LChildId { get; set; }
	public string RChildId { get; set; }
	public string Narrator { get; set; }
	public string Room { get; set; }
	public int PlayerSpeed { get; set; }
	public int TriggerType { get; set; }
	public int WaitTime { get; set; }
	public GameObject Obj { get; set; }
	public Node LeftChild { get; set; }
	public Node RightChild { get; set; }
	public List<ObjectAnimation> objAnims = new List<ObjectAnimation>();
}
