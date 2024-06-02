using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSFX : MonoBehaviour
{
    public Slider sVolumeSFX;
    public AudioSource asSFX;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VolumeMusic()
    {
        asSFX.volume = sVolumeSFX.value;
    }
}
