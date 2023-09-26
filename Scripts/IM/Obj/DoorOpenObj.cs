using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenObj : MonoBehaviour
{
    public int carkey;
    Animator animator;
    AudioSource audio;
    public AudioClip [] clip;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ItemPickUp>() != null && other.GetComponent<ItemPickUp>().item.itemType == Item.ItemType.Exititem)
        {
            switch (other.GetComponent<ItemPickUp>().item.itemName)
            {
                case "Key":
                    other.GetComponent<ItemPickUp>().item.exititemUse = 1;
                    carkey = 1;
                    audio.PlayOneShot(clip[0], 1f);
                    break;
            }

            //탈출 조건 충족했는지 확인하는 함수
            Open();
        }
    }

    void Start()
    {
        carkey = 0;
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    void Open()
    {
        if (carkey > 0)
        {
            Debug.Log("exit");
            animator.SetBool("Open", true);
            audio.PlayOneShot(clip[1], 1f);
            
        }
    }
}
