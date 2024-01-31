using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip gamePlayClip, GameOverClip ,levelCompletedClip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = gamePlayClip;
        audioSource.playOnAwake = true;
        audioSource.volume = 0.6f;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void GameOver()
    {
        audioSource.clip = GameOverClip;
        audioSource.playOnAwake = true;
        audioSource.loop = false;
        audioSource.Play();
    }
    public void LevelCompleted()
    {
        audioSource.clip = levelCompletedClip;
        audioSource.playOnAwake = true;
        audioSource.loop = true;
        audioSource.Play();
    }
}
