using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class NarratorController : MonoBehaviour {	
	public List<CallLine> callLines = new List<CallLine>();
	public List<SubLine> subLines = new List<SubLine>();
	
	public static NarratorController Instance {get; private set;}
	
	public GameObject subtitleGameObject;
	public GameObject subtitleShadowGameObject;
	public AudioSource narratorAudio;
	
	private Text subtitleText;
	private Text subtitleTextShadow;
	private AudioClip dialogClip;
	private IEnumerator subCoroutine;
	private IEnumerator callCoroutine;
	
	public class SubLine {
		public string text;
		public float timing;
		
		public SubLine (float timing, string text) {
			this.text = text;
			this.timing = timing;
		}
		
	}
	
	public class CallLine {
		public string obj, method, arg;
		public float timing;
		
		public CallLine (float timing, string obj, string method, string arg){
			this.timing = timing;
			this.obj = obj;
			this.method = method;
			this.arg = arg;
		}
		
	}

	void Awake()
	{
		if (Instance != null &&
		    Instance != this) {
			Destroy (gameObject);
		}
		
		Instance = this;
		narratorAudio = GetComponent<AudioSource> ();
		
		
	}
	
	public void playDialog(string clipName)
	{
		if (subtitleText == null) {
			subtitleText = subtitleGameObject.GetComponent<Text> ();
			subtitleText.supportRichText = true;
		}
		if (subtitleTextShadow == null) {
			subtitleTextShadow = subtitleShadowGameObject.GetComponent<Text> ();
			subtitleTextShadow.supportRichText = true;
		}
		
		TextAsset textFile = Resources.Load ("Subtitles/" + clipName) as TextAsset;
		
		if (textFile == null) {
			return;
		}

		Reset ();
		Parser (textFile);

		dialogClip = Resources.Load ("Narrator/" + clipName) as AudioClip;
		if (dialogClip != null) {
			narratorAudio.clip = dialogClip;
			narratorAudio.Play ();
		}
		
		if(textFile != null)
		{	
			subCoroutine = (dialogClip != null) ? showSubtitleAudio() : showSubtitle();
			callCoroutine = showCallAudio();
			
			StartCoroutine (callCoroutine);
			StartCoroutine (subCoroutine);
		}
		
		
	}
	
	private void Reset() {
		callLines = new List<CallLine>();
		subLines = new List<SubLine>();
		
		if (callCoroutine != null) StopCoroutine (callCoroutine);
		if (subCoroutine != null) StopCoroutine (subCoroutine);
		callCoroutine = subCoroutine = null;
		
		dialogClip = null;
		if(subtitleText != null) subtitleText.text = "";
		if(subtitleTextShadow != null) subtitleTextShadow.text = "";
	}
	
	private void Parser(TextAsset textFile) {
		string[] lines = textFile.text.Replace("\r\n","\n").Split ('\n');
		
		foreach (string line in lines) {
			string[] lineTemp = line.Split('|');
			
			if(lineTemp.Length != 2 || line.StartsWith("//")) continue;
			
			if(lineTemp[1].StartsWith("\\call")){
				string[] temp = lineTemp[1].Split ('-');
				
				if (temp.Length == 3)
					callLines.Add (new CallLine (float.Parse (lineTemp [0]), temp [1], temp [2], null));
				else if (temp.Length == 4)
					callLines.Add (new CallLine (float.Parse (lineTemp [0]), temp [1], temp [2], temp [3]));
			}
			else{
				if(lineTemp[1].Contains("\\clear"))
					lineTemp[1] = "";
				subLines.Add(new SubLine (float.Parse(lineTemp[0]), lineTemp[1]));
			}
			
		}
		
	}
	
	IEnumerator showSubtitle() {
		float prevTime = 0;
		
		foreach(SubLine s in subLines) {
			yield return new WaitForSeconds(s.timing - prevTime);
			
			prevTime = s.timing;
			subtitleText.text = s.text;
			subtitleTextShadow.text = s.text;
		}
	}
	
	IEnumerator showSubtitleAudio() {
		foreach(SubLine s in subLines) {
			while (s.timing > narratorAudio.time &&
			       !(narratorAudio.time == 0 && !narratorAudio.isPlaying))
			{
				//Debug.Log("wait:" + (s.timing - narratorAudio.time) + "time:" + narratorAudio.time + " s.timing:" + s.timing);
				yield return new WaitForSeconds(s.timing - narratorAudio.time);
			}
			
			subtitleText.text = s.text;
			subtitleTextShadow.text = s.text;
		}
	}
	
	IEnumerator showCallAudio() {
		foreach(CallLine s in callLines) {
			while (s.timing > narratorAudio.time &&
			       !(narratorAudio.time == 0 && !narratorAudio.isPlaying))
			{
				//Debug.Log("call:" + (s.timing - narratorAudio.time) + "time:" + narratorAudio.time + " s.timing:" + s.timing);
				yield return new WaitForSeconds(s.timing - narratorAudio.time);
			}
			
			if (s.arg == null)
				GameObject.Find(s.obj).SendMessage(s.method);
			else
				GameObject.Find(s.obj).SendMessage(s.method, s.arg);
		}
	}
}