using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MusicController : MonoBehaviour {
	private AudioSource musicAudio;
	private AudioClip currentClip;
	
	private Queue<FileInfo> happyFiles;
	private Queue<FileInfo> sadFiles;
	private const string rootDir = "Assets/Resources/Soundtrack/";

	// Use this for initialization
	void Start () {
		musicAudio = GetComponent<AudioSource> ();

		DirectoryInfo dir = new DirectoryInfo(rootDir + "Happy/");
		FileInfo[] info = dir.GetFiles("*.wav");
		happyFiles = new Queue<FileInfo> (info);
		
		dir = new DirectoryInfo(rootDir + "Sad/");
		info = dir.GetFiles("*.wav");
		sadFiles = new Queue<FileInfo> (info);

	}

	// Update is called once per frame
	void Update () {
	
	}

	void PlayAudio (string filename) {
		if(filename.Contains("happy")){
			FileInfo f = happyFiles.Dequeue();
			happyFiles.Enqueue(f);
			filename = "Happy/"+Path.GetFileNameWithoutExtension(f.Name);
		}
		else if(filename.Contains("sad")){
			FileInfo f = sadFiles.Dequeue();
			sadFiles.Enqueue(f);
			filename = "Sad/"+Path.GetFileNameWithoutExtension(f.Name);
		}

		currentClip = Resources.Load ("Soundtrack/" + filename) as AudioClip;

		
		if (currentClip != null) {
			musicAudio.clip = currentClip;
			musicAudio.Play ();
		}
	}

}