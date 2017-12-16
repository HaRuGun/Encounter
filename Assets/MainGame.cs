﻿using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;


public struct QaA
{
    public int difficultyId;
    public int questionId;
    public string questionText;
    public string[] answerText;
}


public class MainGame : MonoBehaviour
{
    public GameObject Foreigner;
    public GameObject Answer;
    public GameObject Timer;

    public Text ForeignerText;
    public Text Answer1Text;
    public Text Answer2Text;
    public Text Answer3Text;

    public DataManager dataManager;
    public Answer AnswerScript;
    int[] questionArr;

    private List<QaA> listQaA;
    public int nowQuestion;

    public float totalTime;
    public float currentTime;
    public float freindship;
    public int hint;

    public float checkTime;

    public void Start()
    {
        listQaA = new List<QaA>();
        dataManager.GetComponent<DataManager>().Init();
        XmlToList(dataManager.xml);

        nowQuestion = 0;

        totalTime = 60.0f;
        currentTime = 3.0f;
        freindship = 0.0f;
        hint = 3;

        AnswerScript = Answer.GetComponent<Answer>();

        Ask();
    }

    public void Update()
    {
        //CheckTimer();
        //CheckFreindship();
        CheckAnswer();
    }

    public void CheckFreindship()
    {
        if (freindship >= 10)
        {
            //GameClear
        }
        else if (freindship <= 0)
        {
            //GameOver
        }
    }

    public void CheckTimer()
    {
        checkTime += Time.deltaTime;

        if (checkTime >= currentTime)
            WrongAnswer();

        if (totalTime <= 0.0f)
        {
            //GameOver
        }
    }

    public void CheckAnswer()
    {
        if (AnswerScript.choice == 4)
            return;

        if (questionArr[AnswerScript.choice] == 0)
        {
            CorrectAnswer();
        }
        else
        {
            WrongAnswer();
        }
    }

    public void Ask()
    {
        ForeignerText.text = listQaA[nowQuestion].questionText;

        questionArr = new int[3];
        for (int i = 2; i > 0; --i)
            questionArr[i] = i;

        for (int i = 10; i > 0; --i)
        {
            int a = Random.Range(0, 3);
            int b = Random.Range(0, 3);

            int dest = questionArr[a];
            questionArr[a] = questionArr[b];
            questionArr[b] = dest;
        }
        
        Answer1Text.text = listQaA[nowQuestion].answerText[questionArr[0]];
        Answer2Text.text = listQaA[nowQuestion].answerText[questionArr[1]];
        Answer3Text.text = listQaA[nowQuestion].answerText[questionArr[2]];
    }

    public void CorrectAnswer()
    {
        freindship += 1;
        ResetTime();
        AnswerScript.Reset();
        Ask();
    }

    public void WrongAnswer()
    {
        freindship -= 1;
        ResetTime();
        AnswerScript.Reset();
        Ask();
    }

    public void ResetTime()
    {
        totalTime -= checkTime;
        checkTime = 0.0f;
    }

    public void XmlToList(XmlDocument xmlDoc)
    {
        XmlNodeList QaATables = xmlDoc.SelectNodes("dataroot/QaAItem");
        foreach (XmlNode qaA in QaATables)
        {
            // 수량이 많으면 반복문 사용.
            Debug.Log("[at once] difficultyId :" + qaA.SelectSingleNode("difficultyId").InnerText);
            Debug.Log("[at once] questionId : " + qaA.SelectSingleNode("questionId").InnerText);
            Debug.Log("[at once] questionText : " + qaA.SelectSingleNode("questionText").InnerText);
            Debug.Log("[at once] answerText1 : " + qaA.SelectSingleNode("answerText1").InnerText);
            Debug.Log("[at once] answerText2 : " + qaA.SelectSingleNode("answerText2").InnerText);
            Debug.Log("[at once] answerText3 : " + qaA.SelectSingleNode("answerText3").InnerText);

            QaA dest = new QaA();
            dest.difficultyId = System.Convert.ToInt32(qaA.SelectSingleNode("difficultyId").InnerText);
            dest.questionId = System.Convert.ToInt32(qaA.SelectSingleNode("questionId").InnerText);
            dest.questionText = qaA.SelectSingleNode("questionText").InnerText;

            dest.answerText = new string[3];
            dest.answerText[0] = qaA.SelectSingleNode("answerText1").InnerText;
            dest.answerText[1] = qaA.SelectSingleNode("answerText2").InnerText;
            dest.answerText[2] = qaA.SelectSingleNode("answerText3").InnerText;

            listQaA.Add(dest);
        }
    }
}