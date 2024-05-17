using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundspawner : MonoBehaviour
{
    public Transform spawn_pos;
    public GameObject Tribun;
    public float Time_to_spawn;
    public float Time_btwn_wave;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > Time_to_spawn)
        {
            Spawn_Ground();
            Time_to_spawn = Time.time + Time_btwn_wave;
        }
    }

    public void Spawn_Ground()
    {
        // Instantiate objek dengan rotasi yang sama seperti empty object yang memiliki script ini
        Instantiate(Tribun, spawn_pos.position, spawn_pos.rotation);
        spawn_pos.position = new Vector3(spawn_pos.position.x + 574f, spawn_pos.position.y, spawn_pos.position.z);
    }

}
