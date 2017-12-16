using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScr : MonoBehaviour {

    public static SoundManagerScr instance = null;
    public AudioSource efxSource;
    public AudioSource musicSource;
    public float lowPitchRange = 0.95f;
    public float highPitchRange = 1.05f;

    //배경음악: musicSource
    public AudioClip TitleAudio;    //  타이틀
    public AudioClip IngameAudio;   //  ingame
    public AudioClip GameoverAudio;  //  게임오버
    public AudioClip EndingAudio;   // 엔딩

   
   //효과음: efxSource
   public AudioClip ButtonAudio;  //  리스타트, 엔딩보기 버튼 누를시
   public AudioClip TrueAudio;  //  맞음
   public AudioClip FalseAudio;  //  틀림
   public AudioClip HintAudio;  //  힌트
   public AudioClip StartInGameAudio;  //  인게임종료
   public AudioClip EndInGameAudio;  //  인게임종료
   public AudioClip ResultGameAudio1;  //  결과종료
   public AudioClip ResultGameAudio2;  //  인게임종료
   
    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        //임의의 함수
        
    }

    private void Start()
    {
        Debug.Log("1");
        //musicSource.clip = 
       // PlayTitleAudio();
    }

    //배경음악 시작
    public void PlayTitleAudio()
    {
        MusicPlaySingle(TitleAudio);
    }
    public void PlayIngameAudio()
    {
        MusicPlaySingle(IngameAudio);
    }
    public void PlayGameoverAudio()
    {
        MusicPlaySingle(GameoverAudio);
    }
    public void PlayEndingAudio()
    {
        MusicPlaySingle(EndingAudio);
    }
    
    //  효과음악 off
    public void PlayButtonAudio()
    {
        MusicPlaySingle(ButtonAudio);
    }
    public void PlayTrueAudio()
    {
        MusicPlaySingle(TrueAudio);
    }
    public void PlayFalseAudio()
    {
        MusicPlaySingle(FalseAudio);
    }
    public void PlayHintAudio()
    {
        MusicPlaySingle(HintAudio);
    }
    public void PlayStartInGameAudio()
    {
        MusicPlaySingle(StartInGameAudio);
    }
    public void PlayEndInGameAudio()
    {
        MusicPlaySingle(EndInGameAudio);
    }
    public void PlayResultGameAudio1()
    {
        MusicPlaySingle(ResultGameAudio1);
    }
    public void PlayResultGameAudio2()
    {
        MusicPlaySingle(ResultGameAudio2);
    }



    // Update is called once per frame
    void MusicPlaySingle(AudioClip clip) {
        musicSource.clip = clip;
        musicSource.Play();
        Debug.Log("2");
    }

    void MusicStopSingle()
    {
        musicSource.Stop();
    }

    // Update is called once per frame
    void EfxPlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    void EfxStopSingle()
    {
        efxSource.Stop();
    }
}
