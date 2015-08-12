using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

public class Node{
	[XmlAttribute("id")]
	public int id;
	[XmlAttribute("nChild")]
	public int nChild;
	[XmlAttribute("pChild")]
	public int pChild;
	public string data;

	private Node negativeChild;
	private Node positiveChild;

	public void setNegativeChild(Node negativeChild)
	{
		this.negativeChild = negativeChild;
	}
	
	public void setPositiveChild(Node positiveChild)
	{
		this.positiveChild = positiveChild;
	}

	public Node getNegativeChild()
	{
		return negativeChild;
	}
	
	public Node getPositiveChild()
	{
		return positiveChild;
	}
}
