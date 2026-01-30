using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    // Préfab(s) à assigner dans l’Inspector
    public GameObject[] witches;
    public GameObject[] witches2;
    public GameObject[] witches3;
    private PlayerController playerController;
    public int CONVICTION_THRESHOLD_1 = 20;
    public int CONVICTION_THRESHOLD_2 = 60;
    // public int CONVICTION_THRESHOLD_3 = 20;

    public int id;
    private int rand;
    void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        RandomSpawner gen_rand = GetComponentInParent<RandomSpawner>();
        if (gen_rand != null && gen_rand.spawner_id == id)
        {
            rand = Random.Range(0, witches.Length);
            // Debug.Log(rand);
            SpawnEnemyAtIndex(rand);
            gen_rand.spawner_id = 0;
        }
    }

    void SpawnEnemyAtIndex(int index)
    {
        // Debug.Log(witches[index]);
        int playerConviction = playerController.conviction;
        if (playerConviction < CONVICTION_THRESHOLD_1)
            Instantiate(witches[index], transform.position, transform.rotation);
        else if (playerConviction >= CONVICTION_THRESHOLD_1 && playerConviction < CONVICTION_THRESHOLD_2)
            Instantiate(witches2[index], transform.position, transform.rotation);
        else if (playerConviction >= CONVICTION_THRESHOLD_2)
            Instantiate(witches3[index], transform.position, transform.rotation);
    }
}
