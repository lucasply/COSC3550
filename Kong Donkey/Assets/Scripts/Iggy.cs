using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iggy : MonoBehaviour
{
    public List<AudioClip> iggySounds = new List<AudioClip>();
    private AudioSource source;

    public float warningChance = 0.05f;
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
            2 : be careful on ladder
        */

        AudioClip clip = iggySounds[index];

        if (clip == null) {
            Debug.Log("Sound clip not found at index: " + index);
            return;
        }
        else if (isPlaying) {
            return;
        }
        else if (index == 2 && Random.value >= warningChance) {
            return;
        }

        isPlaying = true;

        source.PlayOneShot(clip);
        StartCoroutine(Wait(clip.length));
        
        isPlaying = false;
    }

    IEnumerator Wait(float waitTime) {
        yield return new WaitForSecondsRealtime(waitTime);
    }


}
