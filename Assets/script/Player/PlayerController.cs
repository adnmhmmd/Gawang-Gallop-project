using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float for_speed;
    public Transform center_pos;
    public Transform left_pos;
    public Transform right_pos;
    private int current_pos;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //current 0 = center, 1 = left, 2 = right
        current_pos = 0;
        Physics.IgnoreLayerCollision(3, 6);
    }
    void Awake()
    {
        enabled = false; // Matikan script saat game dimulai
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + for_speed * Time.deltaTime, transform.position.y, transform.position.z);
        if (current_pos == 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -45f);
        }
        else if (current_pos == 1)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -35f);
        }
        else if (current_pos == 2)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -55f);
        }

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

    }

    public void StartMovement()
    {
        enabled = true; // Mengaktifkan pembaruan skrip untuk memulai pergerakan pemain

        // Atur kecepatan pergerakan pemain sesuai yang telah ditentukan
        for_speed = 20.0f; // Misalnya, di sini kita atur kecepatan pemain menjadi 5 unit per detik
    }

    public void BeginGame()
    {
        Invoke("StartMovement", 4f); // Panggil fungsi StartMovement setelah penundaan 4 detik
    }



}
