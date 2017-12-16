using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answer : MonoBehaviour
{
    public int choice;

    public void Start()
    {
        Reset();
    }

    public void Reset()
    {
        choice = 4;
    }

    //
    
    public void Answer1Select()
    {
        choice = 0;
    }

    public void Answer2Select()
    {
        choice = 1;
    }

    public void Answer3Select()
    {
        choice = 2;
    }
}
