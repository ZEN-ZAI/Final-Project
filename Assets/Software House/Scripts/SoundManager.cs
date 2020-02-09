using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
	#region Singleton
	public static SoundManager instance;
	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}
    #endregion

    public AudioSource mainAudio;
    public AudioSource supportAudio;

    public AudioClip soundLogin;
    public AudioClip soundGamePlay;

    public AudioClip dialogPimp;

    private void Start()
    {
        SetBGM();
        SceneManager.sceneLoaded += CheckBGM;
    }

    public void CheckBGM(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Login")
        {
            SetBGM(soundLogin);
        }
        else if (scene.name == "Story")
        {
            SetBGM(soundLogin);
        }
        else if (scene.name == "Home")
        {
            SetBGM(soundGamePlay);
        }
    }

    public void SetBGM()
    {
        if (SceneManager.GetActiveScene().name == "Login")
        {
            SetBGM(soundLogin);
        }
        else if (SceneManager.GetActiveScene().name == "Story")
        {
            SetBGM(soundLogin);
        }
        else if (SceneManager.GetActiveScene().name == "Home")
        {
            SetBGM(soundGamePlay);
        }
    }

    public void SetBGM(AudioClip audioClip)
    {
        mainAudio.clip = audioClip;
        mainAudio.Play();
    }

    public void PlaySoundEffect(AudioClip audioClip)
    {
        supportAudio.PlayOneShot(audioClip);
    }

    public void PlayDialogPimp()
    {
        supportAudio.PlayOneShot(dialogPimp);
    }

}
