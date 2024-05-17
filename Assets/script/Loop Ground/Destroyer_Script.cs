using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer_Script : MonoBehaviour
{
    public float distanceThreshold = 1000f; // Jarak setelah objek dihancurkan
    private Transform player;

    void Start()
    {
        // Mengasumsikan bahwa objek pemain memiliki tag "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) > distanceThreshold)
        {
            Destroy(gameObject);
        }
    }
}
