using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawn : MonoBehaviour
{
    public GameObject[] enemies;
    public float spawn_in_sec;
    public double timer = 0;
    public int No_of_enemies;

    private void Update()
    {
        if (timer < spawn_in_sec)
        {
            timer += Time.deltaTime;
        }
        else
        {
            Spawn();
            timer = 0f;
        }
    }
    public void Spawn()
    {
        GameObject Enemy_Spawned = Instantiate(enemies[Random.Range(0, No_of_enemies)], 
            new Vector3(Random.Range(-1.5f,1.5f),5.3f,0f), transform.rotation) as GameObject;

        Destroy(Enemy_Spawned, 6f);
    }
}