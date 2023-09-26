using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Unity.VisualScripting;
using Cursor = UnityEngine.Cursor;

public class PuzzleDoor : MonoBehaviour
{
    Animator anim;
    AudioSource audio;
    [SerializeField] private Text Text;
    string codeValue = "";
    public string Code;
    public GameObject keypad;
    public AudioClip keypadAudio;
    bool ispanel;


    void Start()
    {
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        anim.SetBool("Open", false);

    }

    void Update()
    {
        Text.text = codeValue;

        if (Input.GetKeyDown(KeyCode.F) && ispanel == true)
        {
            keypad.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }

        if (codeValue == Code)
        {
            Dooropen();
            keypad.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            StartCoroutine(dooraudio());
        }
        if (codeValue.Length >= 5)
        {
            codeValue = "";
        }





    }

    public void Dooropen()
    {
        anim.SetBool("Open", true);
    }

   /* private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            ispanel = true;
        }
        
    }*/

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            ispanel = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            ispanel = false;
        }
    }

    public void digit(string digit)
    {
        codeValue += digit;
    }

    public void Exit()
    {
        keypad.SetActive(false);
    }

    public void Clear()
    {
        codeValue = " ";
    }

    IEnumerator dooraudio()
    {
        audio.PlayOneShot(keypadAudio, 1f);
        yield return new WaitForSeconds(1);
        audio.Stop();
        audio.enabled = false;

    }
}
