using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject Enemy;
    public int id;

    void Update()
    {
        RandomSpawner gen_rand = GetComponentInParent<RandomSpawner>();
        if (gen_rand != null)
        {
            if (gen_rand.spawner_id == id){
                SpawnEnemys();
                gen_rand.spawner_id = 0;
            }
        } 
    }

    void SpawnEnemys()
    {
        Instantiate(Enemy, transform.position, transform.rotation);
    }
}