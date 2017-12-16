using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManagerScr : MonoBehaviour {

    //Result파트
    public GameObject Result;
    public GameObject ResultText;
    public GameObject Right;
    public GameObject Wrong;
    public GameObject Friendship;
    public GameObject TotalComment;
    public GameObject FinalConfirm;

    //엔딩파트
    public GameObject Ending;

    //엔딩결과 타입
    public GameObject Result1;
    public GameObject Result2;
    public GameObject Result3;
    public GameObject RestartBtn;


    private void Start()
    {
        StartCoroutine(WaitSecondAndShowResult());
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
        Result.SetActive(false);
        Ending.SetActive(true);

        //  우호도에 따른 결과 다름
        resultType = 2;
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

}
