using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class amulet : MonoBehaviour
{
    
    //public EnemyAI enemyAI;
    public int laymask;// ���̾� ����
    public Collider[] coll;
    AudioSource audio;
    public AudioClip clip;

    Transform Cam;

    private void Awake()
    {
        laymask = LayerMask.NameToLayer("ENEMY");
        Cam = GameManager.instance.camPos;
        transform.position = Cam.position;
        audio = GetComponent<AudioSource>();
    }
    void Start()
    {
        StartCoroutine(Stun());
       
    }


    IEnumerator Stun()
    {
        yield return null;
        coll = Physics.OverlapSphere(transform.position, 10f,  1 << laymask);
        audio.PlayOneShot(clip, 1f);

        if(coll.Length > 0)
        {
            EnemyAI enemyAI = coll[0].GetComponent<EnemyAI>();
            enemyAI.isStun = 1;
        }
       
      
        yield return new WaitForSecondsRealtime(1f);
        Destroy(gameObject);
    }


}
