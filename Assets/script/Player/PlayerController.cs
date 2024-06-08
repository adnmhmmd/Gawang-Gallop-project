using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float for_speed = 5f;
    public float side_speed = 5f;
    public float jump_force = 5f;
    public Transform center_pos;
    public Transform left_pos;
    public Transform right_pos;
    private int current_pos;
    public Rigidbody rb;
    private bool isGrounded;
    private Animator animator;
    private bool isCollided = false;
    public GameObject gameover;
    public Text coinText;

    private Vector2 startTouchPosition, endTouchPosition;

    private int currentSessionCoinCount;
    private int totalCoinCount;

    public AudioClip coinSound; // Tambahkan AudioClip untuk koin
    private AudioSource audioSource; // Tambahkan AudioSource untuk memutar suara

    void Start()
    {
        current_pos = 0;
        Physics.IgnoreLayerCollision(3, 6);
        isGrounded = true;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        currentSessionCoinCount = 0;
        LoadTotalCoins();
        UpdateCoinUI();

        audioSource = GetComponent<AudioSource>(); // Inisialisasi AudioSource
    }

    void Awake()
    {
        enabled = false;
    }

    void Update()
    {
        if (isCollided)
            return;

        transform.position += new Vector3(for_speed * Time.deltaTime, 0, 0);

        if (current_pos == 0)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, center_pos.position.y, center_pos.position.z), side_speed * Time.deltaTime);
        }
        else if (current_pos == 1)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, left_pos.position.y, left_pos.position.z), side_speed * Time.deltaTime);
        }
        else if (current_pos == 2)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, right_pos.position.y, right_pos.position.z), side_speed * Time.deltaTime);
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;
                Swipe();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void Swipe()
    {
        Vector2 swipeDelta = endTouchPosition - startTouchPosition;

        if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
        {
            if (swipeDelta.x > 0)
            {
                MoveRight();
            }
            else
            {
                MoveLeft();
            }
        }
        else
        {
            if (swipeDelta.y > 0 && isGrounded)
            {
                Jump();
            }
        }
    }

    void MoveLeft()
    {
        if (current_pos == 0)
        {
            current_pos = 1;
        }
        else if (current_pos == 2)
        {
            current_pos = 0;
        }
    }

    void MoveRight()
    {
        if (current_pos == 0)
        {
            current_pos = 2;
        }
        else if (current_pos == 1)
        {
            current_pos = 0;
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jump_force, ForceMode.Impulse);
        isGrounded = false;
        animator.SetBool("isJumping", true);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Player is grounded!");
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            HandleCollisionWithObstacle();
        }
        else if (collision.gameObject.CompareTag("Coin"))
        {
            CollectCoin(collision.gameObject);
        }
    }

    void HandleCollisionWithObstacle()
    {
        isCollided = true;
        animator.SetTrigger("Fall");
        for_speed = 0;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;

        Invoke("ShowGameOverPanel", 3f);
    }

    void ShowGameOverPanel()
    {
        if (gameover != null)
        {
            gameover.SetActive(true);
        }
        else
        {
            Debug.LogError("Game Over Panel belum ditetapkan!");
        }
    }

    IEnumerator EndGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        EndGame();
    }

    void EndGame()
    {
        Debug.Log("Game Over!");
    }

    public void StartMovement()
    {
        enabled = true;
        for_speed = 40.0f;
    }

    public void BeginGame()
    {
        Invoke("StartMovement", 3f);
    }

    void CollectCoin(GameObject coin)
    {
        currentSessionCoinCount++;
        totalCoinCount++;
        SaveTotalCoins();
        Destroy(coin);
        UpdateCoinUI();
        Debug.Log("Coin collected! Total coins: " + totalCoinCount);

        // Memutar audio saat terkena koin
        if (coinSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(coinSound);
        }
    }

    void SaveTotalCoins()
    {
        PlayerPrefs.SetInt("TotalCoinCount", totalCoinCount);
    }

    void LoadTotalCoins()
    {
        totalCoinCount = PlayerPrefs.GetInt("TotalCoinCount", 0);
    }

    void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = currentSessionCoinCount.ToString();
        }
        else
        {
            Debug.LogError("Coin Text UI belum ditetapkan!");
        }
    }
}
