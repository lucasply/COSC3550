using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    // Sound effect variables
    public List<AudioClip> breaking = new List<AudioClip>();
    private AudioSource source;

    [Header("Barrel Settings")]
    public float fallChance = 0.2f;
    public float fallSpeed = 2f;    
    
    private bool isFalling = false; // Flag to check if the barrel is falling

    private Rigidbody2D rb;
    private Animator anim;

    void Start(){
        source = GetComponent<AudioSource>();
        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
                anim.Play(stateName: "BarrelFall");
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                transform.rotation = Quaternion.Euler(0,0,0);

            }
        }
        else if(other.CompareTag("LadderBottomTrigger")){
            Debug.Log("Barrel has reached the bottom!");
            isFalling = false;
            gameObject.layer = LayerMask.NameToLayer("Objects"); // Reset the layer to default when it reaches the bottom
            anim.Play(stateName: "BarrelRoll");
            rb.constraints = RigidbodyConstraints2D.None;

        }
        else if(other.CompareTag("Fire"))
        {
            Debug.Log("Barrel reached the fire");
            destroyBarrel();
        }
    }

    private destroyBarrel() {
        AudioClip clip = breaking[Random.Range(0, breaking.Count)];
        source.PlayOneShot(clip);

        // Actually destroy object
        Destroy(this.gameObject);
    }

}
