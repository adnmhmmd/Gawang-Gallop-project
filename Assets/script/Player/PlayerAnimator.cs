using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayAnimation()
    {
        // Aktifkan animasi saat tombol "Begin" diklik
        animator.enabled = true;
        // Panggil method trigger untuk memulai animasi tertentu
        animator.SetTrigger("Playerfirst");
    }


}
