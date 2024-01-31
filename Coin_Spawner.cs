using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Spawner : MonoBehaviour
{
    public GameObject Coins;
    public Transform Enemy_pos;

    private void Start()
    {
        Enemy_pos = GetComponent<Transform>();
    }
    public void Spawn()
    {
        GameObject Coin = Instantiate(Coins, Enemy_pos.position, Enemy_pos.rotation) as GameObject;
        Destroy(Coin, 8f);
    }
}
