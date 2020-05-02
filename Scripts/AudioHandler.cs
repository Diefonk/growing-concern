using UnityEngine;

public class AudioHandler : MonoBehaviour {
	private AudioSource[] audios;
	private System.Random random;

	private void Awake() {
		audios = GetComponentsInChildren<AudioSource>();
		random = new System.Random();
	}

	public void play() {
		audios[random.Next(0, audios.Length)].Play();
	}
}
