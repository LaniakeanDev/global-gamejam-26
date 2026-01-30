using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    // Préfab(s) à assigner dans l’Inspector
    public GameObject[] witches;

    public int id;
    private int rand;

    void Update()
    {
        RandomSpawner gen_rand = GetComponentInParent<RandomSpawner>();

        if (gen_rand != null && gen_rand.spawner_id == id)
        {
            rand = Random.Range(0, witches.Length);
            Debug.Log(rand);
            SpawnEnemyAtIndex(rand);
            gen_rand.spawner_id = 0;
        }
    }

    void SpawnEnemyAtIndex(int index)
    {
        Debug.Log(witches[index]);
        Instantiate(witches[index], transform.position, transform.rotation);
    }
}
