using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultDataKeeper : MonoBehaviour
{
    public static ResultDataKeeper instance = null;
    public ResultData resultData;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
