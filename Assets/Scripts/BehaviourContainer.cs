using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("Behaviours")]
public class BehaviourContainer{
	[XmlArray("Nodes")]
	[XmlArrayItem("node")]
	public List<Node> nodes = new List<Node>();

	public static BehaviourContainer Load()
	{
		string path = Application.dataPath + "/Resources/Behaviours.xml";
		var serializer = new XmlSerializer(typeof(BehaviourContainer));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as BehaviourContainer;
		}
	}
}
