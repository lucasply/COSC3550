using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource source;

    void Awake() {
        source = GetComponent<AudioSource>();
    }

    void Update() {
        float vol;
        
        if (GameManager.Instance == null) {
            vol = 1f;
        }
        else {
            vol = GameManager.Instance.musVolume;
        }

        // Update volume
        source.volume = vol;
    }
}
