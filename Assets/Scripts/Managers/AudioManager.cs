using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	//Audio players components.
	[SerializeField] private AudioSource SFXSource;
	[SerializeField] private AudioSource MusicSource;

	//Audio Mixers
	[SerializeField] private AudioMixerGroup SFXMixerGroup;
	[SerializeField] private AudioMixerGroup MusicMixerGroup;

	[SerializeField] private AudioClip backgroundMusic;

	//Singleton 
	public static AudioManager Instance = null;

	private void Awake()
	{
		//Singleton pattern
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);

		//Set the groups of the AudioMixer
		SFXSource.outputAudioMixerGroup = SFXMixerGroup;
		MusicSource.outputAudioMixerGroup = MusicMixerGroup;

		PlayMusic(backgroundMusic);
	}

	/// <summary>
	/// Play a single Audio Clip.
	/// </summary>
	/// <param name="clip"></param>
	public void PlayAudio(AudioClip clip)
	{
		SFXSource.clip = clip;
		SFXSource.Play();
	}

	/// <summary>
	/// Play music in the Music Audio Source.
	/// </summary>
	/// <param name="clip"></param>
	public void PlayMusic(AudioClip clip)
	{
		MusicSource.clip = clip;
		MusicSource.Play();
	}

}
