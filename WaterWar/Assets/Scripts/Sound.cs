using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour
{
	public static Sound Instance { get; private set; }

	public AudioClip bump;
	public AudioClip win;
	public AudioClip bing1;
	public AudioClip bing2;
	public AudioClip ping;
	public AudioClip collect;
	public AudioClip touchdown;
	public AudioSource effect;
	public AudioSource effect2;

	private void Awake ()
	{
		if (Instance != null) {
			DestroyImmediate (gameObject);
			return;
		}
		Instance = this;
		DontDestroyOnLoad (this);
	}
	// Use this for initialization
	public void Start ()
	{

	}

	public void PlayBump ()
	{
		effect.PlayOneShot (bump);
	}

	public void PlayWin ()
	{
		if (effect.isPlaying)
			effect2.PlayOneShot (win);
		else
			effect.PlayOneShot (win);
	}

	public void PlayBing1 ()
	{
		if (effect.isPlaying)
			effect2.PlayOneShot (bing1);
		else
			effect.PlayOneShot (bing1);
	}

	public void PlayBing2 ()
	{
		if (effect.isPlaying)
			effect2.PlayOneShot (bing2);
		else
			effect.PlayOneShot (bing2);
	}

	public void PlayPing ()
	{
		if (effect.isPlaying)
			effect2.PlayOneShot (ping);
		else
			effect.PlayOneShot (ping);
	}

	public void PlayCollect ()
	{
		if (effect.isPlaying)
			effect2.PlayOneShot (collect);
		else
			effect.PlayOneShot (collect);
	}

	public void PlayTouchdown ()
	{
		if (effect.isPlaying)
			effect2.PlayOneShot (touchdown);
		else
			effect.PlayOneShot (touchdown);
	}
}
