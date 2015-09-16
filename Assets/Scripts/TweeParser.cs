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
		Node node = null;
		StringBuilder buffer = new StringBuilder();
		string[] lines;
		string[] elements;
		string line;

		lines = tweeAsset.text.Split(new string[] {"\n"}, System.StringSplitOptions.None);
		
		for (int i = 0; i < lines.Length; i++) {
			line = lines[i].ToLower();
			if (line.Length == 0) {
				continue;
			} if (line.StartsWith(":: ")) {
				if (node != null) {
					nodeList.Add(node);
				}
				node = new Node();
				node.Id = line.Substring(3);
			} else {
				elements = line.Split(new string[] {"-"}, System.StringSplitOptions.None);
				if (elements[0].StartsWith(">narrator")) {
					if (elements.Length < 2) {
						Debug.Log("Invalid separator. Try to use \"-\" after \">narrator\" (node: " + node.Id + ", twee line: " + i + ")");
						Application.Quit();
					}
					else if (elements[1].Length == 0) {
						Debug.Log("Missing narrator content (node: " + node.Id + ", line: " + i + ")");
						Application.Quit();
					}
					node.Narrator = elements[1];
				} else if (elements[0].StartsWith(">animation")) {
					if (elements.Length < 3) {
						Debug.Log("Invalid separator. Try to use \"-\" to separate contents (node: " + node.Id + ", twee line: " + i + ")");
						Application.Quit();
					} else if (elements[1].Length == 0) {
						Debug.Log("Missing animation object (node: " + node.Id + ", line: " + i + ")");
						Application.Quit();
					} else if (elements[2].Length == 0) {
						Debug.Log("Missing animation trigger (node: " + node.Id + ", line: " + i + ")");
						Application.Quit();
					}
					node.objAnims.Add(new ObjectAnimation(elements[1], elements[2]));
				} else if (line.StartsWith(">loadroom")) {
					if (elements.Length < 3) {
						Debug.Log("Invalid separator. Try to use \"-\" after \">loadroom\" (node: " + node.Id + ", twee line: " + i + ")");
						Application.Quit();
					} else if (elements[1].Length == 0) {
						Debug.Log("Missing loadroom content (node: " + node.Id + ", line: " + i + ")");
						Application.Quit();
					}
					node.Room = elements[1];				
				} else if (line.StartsWith(">playerspeed")) {
					if (elements.Length < 2) {
						Debug.Log("Invalid separator. Try to use \"-\" after \">playerspeed\" (node: " + node.Id + ", twee line: " + i + ")");
						Application.Quit();
					}
					else if (elements[1].Length == 0) {
						Debug.Log("Missing player speed (node: " + node.Id + ", line: " + i + ")");
						Application.Quit();
					} else if (!int.TryParse(elements[1], out aux)) {
						Debug.Log("Invalid playerspeed (line: " + i + ")");
						Application.Quit();
					}
					node.PlayerSpeed = aux;
				} else if (line.StartsWith("[[autointeract")) {
					if (elements.Length < 3) {
						Debug.Log("Invalid separator. Try to use \"-\" to separate contents (node: " + node.Id + ", twee line: " + i + ")");
						Application.Quit();
					} else if (elements[1].Length == 0) {
						Debug.Log("Missing autointeract object (node: " + node.Id + ", line: " + i + ")");
						Application.Quit();
					} else if (elements[2].Length == 0) {
						Debug.Log("Missing autointeract next node (node: " + node.Id + ", line: " + i + ")");
						Application.Quit();
					}
					node.TriggerType = (int) Trigger.autointeract;
					node.Obj = GameObject.Find(elements[1]);
					elements[2] = elements[2].Substring(1).Replace("]", string.Empty);
					if (node.LChildId == null) {
						node.LChildId = elements[2];
					} else {
						node.RChildId = elements[2];
					}
				} else if (line.StartsWith("[[buttoninteract")) {
					if (elements.Length < 3) {
						Debug.Log("Invalid separator. Try to use \"-\" to separate contents (node: " + node.Id + ", twee line: " + i + ")");
						Application.Quit();
					} else if (elements[1].Length == 0) {
						Debug.Log("Missing buttoninteract object (node: " + node.Id + ", line: " + i + ")");
						Application.Quit();
					} else if (elements[2].Length == 0) {
						Debug.Log("Missing buttoninteract next node (node: " + node.Id + ", line: " + i + ")");
						Application.Quit();
					}
					node.TriggerType = (int) Trigger.buttoninteract;
					node.Obj = GameObject.Find(elements[1]);
					elements[2] = elements[2].Substring(1).Replace("]", string.Empty);
					if (node.LChildId == null) {
						node.LChildId = elements[2];
					} else {
						node.RChildId = elements[2];
					}
				} else if (line.StartsWith("[[wait")) {
					if (elements.Length < 3) {
						Debug.Log("Invalid separator. Try to use \"-\" to separate contents (node: " + node.Id + ", twee line: " + i + ")");
						Application.Quit();
					} else if (elements[1].Length == 0) {
						Debug.Log("Missing wait time (node: " + node.Id + ", line: " + i + ")");
						Application.Quit();
					} else if (elements[2].Length == 0) {
						Debug.Log("Missing wait next node (node: " + node.Id + ", line: " + i + ")");
						Application.Quit();
					} else if (!int.TryParse(elements[1], out aux)) {
							Debug.Log("Invalid wait time (node: " + node.Id + ", line: " + i + ")");
							Application.Quit();
					}
					node.TriggerType = (int) Trigger.buttoninteract;
					node.WaitTime = aux;

					elements[2] = elements[2].Substring(1).Replace("]", string.Empty);
					if (node.LChildId == null) {
						node.LChildId = elements[2];
					} else {
						node.RChildId = elements[2];
					}
				} else if (line.StartsWith(">") || line.StartsWith("[[")) {
					Debug.Log("Invalid tag (node: " + node.Id + ", line: " + i + ")");
					Application.Quit();
				}
			}
		}
		nodeList.Add(node);
	}
}