using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct ResultData
{
    public int CorrectCount;
    public int WrongCount;
    public float FreindshipScore;
    public bool TimeOver;
}

public class ResultManagerScr : MonoBehaviour {

    //Result파트
    public GameObject Result;
    public GameObject ResultText;
    public GameObject Right;
    public GameObject Wrong;
    public GameObject Friendship;
    public GameObject TotalComment;
    public GameObject FinalConfirm;

    public GameObject RightText;
    public GameObject WrongText;
    public GameObject FreindshipText;

    //엔딩파트
    public GameObject Ending;

    //엔딩결과 타입
    public GameObject GameOver;
    public GameObject Result1;
    public GameObject Result2;
    public GameObject Result3;
    public GameObject RestartBtn;

    public ResultData resultData;

    private void Start()
    {
        GameOver.SetActive(false);

        this.resultData = ResultDataKeeper.instance.resultData;

        RightText.GetComponent<Text>().text = resultData.CorrectCount.ToString();
        WrongText.GetComponent<Text>().text = resultData.WrongCount.ToString();
        FreindshipText.GetComponent<Text>().text = resultData.FreindshipScore.ToString();

        if (resultData.FreindshipScore <= 33.3f)
            resultType = 1;
        else if (33.3f < resultData.FreindshipScore && resultData.FreindshipScore <= 66.6f)
            resultType = 2;
        else if (66.6f < resultData.FreindshipScore)
            resultType = 3;

        if (resultData.TimeOver)
        {
            SoundManagerScr.instance.PlayEndingAudio();
            StartCoroutine(WaitSecondAndShowResult());
        }
        else
        {
            SoundManagerScr.instance.PlayGameoverAudio();
            GameOver.SetActive(true);
        }
    }

    IEnumerator WaitSecondAndShowResult()
    {
        yield return new WaitForSeconds(0.5f);
        Result.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        ResultText.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Right.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Wrong.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Friendship.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        TotalComment.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        FinalConfirm.SetActive(true);
    }

    int resultType;  //  depending on friendship: 1, 2, 3

    IEnumerator ShowRestartBtn()
    {
        yield return new WaitForSeconds(6.0f);
        RestartBtn.SetActive(true);
    }

    public void FinalConfrim()
    {
        SoundManagerScr.instance.PlayResultGameAudio1();
        Result.SetActive(false);
        Ending.SetActive(true);

        //  우호도에 따른 결과 다름
        //resultType = 2;
        switch (resultType)
        {
            case 1:
                ShowResult1();
                break;
            case 2:
                ShowResult2();
                break;
            case 3:
                ShowResult3();
                break;
        }
        StartCoroutine(ShowRestartBtn());
    }

    public void ShowResult1()
    {
        Result1.SetActive(true);

    }

    public void ShowResult2()
    {
        Result2.SetActive(true);
    }

    public void ShowResult3()
    {
        Result3.SetActive(true);
    }

    public void Restart()
    {
        SoundManagerScr.instance.PlayResultGameAudio2();
        SceneManagerScr.instance.SceneChange("InGame");
    }
}
