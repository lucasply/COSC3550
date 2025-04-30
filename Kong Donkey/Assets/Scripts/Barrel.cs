using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [Header("Barrel Settings")]
    public float fallChance = 0.2f;
    public float fallSpeed = 2f;    
    
    private bool isFalling = false; // Flag to check if the barrel is falling

    private Rigidbody2D rb;

    void Start(){
        
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found on barrel!");
        }
        else
        {
            Debug.Log("Rigidbody2D assigned to barrel: " + gameObject.name);
        }

    }

    void Update()
    {
        // Check if the barrel is falling and apply falling speed
        if (isFalling)
        {
            rb.velocity = new Vector2(0, -fallSpeed);
        }
    }

    // Collision loggic for barrel
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("LadderTopTrigger")){
            if(Random.value < fallChance){
                Debug.Log("Barrel is falling!");
                isFalling = true;
                gameObject.layer = LayerMask.NameToLayer("Ladder");
            }
        }
        else if(other.CompareTag("LadderBottomTrigger")){
            isFalling = false;
            gameObject.layer = LayerMask.NameToLayer("Objects"); // Reset the layer to default when it reaches the bottom
        }
    }


}
