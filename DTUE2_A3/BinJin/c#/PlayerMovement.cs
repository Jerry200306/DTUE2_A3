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
    private Rigidbody rb;
    private int nowState;
    private int prewState;


    private CharacterController controller;
    public Animator animator;
    private Vector3 moveDirection;
    private float verticalVelocity;
    private float jumpheight;
    private bool isGrounded;

    public Slider healthSlider;
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = transform.GetComponent<Animator>();

        count = 0;
        winTextObject.SetActive(false);
        SetCountText();

        Bloon = 100;
        rb = GetComponent<Rigidbody>();
        jumpheight = 2f;
        verticalVelocity = -2f;
        isGrounded = true;


        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }
    private void changState(int state)
    {
        if (nowState == state)
        {
            prewState = nowState;
            nowState = state;
        }
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
            
        }
        if (collision.gameObject.CompareTag("Terrain"))
        {
            isGrounded = true;
            animator.SetBool("Jump",false);
        }

    }
    void Update()
    {
  
        isGrounded = controller.isGrounded;

        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f; 
            animator.SetBool("Jump", false);
            if (prewState == 1)
            {
                animator.SetBool("move", true);
            }
            else if (prewState == 0)
            {
                changState(0);
            }
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontal, 0, vertical).normalized;

 
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity = jumpForce;
            animator.SetBool("Jump", true);
            changState(2);
        }


        verticalVelocity += gravity * Time.deltaTime;

        Vector3 finalMove = moveDirection * moveSpeed;
        finalMove.y = verticalVelocity;

        if (moveDirection.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        controller.Move(finalMove * Time.deltaTime);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("move", true);
            changState(1);
        }
        else
        {
            animator.SetBool("move", false);
            changState(0);
        }
    }
}
