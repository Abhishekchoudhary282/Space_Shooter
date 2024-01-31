using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public TextMeshPro current_hit;
    public int Total_hit;
    public GameManager gameManager;
    public Coin_Spawner coinSpawn;

    public AudioSource audioSource;
    public AudioClip hitClip;

    public GameObject Enemy_blast;
    public SpriteRenderer Enemy_Sprite;
    public BoxCollider2D enemy_collider;
    public bool enemy_is_alive = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coinSpawn = GetComponent<Coin_Spawner>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        rb.velocity = gameManager.Enemy_Speed * -(transform.up);
        audioSource = GetComponent<AudioSource>();
        Enemy_Sprite = GetComponent<SpriteRenderer>();
        enemy_collider = GetComponent<BoxCollider2D>();

    }
    private void Update()
    {
        current_hit.text = Total_hit.ToString();
        if(enemy_is_alive)
        {
            Enemy_blast.transform.position = transform.position;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            
            Total_hit -= 1;
            if(Total_hit == 0)
            {
                enemy_is_alive = false;
                Enemy_blast.SetActive(true);
                
                audioSource.clip = hitClip;
                audioSource.playOnAwake = true;
                audioSource.loop = false;
                audioSource.Play();

                enemy_collider.enabled = false;
                Enemy_Sprite.enabled = false;

                StartCoroutine(Enemy_Destroyed());

                coinSpawn.Spawn();
            }
        }
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
    IEnumerator Enemy_Destroyed()
    {
        yield return new WaitForSeconds(1);

        Destroy(gameObject);

    }
}