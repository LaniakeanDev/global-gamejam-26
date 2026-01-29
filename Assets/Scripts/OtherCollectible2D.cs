using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherCollectible2D : MonoBehaviour
{

    public float rotationSpeed = 0.2f;
    public GameObject onCollectEffect;
    public int direction = 1;
    public int frame = 0;

    // Update is called once per frame
    void Update()
    {
        if (frame%3 == 0) 
            transform.Translate(0, direction * rotationSpeed, 0);
        frame++;
        if (frame >= 99){
            frame = 0;
            direction *= -1;
        }
        
    }

    private void OnTriggerStay2D(Collider2D other) {
        
         // Check if the other object has a PlayerController2D component
        if (other.GetComponent<PlayerController2D>() != null) {
            
            // Destroy the collectible
            Destroy(gameObject);

            // Instantiate the particle effect
            Instantiate(onCollectEffect, transform.position, transform.rotation);
        }
    }
}


