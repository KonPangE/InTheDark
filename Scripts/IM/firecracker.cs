using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firecracker : MonoBehaviour
{
    Item item;
    AudioSource audio;
    public GameObject fire;
    public ParticleSystem particle;
    public Transform cam;
    public AudioClip clip;

    public float throwForce = 10f; //������ ��
    public float throwTorque = 3f;// ������ ����

    public float throwdown = 0.1f; //������ �� ����

    bool throwready; //���� �� �ִ��� Ȯ��
    Rigidbody rb;


    private void OnEnable()
    {
        cam = GameManager.instance.camPos;

        transform.position = cam.position;
        transform.rotation = cam.rotation;
    }
    private void Start()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        throwready = true;
        if (!particle.isPlaying)
        {
            transform.position = cam.position;
            transform.rotation = cam.rotation;
            particle.Play();
            audio.PlayOneShot(clip, 8f);
            Throw();
            
        }
        Destroy(this.gameObject,9f);

    }
   
    void Throw()
    {
        throwready = false;

        //���� ������Ʈ ����
        //GameObject firecracker = Instantiate(fire);

        //�����ִ� �������� ������
        Vector3 force = (cam.transform.forward * throwForce) + (transform.up * throwTorque);
        rb.AddForce(force, ForceMode.Impulse);

        Invoke(nameof(ResetThrow), throwdown);

    }
    void ResetThrow()
    {
        throwready = true;
    }

   

}
