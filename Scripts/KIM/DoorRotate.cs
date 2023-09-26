using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRotate : MonoBehaviour
{
    public Animator anim;
    public AudioClip clip;
    AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag=="Player" && Input.GetKeyDown(KeyCode.F))
        {
             DoorOpen();
             source.PlayOneShot(clip,1f);
        }
        else 
        { 
            DoorClose();
        }

        if (other.CompareTag("ENEMY"))
        {
            DoorOpen();
        }

    }


    public void DoorOpen()
    {
        Debug.Log("¿­¸²");
        anim.SetBool("Open", true);
        anim.SetBool("Close", false);
    }

    public void DoorClose()
    {
        Debug.Log("´ÝÈû");
        anim.SetBool("Open", false);
        anim.SetBool("Close", true);
    }
}
