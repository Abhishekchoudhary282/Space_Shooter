using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int playerscore;
    public int total_Coins_to_Collect;
    public TextMeshProUGUI scoreText;
    public GameObject GameOverPanel;
    public GameObject WinPanel;
    public Enemy_Spawn enemy_Spawn;
    public Player_movement player_Movement;
    public AudioManager audioManager;
    public float Enemy_Speed;
    public float coin_Speed;
    public float Destory_bullets_after_seconds;
    public GameObject WinClip;

    private void Start()
    {
        Time.timeScale = 1f;

        playerscore = 0;
        scoreText.text = playerscore.ToString() + "/" + total_Coins_to_Collect;
        player_Movement = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_movement>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

    }
    private void Update()
    {
        PlayWinPanel();
        scoreText.text = playerscore.ToString() + "/" + total_Coins_to_Collect;
    }
    public void AddScore()
    {
        playerscore += 1;
        scoreText.text = playerscore.ToString() + "/" + total_Coins_to_Collect;
    }
    void PlayWinPanel()
    {
        if(playerscore == total_Coins_to_Collect)
        {
            WinClip.SetActive(true);
            audioManager.audioSource.Stop();
            WinPanel.SetActive(true);
            player_Movement.enabled = false;
            enemy_Spawn.enabled = false;
            Time.timeScale = 0f;

        }
    }
    public void Pause()
    {
        player_Movement.enabled = false;
        enemy_Spawn.enabled = false;
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        player_Movement.enabled = true;
        enemy_Spawn.enabled = true;
        Time.timeScale = 1f;
    }

}