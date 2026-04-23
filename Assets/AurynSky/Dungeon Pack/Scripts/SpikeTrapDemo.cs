using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapDemo : MonoBehaviour {

    public Animator spikeTrapAnim;
    private AudioSource spikeTrapAudio;

    [Header("Player")]
    public Transform player;

    [Header("Sound Range")]
    public float maxSoundRange = 15f;
    public float mediumSoundRange = 10f;
    public float closeSoundRange = 5f;

    void Awake()
    {
        spikeTrapAnim = GetComponent<Animator>();
        spikeTrapAudio = GetComponent<AudioSource>();

        StartCoroutine(OpenCloseTrap());
    }

    IEnumerator OpenCloseTrap()
    {
        spikeTrapAnim.SetTrigger("open");
        HandleTrapSound();
        yield return new WaitForSeconds(2);

        spikeTrapAnim.SetTrigger("close");
        yield return new WaitForSeconds(2);

        StartCoroutine(OpenCloseTrap());
    }

    private void HandleTrapSound()
    {
        if (player == null || spikeTrapAudio == null)
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= maxSoundRange)
        {
            if (!spikeTrapAudio.isPlaying)
            {
                spikeTrapAudio.Play();
            }

            if (distance <= closeSoundRange)
            {
                spikeTrapAudio.volume = 1f;
            }
            else if (distance <= mediumSoundRange)
            {
                spikeTrapAudio.volume = 0.5f;
            }
            else
            {
                spikeTrapAudio.volume = 0.15f;
            }
        }
        else
        {
            if (spikeTrapAudio.isPlaying)
            {
                spikeTrapAudio.Stop();
            }
        }
    }
}