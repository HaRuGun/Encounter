using System.Collections;
using System.Collections.Generic;
using UnityEngine;


struct QaA
{
    int difficultyId;
    int questionId;
    string questionText;
    string[] answerText;
}


public class MainGame : MonoBehaviour
{
    public GameObject Foreigner;
    protected GameObject ForeignerSprite;
    public GameObject Answer;
    public GameObject UI;

    public void Start()
    {
        List<QaA> listQaA = new List<QaA>();
        
    }

}