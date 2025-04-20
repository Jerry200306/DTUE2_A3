using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private int count;//Count Gold
    private int Bloon;
    private float damage = 20;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float jumpForce = 20f;  
    public float gravity = -9.81f;

    public AudioSource coinSound;




    private CharacterController controller;
    private Animator animator;
    private Vector3 moveDirection;
    private float verticalVelocity;

    public Slider healthSlider;
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        count = 0;
        winTextObject.SetActive(false);
        SetCountText();

        Bloon = 100;


        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Bloon = (int)currentHealth; 
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Debug.Log("Player death£¡");
            
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gold"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        if (coinSound != null)
        {
            coinSound.Play();
        }
    }
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 2)
        {
            winTextObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //TakeDamage(20);
            // if (Bloon <= 0)
            //{
            if (collision.gameObject.CompareTag("Enemy"))
            {
                TakeDamage(20);
                if (Bloon <= 20)
                {
                    Destroy(gameObject);
                    winTextObject.gameObject.SetActive(true);
                    winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
                }
            }
            //}
        }

    }
    void Update()
    {
 
        bool isGrounded = controller.isGrounded;


        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        moveDirection = new Vector3(horizontal, 0, vertical).normalized;


        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            verticalVelocity = jumpForce;
            if (animator != null)
                animator.SetTrigger("Jump");
        }


        if (!isGrounded)
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
        else if (verticalVelocity < 0)
        {
            verticalVelocity = -0.1f; 
        }

 
        Vector3 finalMove = moveDirection * moveSpeed;
        finalMove.y = verticalVelocity;

 
        if (moveDirection.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        controller.Move(finalMove * Time.deltaTime);

     
        //if (animator != null)
        //{
        //    animator.SetBool("IsWalking", moveDirection.magnitude > 0.1f);
        //    animator.SetBool("IsGrounded", isGrounded);
        //}

    }
}
