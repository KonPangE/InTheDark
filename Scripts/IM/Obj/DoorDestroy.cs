using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDestroy : MonoBehaviour
{

    public int fueltank;
    public int matchbox;
    public ParticleSystem particle;
    public AudioClip [] clip;
    AudioSource audioSource;



    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ItemPickUp>() != null && other.GetComponent<ItemPickUp>().item.itemType == Item.ItemType.Exititem)
        {
            switch (other.GetComponent<ItemPickUp>().item.itemName)
            {
                case "Fueltank":
                    other.GetComponent<ItemPickUp>().item.exititemUse = 1;
                    fueltank = 1;
                    audioSource.PlayOneShot(clip[0],1f);
                    break;
                case "MatchBox":
                    other.GetComponent<ItemPickUp>().item.exititemUse = 1;
                    matchbox = 1;
                    audioSource.PlayOneShot(clip[1],1f);
                    break;
            }

            
            Fire();
        }


    }
    // Start is called before the first frame update
    void Start()
    {
        fueltank = 0;
        matchbox = 0;
        audioSource = GetComponent<AudioSource>();
    }

    void Fire()
    {
        if(fueltank > 0 && matchbox > 0 && !particle.isPlaying)
        {
            Debug.Log("Destory");
            particle.Play();
            audioSource.PlayOneShot(clip[2], 1f);
            Destroy(gameObject,5f);
            
        }
        
    }
}
