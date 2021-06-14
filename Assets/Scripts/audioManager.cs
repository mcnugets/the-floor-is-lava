using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour {

    public AudioClip scoreSoundeffect;
    public AudioClip jumpingSoundEffct;
    public AudioClip deathLavaSound;
    public AudioClip annoyingassMusic;

    public static  AudioSource scoreAudioSource;
    public static AudioSource jumpingAudioSource;
    private AudioSource deathLavaAudioSource;
    private AudioSource gameoverMusic;

    public characterController2D controller;
    private bool isgameover = false;
    private bool anotheruselessbool = false;
  



    private AudioSource AddThatAudio(AudioClip newclip, float volume, bool loop, bool awake)
    {
        AudioSource newaudio = gameObject.AddComponent<AudioSource>();

        newaudio.clip = newclip;
        newaudio.volume = volume;
        newaudio.loop = loop;
        newaudio.playOnAwake = awake;


        return newaudio;
    }

	// Use this for initialization
	void Start () {

        scoreAudioSource = AddThatAudio(scoreSoundeffect, 0.7f, false, false);
        jumpingAudioSource = AddThatAudio(jumpingSoundEffct, 0.7f, false, false);
        deathLavaAudioSource = AddThatAudio(deathLavaSound, 0.7f, false, false);
        gameoverMusic = AddThatAudio(annoyingassMusic, 1.0f, true, false);
       
      //  audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        managingsounds();


        //if player has gotten the score play aqnnother sound

        //if  player has jumped play the thifd sound

    }
    void managingsounds()
    {
        


        //if playe is dead play this sound
        if (Player.isDead && !isgameover)
        {
            if (!deathLavaAudioSource.isPlaying)
            {
                isgameover = true;
                deathLavaAudioSource.Play();


            }

        }

        if (scoremanager.gameover)
        {
            Debug.Log("FUCKING TRUE");
            if (!gameoverMusic.isPlaying)
            {
                gameoverMusic.Play();
            }
        }
    }
}
