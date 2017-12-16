using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManagerScr : MonoBehaviour {

    public GameObject Result;
    public GameObject Ending;

    public GameObject Result1;
    public GameObject Result2;
    public GameObject Result3;
    public GameObject RestartBtn;

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
