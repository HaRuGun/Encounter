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
    public GameObject HintButton;

    public GameObject HintChecker1;
    public GameObject HintChecker2;
    public GameObject HintChecker3;

    public GameObject CorrectCircle;
    public GameObject WrongCircle;
    public GameObject WrongCircle2;

    public GameObject ReadyOverlay;
    public GameObject GoOverlay;
    public GameObject TimeOverlay;
    public GameObject GameOverlay;
    //public GameObject ResultDataKeeper;

    public GameObject FreindshipHandle;
    public Slider FriendshipSlider;

    public Text FriendshipText;
    public Text ForeignerText;
    public Text Answer1Text;
    public Text Answer2Text;
    public Text Answer3Text;
    public Text TotalTimeText;
    
    public Sprite NormalSprite;
    public Sprite HintSprite;
    public Sprite GreenHand;
    public Sprite YellowHand;
    public Sprite RedHand;

    public DataManager dataManager;
    public Answer AnswerScript;
    public Slider TimerSlider;
    int[] questionArr;

    private List<QaA> listQaA;
    public int nowQuestion;
    public int difficulty;
    public int prevDiff;

    public float totalTime;
    public float currentTime;
    public float freindship;
    public int hint;
    public bool hintUse;

    public bool checkAnimation;
    public bool checkMoving;
    
    public float checkTime;

    public int CorrectCount;
    public int WrongCount;
    public ResultData resultData;

    public bool IsGameStart;

    public void Awake()
    {
        listQaA = new List<QaA>();
        dataManager.GetComponent<DataManager>().Init();
        XmlToList(dataManager.xml);

        nowQuestion = 0;
        difficulty = 0;
        prevDiff = difficulty;

        totalTime = 60.0f;
        currentTime = 5.0f;
        freindship = 50.0f;
        hint = 3;
        hintUse = false;
        checkTime = 3.0f;

        checkAnimation = false;
        checkMoving = false;

        IsGameStart = true;

        AnswerScript = Answer.GetComponent<Answer>();

        StartCoroutine(GameStart());

        TimerSlider = Timer.GetComponent<Slider>();

        resultData = new ResultData();

    }

    IEnumerator GameStart()
    {
        checkAnimation = true;
        checkMoving = true;

        SoundManagerScr.instance.PlayIngameAudio();

        ReadyOverlay.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        ReadyOverlay.SetActive(false);
        GoOverlay.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        GoOverlay.SetActive(false);

        checkAnimation = false;
        checkMoving = false;

        SoundManagerScr.instance.PlayStartInGameAudio();
        Ask();

        StopCoroutine(GameStart());
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
        if (freindship >= 100.0f)
            freindship = 100.0f;

        FriendshipSlider.value = freindship;
        FriendshipText.text = System.Convert.ToInt32(freindship).ToString() + "%";

        if (freindship <= 33.3f)
            FreindshipHandle.GetComponent<Image>().sprite = RedHand;
        if (33.3f < freindship && freindship <= 66.6f)
            FreindshipHandle.GetComponent<Image>().sprite = YellowHand;
        if (66.6f < freindship)
            FreindshipHandle.GetComponent<Image>().sprite = GreenHand;

    }

    public void MinusFriednShip()
    {
        freindship -= 10.0f;
        FriendshipSlider.value = freindship;
        FriendshipText.text = System.Convert.ToInt32(freindship).ToString() + "%";

        if (freindship <= 33.3f)
            FreindshipHandle.GetComponent<Image>().sprite = RedHand;
        if (33.3f < freindship && freindship <= 66.6f)
            FreindshipHandle.GetComponent<Image>().sprite = YellowHand;
        if (66.6f < freindship)
            FreindshipHandle.GetComponent<Image>().sprite = GreenHand;
    }

    public void HintClick()
    {
        if (hint <= 0 || hintUse)
            return;

        SoundManagerScr.instance.PlayHintAudio();

        hint -= 1;
        hintUse = true;

        if (hint == 0)
        {
            HintChecker1.SetActive(false);
            HintChecker2.SetActive(false);
            HintChecker3.SetActive(false);
        }
        if (hint == 1)
        {
            HintChecker1.SetActive(true);
            HintChecker2.SetActive(false);
            HintChecker3.SetActive(false);
        }
        else if (hint == 2)
        {
            HintChecker1.SetActive(true);
            HintChecker2.SetActive(true);
            HintChecker3.SetActive(false);
        }
        else if (hint == 3)
        {
            HintChecker1.SetActive(true);
            HintChecker2.SetActive(true);
            HintChecker3.SetActive(true);
        }

        int rand = Random.Range(0, 3);
        while(questionArr[rand] == 2)
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
        if (freindship <= 0)
        {
            GameFinish(false);
        }
    }

    public void CheckTimer()
    {
        if (checkAnimation || checkMoving)
            return;

        checkTime -= Time.deltaTime;
        TimerSlider.value = checkTime / currentTime;

        totalTime -= Time.deltaTime;

        TotalTimeText.text = totalTime.ToString("00.0").Replace(".", ":");

        if (checkTime <= 0.0f)
            WrongAnswer();

        if (totalTime <= 0.0f)
        {
            GameFinish(true);
        }
    }

    public void CheckAnswer()
    {
        if (checkAnimation || checkMoving)
            return;

        if (AnswerScript.choice == 4)
            return;
        
        if (questionArr[AnswerScript.choice] == 2)
            CorrectAnswer();
        else
            WrongAnswer();
    }

    public void Ask()
    {
        if (33.3f > freindship)
            difficulty = 0;
        if (66.6f > freindship && freindship >= 33.3f)
            difficulty = 1;
        if (freindship >= 66.6f)
            difficulty = 2;

        if (prevDiff != difficulty)
            StartCoroutine(ChangeForeigner());
        prevDiff = difficulty;

        hintUse = false;

        switch (difficulty)
        {
            case 0:
                nowQuestion = Random.Range(0, 34);
                break;
            case 1:
                nowQuestion = Random.Range(34, 67);
                break;
            case 2:
                nowQuestion = Random.Range(67, 91);
                break;
        }
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
            if (questionArr[i] == 2)
            {
                CorrectCircle.transform.position = new Vector3(0.0f, (-1.4f * i) - 1.2f, -1.0f);

                int j = i + 1;
                if (j > 2) j = 0;
                WrongCircle.transform.position = new Vector3(0.0f, (-1.4f * j) - 1.2f, -1.0f);

                int k = i - 1;
                if (k < 0) k = 2;
                WrongCircle2.transform.position = new Vector3(0.0f, (-1.4f * k) - 1.2f, -1.0f);
            }
        }
    }

    public void CorrectAnswer()
    {
        SoundManagerScr.instance.PlayTrueAudio();
        StartCoroutine(Correct());
    }

    IEnumerator Correct()
    {
        checkAnimation = true;
        Foreigner.GetComponent<Foreigner>().FaceSurprise();

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

        CorrectCount += 1;
        Foreigner.GetComponent<Foreigner>().FaceNormal();
        checkAnimation = false;
    }

    public void WrongAnswer()
    {
        SoundManagerScr.instance.PlayFalseAudio();
        StartCoroutine(Wrong());
    }

    IEnumerator Wrong()
    {
        checkAnimation = true;
        Foreigner.GetComponent<Foreigner>().FaceAngry();

        float f = -1.2f + Answer.GetComponent<Answer>().choice * -1.4f;
        float g = WrongCircle.transform.position.y;
        float h = WrongCircle2.transform.position.y;

        if (f == g)
        {
            WrongCircle.SetActive(true);
            yield return new WaitForSeconds(0.5f * totalTime / 60.0f);
            WrongCircle.SetActive(false);
        }
        if (f == h)
        {
            WrongCircle2.SetActive(true);
            yield return new WaitForSeconds(0.5f * totalTime / 60.0f);
            WrongCircle2.SetActive(false);
        }
        Debug.Log(Answer.GetComponent<Answer>().choice.ToString());
        Debug.Log(WrongCircle.transform.position.y.ToString());
        Debug.Log(WrongCircle2.transform.position.y.ToString());

        MinusFriednShip();
        checkTime = currentTime;
        if (currentTime >= totalTime)
        {
            currentTime = totalTime;
        }
        AnswerScript.Reset();
        Ask();

        WrongCount += 1;
        Foreigner.GetComponent<Foreigner>().FaceNormal();
        checkAnimation = false;
    }

    IEnumerator ChangeForeigner()
    {
        checkMoving = true;
        Foreigner.GetComponent<Animator>().enabled = true;

        if (!IsGameStart)
            Foreigner.GetComponent<Animator>().Play("Foreigner2");
        yield return new WaitForSeconds(1.0f);

        Foreigner.GetComponent<Foreigner>().SetChar(difficulty);

        if (!IsGameStart)
            Foreigner.GetComponent<Animator>().Play("Foreigner1");
        yield return new WaitForSeconds(1.0f);

        Foreigner.GetComponent<Animator>().enabled = false;
        checkMoving = false;

        
        IsGameStart = false;

        StopCoroutine(ChangeForeigner());
    }

    public void GameFinish(bool isTimeSet)
    {
        checkAnimation = true;
        checkMoving = true;

        resultData.CorrectCount = this.CorrectCount;
        resultData.WrongCount = this.WrongCount;
        resultData.FreindshipScore = freindship;
        resultData.TimeOver = isTimeSet;

        SoundManagerScr.instance.PlayEndInGameAudio();
        if (isTimeSet)
            TimeOverlay.SetActive(true);
        else
            GameOverlay.SetActive(true);

        StartCoroutine(GameOverCor());
    }

    IEnumerator GameOverCor()
    {
        yield return new WaitForSeconds(1.0f);
        TimeOverlay.SetActive(false);
        GameOverlay.SetActive(false);
        
        ResultDataKeeper.instance.resultData = this.resultData;
        SceneManagerScr.instance.SceneChange("Scene02");

        checkAnimation = false;
        checkMoving = false;
        StopCoroutine(GameOverCor());
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