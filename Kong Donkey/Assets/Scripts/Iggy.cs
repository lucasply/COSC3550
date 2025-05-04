using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iggy : MonoBehaviour
{
    public List<AudioClip> iggySounds = new List<AudioClip>();
    private AudioSource source;

    private bool isPlaying = false;

    // Start is called before the first frame update
    void Awake()
    {
        source = GetComponent<AudioSource>();
        playIggySound(0);
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

        source.PlayOneShot(clip);
    }
}
