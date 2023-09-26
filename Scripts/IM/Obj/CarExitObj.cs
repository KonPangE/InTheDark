using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CarExitObj : MonoBehaviour
{
    public int carKey;
    public int book;
    public int fueltank;
    public AudioClip[] caraudio;
    AudioSource audioSource;

    public delegate void CarExitEnding();

    public static CarExitEnding carExitEnding;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<ItemPickUp>() != null && other.GetComponent<ItemPickUp>().item.itemType == Item.ItemType.Exititem)
        {
                switch(other.GetComponent<ItemPickUp>().item.itemName)
                {
                    case "Key":
                        other.GetComponent<ItemPickUp>().item.exititemUse = 1;
                        carKey = 1;
                        audioSource.PlayOneShot(caraudio[2], 1f);
                        
                        break;
                    case "Book":
                        other.GetComponent<ItemPickUp>().item.exititemUse = 1;
                        book = 1;
                            audioSource.PlayOneShot(caraudio[1], 1f);
                    break;
                    case "Fueltank":
                        other.GetComponent<ItemPickUp>().item.exititemUse = 1;
                        fueltank = 1;
                        audioSource.PlayOneShot(caraudio[0], 1f);
                        audioSource.loop = false;
                    break;
                }
            
            //Ż�� ���� �����ߴ��� Ȯ���ϴ� �Լ�
            Exit();
        }
    }
    
    void Start()
    {
        carKey = 0;
        book = 0;
        fueltank = 0;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Exit()
    {
        if(carKey > 0 && book > 0 && fueltank > 0) 
        {
            GameManager.instance.ending = true;
            GameManager.instance.enddingState = 0;
            Debug.Log("exit");
            //���� ���
            StartCoroutine(carexit());
            //�ڵ��� Ż�� ���� �̹��� Ȱ��ȭ && Ÿ��Ʋ�� �ε�
            
        }
    }

    IEnumerator carexit()
    {
        yield return new WaitForSeconds(3);
        audioSource.PlayOneShot(caraudio[3], 1f);
    }
}
