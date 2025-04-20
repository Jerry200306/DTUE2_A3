using UnityEngine;

public class EnemyAI : MonoBehaviour
{
     
    public float moveSpeed = 5f;

   
    public float detectionDistance = 10f;


    public int damageToPlayer = 20;

     
    public AudioClip hitSound;

      
    public float soundVolume = 1f;

    
    private Transform player;

    
    private AudioSource audioSource;

    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").transform;

        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        
        if (player != null && Vector3.Distance(transform.position, player.position) <= detectionDistance)
        {
            Vector3 moveDirection = player.position - transform.position;
            moveDirection.Normalize();
            transform.position += moveDirection * moveSpeed * Time.deltaTime; 

            
             
        }
         
    }

    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.collider.CompareTag("Player"))
        {
            
            if (hitSound != null)
            {
                audioSource.PlayOneShot(hitSound, soundVolume);
            }

            
            PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();

            
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageToPlayer);
            }
            else
            {
                
                Debug.LogError("Player object collided with but does not have PlayerHealth component!");
            }
        }
    }
}