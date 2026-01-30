using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float spawnRate;
    public int maxQuantity;
    public int counter = 0;
    private float timer = 0;
    public int spawner_id = 0;
    void Update()
    {
        if(timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
            spawner_id = 0;
        }
        else
        {
            if (counter < maxQuantity){
                spawner_id = Random.Range(1, 14);
                counter++;
            }
            timer = 0;
        }
    }

    
}
