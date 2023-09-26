using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WellExitObj : MonoBehaviour
{
    public int Flashlight;
    public int airtank;
    public int rope;

    public AudioClip [] wellcilp;
    AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ItemPickUp>() != null && other.GetComponent<ItemPickUp>().item.itemType == Item.ItemType.Exititem)
        {
            switch (other.GetComponent<ItemPickUp>().item.itemName)
            {
                case "Flashlight":
                    other.GetComponent<ItemPickUp>().item.exititemUse = 1;
                    Flashlight = 1;
                    audioSource.PlayOneShot(wellcilp[0]);
                    break;
                case "airtank":
                    other.GetComponent<ItemPickUp>().item.exititemUse = 1;
                    airtank = 1;
                    audioSource.PlayOneShot(wellcilp[1]);
                    audioSource.SetScheduledEndTime(2);
                    break;
                case "Rope":
                    other.GetComponent<ItemPickUp>().item.exititemUse = 1;
                    rope = 1;
                    audioSource.PlayOneShot(wellcilp[2]);
                    audioSource.SetScheduledEndTime(0);
                    break;
            }

            //Ż�� ���� �����ߴ��� Ȯ���ϴ� �Լ�
            Exit();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Flashlight = 0;
        airtank = 0;
        audioSource = GetComponent<AudioSource>();
    }

    void Exit()
    {
        if (Flashlight > 0 && airtank > 0 && rope > 0)
        {
            Debug.Log("exit");
            GameManager.instance.ending = true;
            GameManager.instance.enddingState = 2;
            //�ڵ��� Ż�� ���� �̹��� Ȱ��ȭ && Ÿ��Ʋ�� �ε�
            StartCoroutine(wellsound());
           

        }
    }

    IEnumerator wellsound()
    {
        yield return new WaitForSeconds(2f);
        audioSource.PlayOneShot(wellcilp[3], 0.8f);
        yield return new WaitForSeconds(1f);
        audioSource.PlayOneShot(wellcilp[4], 0.8f);
        audioSource.loop = false;

    }

}
