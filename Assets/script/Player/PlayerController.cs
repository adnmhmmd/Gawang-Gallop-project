using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float for_speed = 5f;
    public float side_speed = 5f;
    public float jump_force = 5f; // Gaya lompat
    public Transform center_pos;
    public Transform left_pos;
    public Transform right_pos;
    private int current_pos;
    public Rigidbody rb;
    private bool isGrounded; // Variabel untuk memeriksa apakah pemain di tanah
    private Animator animator; // Referensi ke Animator
    private bool isCollided = false; // Menandakan jika pemain telah bertabrakan dengan obstacle

    void Start()
    {
        // current 0 = center, 1 = left, 2 = right
        current_pos = 0;
        Physics.IgnoreLayerCollision(3, 6);
        isGrounded = true; // Pemain di tanah saat memulai
        animator = GetComponent<Animator>(); // Inisialisasi referensi ke Animator
        rb = GetComponent<Rigidbody>(); // Inisialisasi referensi ke Rigidbody

        // Pastikan Rigidbody memiliki pengaturan yang benar
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Awake()
    {
        enabled = false; // Matikan script saat game dimulai
    }

    void Update()
    {
        if (isCollided)
            return; // Jangan update jika pemain telah bertabrakan

        // Update posisi ke depan
        transform.position += new Vector3(for_speed * Time.deltaTime, 0, 0);

        // Update posisi samping berdasarkan current_pos
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

        // Input untuk mengubah current_pos
        if (Input.GetKeyDown(KeyCode.LeftArrow))
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

        if (Input.GetKeyDown(KeyCode.RightArrow))
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

        // Input untuk lompat
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jump_force, ForceMode.Impulse);
        isGrounded = false; // Pemain tidak di tanah setelah melompat
        animator.SetBool("isJumping", true); // Set parameter isJumping di Animator
    }

    // Mengatur isGrounded ketika karakter menyentuh tanah
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Pemain di tanah jika bertabrakan dengan objek ber-tag "Ground"
            animator.SetBool("isJumping", false); // Reset parameter isJumping di Animator
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            HandleCollisionWithObstacle(); // Panggil fungsi penanganan tabrakan
        }
    }

    void HandleCollisionWithObstacle()
    {
        isCollided = true; // Set status tabrakan
        animator.SetTrigger("Fall"); // Mainkan animasi jatuh
        for_speed = 0; // Hentikan pergerakan maju dengan mengatur for_speed ke 0
        rb.velocity = Vector3.zero; // Hentikan semua gerakan
        rb.isKinematic = true; // Matikan fisika sementara
    }

    IEnumerator EndGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Tunggu selama delay yang ditentukan
        EndGame(); // Panggil fungsi untuk mengakhiri permainan
    }

    void EndGame()
    {
        // Lakukan apa pun yang diperlukan untuk mengakhiri permainan di sini
        Debug.Log("Game Over!"); // Contoh: Cetak pesan "Game Over!" ke konsol
        // Tambahan: Mungkin Anda ingin memuat ulang level atau menampilkan layar game over
    }

    public void StartMovement()
    {
        enabled = true; // Mengaktifkan pembaruan skrip untuk memulai pergerakan pemain

        // Atur kecepatan pergerakan pemain sesuai yang telah ditentukan
        for_speed = 40.0f; // Misalnya, di sini kita atur kecepatan pemain menjadi 5 unit per detik
    }

    public void BeginGame()
    {
        Invoke("StartMovement", 3f); // Panggil fungsi StartMovement setelah penundaan 3 detik
    }
}