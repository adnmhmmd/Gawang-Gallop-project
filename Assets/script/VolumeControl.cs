using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VolumeControl : MonoBehaviour
{
    public Slider sVolumeMusic;
    
    public AudioSource asMusic;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VolumeMusic()
    {
        asMusic.volume = sVolumeMusic.value;
        
    }
}
