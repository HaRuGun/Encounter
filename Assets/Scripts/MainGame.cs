using System.Collections;
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
    public Slider FriendshipSlider;
    public GameObject HintButton;

    public GameObject CorrectCircle;
    public GameObject WrongCircle;
    public GameObject WrongCircle2;

    public Text ForeignerText;
    public Text Answer1Text;
    public Text Answer2Text;
    public Text Answer3Text;
    public Text TotalTimeText;
    public Text FriendshipText;

    public Sprite NormalSprite;
    public Sprite HintSprite;

    public DataManager dataManager;
    public Answer AnswerScript;
    public Slider TimerSlider;
    int[] questionArr;

    private List<QaA> listQaA;
    public int nowQuestion;

    public float totalTime;
    public float currentTime;
    public float freindship;
    public int hint;
    public bool checkAnimation;

    public float checkTime;

    public void Awake()
    {
        listQaA = new List<QaA>();
        dataManager.GetComponent<DataManager>().Init();
        XmlToList(dataManager.xml);

        nowQuestion = 0;

        totalTime = 60.0f;
        currentTime = 5.0f;
        freindship = 50.0f;
        hint = 3;
        checkTime = 3.0f;

        AnswerScript = Answer.GetComponent<Answer>();

        Ask();
        TimerSlider = Timer.GetComponent<Slider>();
    }

    public void Update()
    {
        CheckTimer();
        CheckFreindship();
        CheckAnswer();
    }

    public void AddFriednShip()
    {
        freindship += 10.0f;
        FriendshipSlider.value = freindship;
        FriendshipText.text = System.Convert.ToInt32(freindship).ToString();
    }

    public void MinusFriednShip()
    {
        freindship -= 10.0f;
        FriendshipSlider.value = freindship;
        FriendshipText.text = System.Convert.ToInt32(freindship).ToString();
    }

    public void HintClick()
    {
        if (hint <= 0)
            return;

        hint -= 1;
        HintButton.GetComponentInChildren<Text>().text = hint.ToString();
        
        int rand = Random.Range(0, 3);
        while(questionArr[rand] == 0)
            rand = Random.Range(0, 3);

        switch (rand)
        {
            case 0:
                Answer1Text.GetComponentInParent<Image>().sprite = HintSprite;
                Answer1Text.GetComponentInParent<Button>().enabled = false;
                break;
            case 1:
                Answer2Text.GetComponentInParent<Image>().sprite = HintSprite;
                Answer2Text.GetComponentInParent<Button>().enabled = false;
                break;
            case 2:
                Answer3Text.GetComponentInParent<Image>().sprite = HintSprite;
                Answer3Text.GetComponentInParent<Button>().enabled = false;
                break;
        }

        if (hint <= 0)
            HintButton.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
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
        if (checkAnimation)
            return;

        checkTime -= Time.deltaTime;
        TimerSlider.value = checkTime / currentTime;

        totalTime -= Time.deltaTime;

        TotalTimeText.text = totalTime.ToString("00.00").Replace(".", ":");

        if (checkTime <= 0.0f)
            WrongAnswer();

        if (totalTime <= 0.0f)
        {
            //GameOver
        }
    }

    public void CheckAnswer()
    {
        if (checkAnimation)
            return;

        if (AnswerScript.choice == 4)
            return;
        
        if (questionArr[AnswerScript.choice] == 0)
            CorrectAnswer();
        else
            WrongAnswer();
    }

    public void Ask()
    {
        nowQuestion = Random.Range(0, 90);
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
        Answer1Text.GetComponentInParent<Image>().sprite = NormalSprite;
        Answer1Text.GetComponentInParent<Button>().enabled = true;
        Answer2Text.text = listQaA[nowQuestion].answerText[questionArr[1]];
        Answer2Text.GetComponentInParent<Image>().sprite = NormalSprite;
        Answer2Text.GetComponentInParent<Button>().enabled = true;
        Answer3Text.text = listQaA[nowQuestion].answerText[questionArr[2]];
        Answer3Text.GetComponentInParent<Image>().sprite = NormalSprite;
        Answer3Text.GetComponentInParent<Button>().enabled = true;

        for (int i = 2; i >= 0; --i)
        {
            if (questionArr[i] == 0)
            {
                CorrectCircle.transform.position = new Vector3(0.0f, (-1.6f * i) - 0.75f, -1.0f);

                int j = i + 1;
                if (j > 2) j = 0;
                WrongCircle.transform.position = new Vector3(0.0f, (-1.6f * j) - 0.75f, -1.0f);

                int k = i - 1;
                if (k < 0) k = 2;
                WrongCircle2.transform.position = new Vector3(0.0f, (-1.6f * k) - 0.75f, -1.0f);
            }
        }
    }

    public void CorrectAnswer()
    {
        StartCoroutine(Correct());
    }

    IEnumerator Correct()
    {
        checkAnimation = true;

        CorrectCircle.SetActive(true);
        yield return new WaitForSeconds(0.1f * totalTime / 60.0f);
        CorrectCircle.SetActive(false);
        yield return new WaitForSeconds(0.1f * totalTime / 60.0f);
        CorrectCircle.SetActive(true);
        yield return new WaitForSeconds(0.3f * totalTime / 60.0f);
        CorrectCircle.SetActive(false);

        AddFriednShip();
        checkTime = currentTime;
        if (currentTime >= totalTime)
        {
            currentTime = totalTime;
        }
        AnswerScript.Reset();
        Ask();

        checkAnimation = false;
    }

    public void WrongAnswer()
    {
        StartCoroutine(Wrong());
    }

    IEnumerator Wrong()
    {
        checkAnimation = true;

        WrongCircle.SetActive(true);
        WrongCircle2.SetActive(true);
        CorrectCircle.SetActive(true);
        yield return new WaitForSeconds(0.5f * totalTime / 60.0f);
        WrongCircle.SetActive(false);
        WrongCircle2.SetActive(false);
        CorrectCircle.SetActive(false);

        MinusFriednShip();
        checkTime = currentTime;
        if (currentTime >= totalTime)
        {
            currentTime = totalTime;
        }
        AnswerScript.Reset();
        Ask();

        checkAnimation = false;
    }

    public void XmlToList(XmlDocument xmlDoc)
    {
        XmlNodeList QaATables = xmlDoc.SelectNodes("dataroot/QaAItem");
        foreach (XmlNode qaA in QaATables)
        {
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