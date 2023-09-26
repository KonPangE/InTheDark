using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class mirror : MonoBehaviour
{

    public Transform player;
    public GameObject _mirror;
    public Image image;
    public AudioClip clip;
    AudioSource audio;
    Color color;

    bool isused = false;
    private void Start()
    {
        color = Color.white;
        color.a = 0f;
        image = GetComponentInChildren<Image>();
        image.color = color;
        audio = GetComponentInChildren<AudioSource>();

        player = GameManager.instance.camPos.transform.parent.gameObject.transform;
        if (!isused)
        {
            //�������� ȣ��
            SpwanPoints();
            //���̵��� �ڷ�ƾ ȣ��
            StartCoroutine(FadeIn());
            audio.PlayOneShot(clip, 3f);
        }
    }
    

    //�÷��̾� ���� ����Ʈ���� ���� ����
    void SpwanPoints()
    {
       Transform[]spawnpoint = GameObject.Find("PlayerspawnpointGroup").GetComponentsInChildren<Transform>();
       int point = Random.Range(1, spawnpoint.Length);
       player.transform.position = spawnpoint[point].transform.position;
       
    }

    //ȭ�� ���̵� ��
    IEnumerator FadeIn()
    {
            while(color.a < 1) 
            {
                color.a += 0.1f;
                yield return new WaitForSeconds(0.05f);
                image.color = color;
            }
      

        //���̵� �ƿ� �ڷ�ƾȣ��(���̵��ο��� �ٷ� ���̵�ƿ�)
        StartCoroutine(FadeOut());

    }
   
    //ȭ�� ���̵� �ƿ�
    IEnumerator FadeOut()
    {
        color.a = 1f;
        while(color.a > 0) 
        {
            color.a -= 0.1f;
            yield return new WaitForSeconds(0.05f);
            image.color = color;
        }

    }
    
   
}