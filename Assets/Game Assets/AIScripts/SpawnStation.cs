using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class SpawnStation : MonoBehaviour
{
    [Header("zombie Spawner")]
    public Transform zombieSpawnpoint;
    public GameObject zombiePrefab;
    public int poolSize = 10;

    public Queue<GameObject> zombiePool = new Queue<GameObject>();

    private void Awake()
    {
        ZombieSpawn();
    }

    void ZombieSpawn()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject zombiePreb = Instantiate(zombiePrefab);
            zombiePreb.SetActive(false);
            zombiePool.Enqueue(zombiePreb);
        }
    }

    GameObject GetZombie(Vector3 pos, Quaternion rot)
    {
        GameObject zombieToSpawn = zombiePool.Dequeue();
        zombieToSpawn.transform.position = pos;
        zombieToSpawn.transform.rotation = rot;
        zombieToSpawn.SetActive(true);
        zombiePool.Enqueue(zombieToSpawn);
        return zombieToSpawn;
    }



    public void SpawnTriggerSpawn()
    {
        if(zombiePool.Count > 0)
        {
            GetZombie(zombieSpawnpoint.position,zombieSpawnpoint.rotation);
        }
    }
}
