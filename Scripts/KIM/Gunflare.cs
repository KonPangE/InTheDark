using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Gunflare : MonoBehaviour
{
    public GameObject flarebulletPrefab;
    public Transform barreEnd;
    public GameObject MuzzleParticle;
    public AudioClip flareclip;

    Animation anim;
    AudioSource audio;

    public int maxBullet = 1; //�ִ� �Ѿ� ����
    public int remainbullet = 1;//���� �Ѿ� ����
    public int currenbullet = 1;//źâ���ִ� �Ѿ� ����


    public bool isfire = false;

    int enemyLayer;
    int exitobjLayer;
    int layerMask;

    private void OnEnable()
    {
        currenbullet = 1;
    }

    void Start()
    {
        anim = GetComponent<Animation>();
        audio = GetComponent<AudioSource>();

        enemyLayer = LayerMask.NameToLayer("ENEMY");
        exitobjLayer = LayerMask.NameToLayer("Fabric");

        layerMask = 1 << enemyLayer | 1 << exitobjLayer;

    }


    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!anim.isPlaying)
            {
                if (currenbullet > 0)
                {
                    Fire();
                    audio.PlayOneShot(flareclip,2f);
                    gameObject.SetActive(false);

                }
                else
                {
                    anim.Play("noAmmo");
                }
            }
        }



       

    }


    void Fire()
    {
        currenbullet--;
        if (currenbullet == 0)
        {
            currenbullet = 0;
        }
        anim.CrossFade("Shoot");
        Instantiate(flarebulletPrefab, barreEnd.position, barreEnd.rotation);
        GameObject a = Instantiate(MuzzleParticle, barreEnd.position, barreEnd.rotation);
        Destroy(a, 0.3f);
    }

}
