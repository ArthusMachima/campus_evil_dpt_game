using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public bool doSpawn;
    [Header("Spawner")] //Arthus
    [SerializeField] GameObject[] EnemyTypes;
    float spawnTime;
    [SerializeField] float spawnRate = 3;


    private void Update()
    {
        //Spawn Timer
        if (doSpawn)
        {
            spawnTime += Time.deltaTime;
            if (spawnTime > spawnRate)
            {
                spawnTime = 0;
                Spawn();
            }
        }
    }

    //Spawn Function
    public void Spawn()
    {
        if (!doSpawn) return;
        int index = Random.Range(0, EnemyTypes.Length);
        Instantiate(EnemyTypes[index], transform.position, Quaternion.identity);
    }





}
