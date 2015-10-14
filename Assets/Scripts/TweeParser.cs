using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

public class TweeParser {
	private static TweeParser instance = null;
	private TextAsset tweeAsset;
	private List<Node> nodeList;

	public static TweeParser getInstance() {
		if (instance == null) {
			instance = new TweeParser();
		}
		return instance;
	}

	public List<Node> getNodeList() {
		return nodeList;
	}

	private TweeParser() {
		tweeAsset = (TextAsset)Resources.Load("tweeAsset", typeof(TextAsset));
		nodeList = new List<Node> ();
		Parse();
	}
		
	private void Parse() {
		int aux = 0;
		bool error = false;
		Node node = null;
		string[] lines;
		string[] elements;
		string line;

		lines = tweeAsset.text.Replace("\r\n", "\n").Split('\n');
		
		for (int i = 0; i < lines.Length; i++) {
			line = lines[i];
			if (line.Length == 0) {
				continue;
			} if (line.ToLower().ToLower().StartsWith(":: ")) {
				if (node != null) {
					nodeList.Add(node);
				}
				node = newNode();
				node.Id = line.Substring(3).ToLower();
			} else {
				elements = line.Split(new string[] {"-"}, System.StringSplitOptions.None);
				if (elements[0].ToLower().StartsWith(">narrator")) {
					if (elements.Length < 2) {
						Debug.Log("Error in node \"" + node.Id + "\": Invalid separator on \">narrator\" command. Try to use \"-\" after \">narrator\".");
						error = true;
						continue;
					}
					else if (elements[1].Length == 0) {
						Debug.Log("Error in node \"" + node.Id + "\": Missing narrator content.");
						error = true;
						continue;
					}
					node.Narrator = elements[1].ToLower();
				} else if (elements[0].ToLower().StartsWith(">animation")) {
					if (elements.Length < 3) {
						Debug.Log("Error in node \"" + node.Id + "\": Invalid separator on \">animation\" command. Try to use \"-\" to separate contents.");
						error = true;
						continue;
					} else if (elements[1].Length == 0) {
						Debug.Log("Error in node \"" + node.Id + "\": Missing animation object.");
						error = true;
						continue;
					} else if (elements[2].Length == 0) {
						Debug.Log("Error in node \"" + node.Id + "\": Missing animation trigger.");
						error = true;
						continue;
					}
					node.objAnims.Add(new ObjectAnimation(elements[1], elements[2]));
				} else if (line.ToLower().ToLower().StartsWith(">loadroom")) {
					if (elements.Length < 2) {
						Debug.Log("Error in node \"" + node.Id + "\": Invalid separator on \">loadroom\" command. Try to use \"-\" after \">loadroom\".");
						error = true;
						continue;
					} else if (elements[1].Length == 0) {
						Debug.Log("Error in node \"" + node.Id + "\": Missing loadroom content.");
						error = true;
						continue;
					}
					node.Room = elements[1];				
				} else if (line.ToLower().ToLower().StartsWith(">unloadroom")) {
					if (elements.Length < 2) {
						Debug.Log("Error in node \"" + node.Id + "\": Invalid separator on \">unloadroom\" command. Try to use \"-\" after \">unloadroom\".");
						error = true;
						continue;
					} else if (elements[1].Length == 0) {
						Debug.Log("Error in node \"" + node.Id + "\": Missing unloadroom content.");
						error = true;
						continue;
					}
					node.RemoveRoom = elements[1];
				} else if (line.ToLower().ToLower().StartsWith(">playerspeed")) {
					if (elements.Length < 2) {
						Debug.Log("Error in node \"" + node.Id + "\": Invalid separator on \">playerspeed\" command. Try to use \"-\" after \">playerspeed\".");
						error = true;
						continue;
					}
					else if (elements[1].Length == 0) {
						Debug.Log("Error in node \"" + node.Id + "\": Missing player speed.");
						error = true;
						continue;
					} else if (!int.TryParse(elements[1], out aux)) {
						Debug.Log("Error in node \"" + node.Id + "\": Invalid playerspeed.");
						error = true;
						continue;
					}
					node.PlayerSpeed = aux;
				} else if (line.ToLower().ToLower().StartsWith("[[autointeract")) {
					if (elements.Length < 3) {
						Debug.Log("Error in node \"" + node.Id + "\": Invalid separator on \"autointeract\" command. Try to use \"-\" to separate contents.");
						error = true;
						continue;
					} else if (elements[1].Length == 0) {
						Debug.Log("Error in node \"" + node.Id + "\": Missing autointeract object.");
						error = true;
						continue;
					} else if (elements[2].Length == 0) {
						Debug.Log("Error in node \"" + node.Id + "\": Missing autointeract next node.");
						error = true;
						continue;
					}
					node.Triggers.Add((int) Trigger.autointeract);
					node.TriggersNames.Add(elements[1]);
					elements[2] = elements[2].Substring(1).Replace("]", string.Empty);
					node.ChildId.Add(elements[2].ToLower());
				} else if (line.ToLower().ToLower().StartsWith("[[buttoninteract")) {
					if (elements.Length < 3) {
						Debug.Log("Error in node \"" + node.Id + "\": Invalid separator on \"buttoninteract\" command. Try to use \"-\" to separate contents.");
						error = true;
						continue;
					} else if (elements[1].Length == 0) {
						Debug.Log("Error in node \"" + node.Id + "\": Missing buttoninteract object.");
						error = true;
						continue;
					} else if (elements[2].Length == 0) {
						Debug.Log("Error in node \"" + node.Id + "\": Missing buttoninteract next node.");
						error = true;
						continue;
					}
					node.Triggers.Add((int) Trigger.buttoninteract);
					node.TriggersNames.Add(elements[1]);
					elements[2] = elements[2].Substring(1).Replace("]", string.Empty);
					node.ChildId.Add(elements[2].ToLower());
				} else if (line.ToLower().ToLower().StartsWith("[[wait")) {
					if (elements.Length < 3) {
						Debug.Log("Error in node \"" + node.Id + "\": Invalid separator on \"wait\" command. Try to use \"-\" to separate contents.");
						error = true;
						continue;
					} else if (elements[1].Length == 0) {
						Debug.Log("Error in node \"" + node.Id + "\": Missing wait time.");
						error = true;
						continue;
					} else if (elements[2].Length == 0) {
						Debug.Log("Error in node \"" + node.Id + "\": Missing wait next node.");
						error = true;
						continue;
					} else if (!int.TryParse(elements[1], out aux)) {
						Debug.Log("Error in node \"" + node.Id + "\": Invalid wait time.");
						error = true;
						continue;
					}
					node.Triggers.Add((int) Trigger.wait);
					node.WaitTime = aux;
					node.TriggersNames.Add(null);
					elements[2] = elements[2].Substring(1).Replace("]", string.Empty);
					node.ChildId.Add(elements[2].ToLower());
				} else if (line.ToLower().ToLower().StartsWith("[[actionbutton")) {
					if (elements.Length < 2) {
						Debug.Log("Error in node \"" + node.Id + "\": Invalid separator on \"actionbutton\" command. Try to use \"-\" to separate contents.");
						error = true;
						continue;
					} else if (elements[1].Length == 0) {
						Debug.Log("Error in node \"" + node.Id + "\": Missing actionbutton next node.");
						error = true;
						continue;
					} 
					node.Triggers.Add((int) Trigger.actionbutton);
					node.TriggersNames.Add(null);
					elements[1] = elements[1].Substring(1).Replace("]", string.Empty);
					node.ChildId.Add(elements[1].ToLower());
				} else if (line.ToLower().ToLower().StartsWith(">") || line.ToLower().ToLower().StartsWith("[[")) {
					Debug.Log("Error in node \"" + node.Id + "\": Invalid tag.");
					error = true;
						continue;
				}
			}
		}
		nodeList.Add(node);
		if (error)
			nodeList = null;
	}

	private Node newNode() {
		Node node = new Node();
		node.ChildId = new List<string>();
		node.Triggers = new List<int> ();
		node.objAnims = new List<ObjectAnimation>();
		node.ChildNode = new List<Node>();
		node.TriggersNames = new List<string>();

		return node;
	}
}