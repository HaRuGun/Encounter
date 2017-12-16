using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScr : MonoBehaviour {

    public static SoundManagerScr instance = null;
    public AudioSource efxSource;
    public AudioSource musicSource;

    public float lowPitchRange = 0.95f;
    public float highPitchRange = 1.05f;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        //임의의 함수
        
    }

    // Update is called once per frame
    void MusicPlaySingle(AudioClip clip) {
        efxSource.clip = clip;
        efxSource.Play();
    }

    void MusicStopSingle()
    {
        efxSource.Stop();
    }

    // Update is called once per frame
    void EfxPlaySingle(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    void EfxStopSingle()
    {
        musicSource.Stop();
    }
}
