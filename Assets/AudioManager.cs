using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


/**
 * class representing sound
 */
[System.Serializable]
public class Sound 
{
	public string name;
	public AudioClip clip;

	private AudioSource _source;
	[Range(0f,2f)]
	public float volume = 0.7f;

	[Range(0f, 0.5f)]
	public float randomVolume = 0.1f;
	[Range(0f, 0.5f)]
	public float randomPitch = 0.1f;

	public bool loop;

	
	/**
	 * sets source of sound
	 */
	public void SetSource(AudioSource source)
	{
		_source = source;
		_source.clip = clip;
		_source.loop = loop;
	}

	/**
	 * plays sound
	 */
	public void Play()
	{
		_source.volume = volume * (1 + Random.Range(-randomVolume/2f, randomVolume/2f));
		_source.pitch =  1 + Random.Range(-randomPitch / 2f, randomPitch / 2f);
		_source.Play();
	}

	/**
	 * stops sound
	 */
	public void Stop()
	{
		_source.Stop();
	}
}


public class AudioManager : MonoBehaviour
{
	[SerializeField] private List<Sound> sounds;
	[SerializeField] private SoundSettings soundSettings;
	
	public static AudioManager Instance;

	private void Awake()
	{
		if (Instance != null)
		{
			if (Instance != this)
			{
				Destroy(gameObject);
			}
		}
		else
		{
			Instance = this;
			DontDestroyOnLoad(this);
		}
	}

	private void Start()
	{
		for (var i = 0; i < sounds.Count; i++)
		{
			var go = new GameObject("Sound_" + i + "_" + sounds[i].name);
			go.transform.SetParent(this.transform);
			sounds[i].SetSource(go.AddComponent<AudioSource>());
			
		}
		PlaySound("Music", true);
	}

	public void PlaySound(string soundName, bool music = false)
	{
		foreach (var t in sounds.Where(t => t.name == soundName))
		{
			if (!music)
			{
				t.volume = (soundSettings.SoundVolume)/10f;
			}
			else
			{
				Debug.Log(soundSettings.MusicVolume);
				t.volume = (soundSettings.MusicVolume)/10f;
			}
			
			t.Play();
			return;
		}
	}

	public void StopSound(string soundName)
	{
		foreach (var t in sounds.Where(t => t.name == soundName))
		{
			t.Stop();
			return;
		}
	}

}
