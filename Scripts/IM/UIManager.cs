using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.UIElements;
//using Image = UnityEngine.UI.Image;

public class UIManager : MonoBehaviour
{
    public Sound UIsound;

    public Sprite[] endingImages;

    public Image fadeimage;
    float fadevalue = 0;
    AudioSource audio;

    public GameObject esPanel;

    int escape;
    public Image Panel;
    public GameObject endingImage;

    int a = 0;


    // Start is called before the first frame update
    void Start()
    {

        GameManager.instance.CreatTrapPooling();
        GameManager.instance.CreateExitOBJ();
        GameManager.instance.Load();
        GameManager.instance.ending = false;
        StartCoroutine(StartFadeOut());
        audio = GetComponent<AudioSource>();
        escape = 0;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        /*audio.clip = UIsound.audioClips[3];
        audio.Play();*/
        StartCoroutine(EndingFade());
    }

    private void OnEnable()
    {
        EnemyAI.OnTraceSoundEvent += this.SoundCheck;
        EnemyAI.OffSoundEvent += this.SoundOff;

    }
    private void OnDisable()
    {
        EnemyAI.OnTraceSoundEvent -= this.SoundCheck;
        EnemyAI.OffSoundEvent -= this.SoundOff;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && escape < 1)
        {
            escape = 1;
            Time.timeScale = 0;
            esPanel.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && escape > 0)
        {
            escape = 0;
            Time.timeScale = 1;
            esPanel.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }



    public void ReStartButtonClick()
    {
        audio.PlayOneShot(UIsound.audioClips[0], 1f);
        StartCoroutine(ReFadeOut());
    }

    public void ExitButton()
    {
        audio.PlayOneShot(UIsound.audioClips[0], 1f);
        StartCoroutine(ExitFadeOut());
    }

    public void ExitButtonClick()
    {
        Application.Quit();
    }

    IEnumerator ReFadeOut()
    {

        if (fadeimage != null)
        {
            while (fadevalue < 1f)
            {

                fadevalue += 0.1f;
                fadeimage.color = new Color(0, 0, 0, fadevalue);

                yield return new WaitForSeconds(0.05f);
            }
        }
        yield return null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator ExitFadeOut()
    {

        if (fadeimage != null)
        {
            while (fadevalue < 1f)
            {

                fadevalue += 0.1f;
                fadeimage.color = new Color(0, 0, 0, fadevalue);

                yield return new WaitForSeconds(0.05f);
            }
        }
        yield return null;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(0);
    }

    IEnumerator StartFadeOut()
    {
        fadevalue = 1f;
        yield return new WaitForSeconds(0.5f);
        if (fadeimage != null)
        {
            while (fadevalue > 0)
            {
                Panel.color = new Color(0, 0, 0, fadevalue);
                fadevalue -= 0.1f;


                yield return new WaitForSeconds(0.05f);
            }
        }
        yield return null;


    }

    void SoundCheck()
    {
        if (a < 1)
        {
            audio.clip = UIsound.audioClips[Random.Range(1, 2)];
            audio.volume = 0.7f;
            audio.Play();

            a = 1;
        }

    }


    void SoundOff()
    {
        StartCoroutine(SoundFade());
    }

    IEnumerator SoundFade()
    {
        float volume = 0.7f;
        while (volume > 0)
        {
            volume -= 0.1f;
            yield return new WaitForSeconds(0.1f);
            audio.volume = volume;

        }
        yield return new WaitForSeconds(0.2f);
        audio.Stop();
        yield return null;
        audio.clip = UIsound.audioClips[Random.Range(3, 4)];
        audio.volume = 1f;
        audio.Play();

        a = 0;

    }

    IEnumerator EndingFade()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (GameManager.instance.ending)
            {
                if (fadeimage != null)
                {
                    while (fadevalue < 1f)
                    {

                        fadevalue += 0.1f;
                        Panel.color = new Color(0, 0, 0, fadevalue);

                        yield return new WaitForSeconds(0.05f);
                    }
                }
                yield return new WaitForSeconds(1f);
                endingImage.SetActive(true);
                var image = endingImage.GetComponent<Image>();
                switch (GameManager.instance.enddingState)
                {
                    case 0://자동차
                        image.color = new Color(255, 255, 255, 1);
                        image.sprite = endingImages[0];
                        break;
                    case 1://문
                        image.color = new Color(255, 255, 255, 1);
                        image.sprite = endingImages[1];
                        break;
                    case 2://우물
                        image.color = new Color(255, 255, 255, 1);
                        image.sprite = endingImages[2];
                        break;
                }
                yield return new WaitForSeconds(3f);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                SceneManager.LoadScene(0);
            }
        }
    }


}
