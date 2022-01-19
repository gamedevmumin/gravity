using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


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

	public void setSource(AudioSource _source)
	{
		this._source = _source;
		this._source.clip = clip;
		this._source.loop = loop;
	}

	public void play()
	{
		_source.volume = volume * (1 + Random.Range(-randomVolume/2f, randomVolume/2f));
		_source.pitch =  1 + Random.Range(-randomPitch / 2f, randomPitch / 2f);
		_source.Play();
	}

	public void stop()
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
			sounds[i].setSource(go.AddComponent<AudioSource>());
			
		}
	}

	public void PlaySound(string soundName)
	{
		foreach (var t in sounds.Where(t => t.name == soundName))
		{
			t.volume *= (soundSettings.SoundVolume)/10f;
			t.play();
			return;
		}
	}

	public void StopSound(string soundName)
	{
		foreach (var t in sounds.Where(t => t.name == soundName))
		{
			t.stop();
			return;
		}
	}

}
