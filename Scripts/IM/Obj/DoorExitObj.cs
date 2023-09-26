
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorExitObj : MonoBehaviour
{
    public int doorhandle;
    AudioSource audioSource;
    public AudioClip [] clip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ItemPickUp>() != null && other.GetComponent<ItemPickUp>().item.itemType == Item.ItemType.Exititem)
        {
            switch (other.GetComponent<ItemPickUp>().item.itemName)
            {
                case "DoorHandle":
                    other.GetComponent<ItemPickUp>().item.exititemUse = 1;
                    doorhandle = 1;
                    audioSource.PlayOneShot(clip[0], 1f);
                    audioSource.loop = false;
                    break;
            }

            //탈출 조건 충족했는지 확인하는 함수
            Exit();
        }

        
    }
    
    void Start()
    {
        doorhandle = 0;
        audioSource = GetComponent<AudioSource>();
    }

    void Exit()
    {
        if (doorhandle > 0)
        {
            Debug.Log("exit");
            GameManager.instance.ending = true;
            GameManager.instance.enddingState = 1;
            StartCoroutine(exitdoor());
            //탈출 성공 이미지 활성화 && 타이틀씬 로드
        }
    }

    IEnumerator exitdoor()
    {
        yield return new WaitForSeconds(1f);
        audioSource.PlayOneShot(clip[1], 1f);
    }
}
