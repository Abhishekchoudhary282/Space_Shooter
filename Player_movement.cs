using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement : MonoBehaviour
{
    private float dealtaX, dealtaY;
    private Rigidbody2D rb;
    [SerializeField] float MaxX, MaxY;

    public Transform Firing_point_1;
    public GameObject Bullet_prefab;
    public SpriteRenderer Ship_sprite;

    public EdgeCollider2D Collider;

    public GameManager gameManager;

    [SerializeField] float fireRate;
    private float lastTimeFired;

    public AudioManager audioManager;
    public AudioSource audioSource;
    public AudioClip hitClip, shootClip;

    public GameObject ShipBlast1;
    public GameObject ShipFire;
    

    private void Start()
    {
        Time.timeScale = 1f;

        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        Collider = GetComponent<EdgeCollider2D>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        Ship_sprite = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        ClampthePlayer();
        Move();
        ShipBlast1.transform.position = transform.position;
        float ydes = 0.38f;
        ShipFire.transform.position = new Vector2(transform.position.x, transform.position.y - ydes);
    }
    void Move()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            Shoot();

            switch (touch.phase)
            {
                case TouchPhase.Began:             
                    dealtaX = touchPos.x - transform.position.x;
                    dealtaY = touchPos.y - transform.position.y;
                    break;

                case TouchPhase.Moved:
                    rb.MovePosition(new Vector2(touchPos.x - dealtaX, touchPos.y - dealtaY));
                    break;

                case TouchPhase.Ended:
                    rb.velocity = Vector2.zero;
                    break;
            }
        }
    }
    void ClampthePlayer()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -MaxX, MaxX),
            Mathf.Clamp(transform.position.y, -MaxY, MaxY), transform.position.z);
            
    }
    void Shoot()
    {
        if (Time.time >= lastTimeFired + (1 / fireRate))
        {
            GameObject Bullet_1 = Instantiate(Bullet_prefab, Firing_point_1.position, Firing_point_1.rotation);
            audioSource.clip = shootClip;
            audioSource.playOnAwake = true;
            audioSource.loop = false;
            audioSource.Play();
            lastTimeFired = Time.time;
            Destroy(Bullet_1, gameManager.Destory_bullets_after_seconds);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            ShipBlast1.SetActive(true);
            audioSource.clip = hitClip;
            audioSource.playOnAwake = true;
            audioSource.loop = false;
            audioSource.Play();
            Ship_sprite.enabled = false;
            Destroy(ShipFire);
            StartCoroutine(GameOver());

            gameManager.player_Movement.enabled = false;
            gameManager.enemy_Spawn.enabled = false;
            Collider.enabled = false;

        }
    }
    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1);
        
        gameManager.GameOverPanel.SetActive(true);
        audioManager.GameOver();
        Destroy(gameObject);
        

    }
}