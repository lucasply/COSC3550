using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iggy : MonoBehaviour
{
    public List<AudioClip> iggySounds = new List<AudioClip>();
    private AudioSource source;

    private bool isPlaying = false;
    public bool muteOnEnd = false;

    // Start is called before the first frame update
    void Awake()
    {
        source = GetComponent<AudioSource>();
        playIggySound(0);
    }

    void Update()
    {
        // Update volume
        // source.volume = GameManager.Instance.sFXVolume;
    }

    public void playIggySound(int index) {
        /*
            0 : help me
            1 : reaction to death
        */

        AudioClip clip = iggySounds[index];

        if (clip == null) {
            Debug.Log("Sound clip not found at index: " + index);
            return;
        }
        else if (source.isPlaying) {
            return;
        }
        else if (muteOnEnd) {
            return;
        }

        source.PlayOneShot(clip);
    }
}
