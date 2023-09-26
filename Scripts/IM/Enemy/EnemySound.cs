using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    public Sound enemySound;
    AudioSource audio;
    EnemyAI enemyAI;
    Transform playerTr;


    int pAudio = 0;
    int tAudio = 0;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        enemyAI = GetComponent<EnemyAI>();
        playerTr = enemyAI.playerTr;
        StartCoroutine(Sound());
        StartCoroutine(Volume());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Sound()
    {
        while (true)
        {
            yield return null;
            switch (enemyAI.state)
            {
                case EnemyAI.State.PATROLL:
                    if (pAudio < 1)
                    {
                        audio.clip = enemySound.audioClips[0];
                        audio.Play();
                        audio.loop = true;
                        pAudio = 1;
                        tAudio = 0;
                    }
                    break;
                case EnemyAI.State.TRACE:
                    if (tAudio < 1)
                    {
                        audio.PlayOneShot(enemySound.audioClips[2], 0.5f);
                        audio.clip = enemySound.audioClips[1];
                        audio.Play();
                        audio.loop = true;
                        tAudio = 1;
                        pAudio = 0;
                    }
                    break;
                case EnemyAI.State.IDLE:
                    audio.Stop();
                    audio.loop = false;
                    tAudio = 0;
                    pAudio = 0;
                    break;
                case EnemyAI.State.STUN:
                    audio.Stop();
                    audio.loop = false;
                    tAudio = 0;
                    pAudio = 0;
                    break;
                case EnemyAI.State.P_DIE:
                    audio.clip = null;
                    audio.PlayOneShot(enemySound.audioClips[Random.Range(3, 4)], 0.7f);
                    StopAllCoroutines();
                    break;
                default:
                    tAudio = 0;
                    pAudio = 0;
                    break;
            }
        }
    }


    IEnumerator Volume()
    {
        float minDist = 10;
        float maxDist = 30;
        float maxVolume = 1f;
        float minVolume = 0f;

        while (true)
        {
            float distance = Vector3.Distance(transform.position, playerTr.position);

            float t = Mathf.InverseLerp(minDist, maxDist, distance);
            float volume = Mathf.Lerp(maxVolume, minVolume, t);

            //volume = Mathf.Clamp01(volume);

            audio.volume = volume;

            yield return new WaitForSeconds(0.1f);

        }
    }


}
