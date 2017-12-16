using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagerScr : MonoBehaviour {

    public Image splashImage;
    public string loadLevel;

    int rightCount;
    int wrongCount;
    float friendship;

    public static SceneManagerScr instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        Start();
    }

    IEnumerator Start() {
        splashImage.canvasRenderer.SetAlpha(0.0f);
        FadeIn();
        yield return new WaitForSeconds(2.5f);
        FadeOut();
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(loadLevel);
    }

    public void FadeIn() {
        splashImage.CrossFadeAlpha(1.0f, 1.5f, false); 
    }

    public void FadeOut(){
        splashImage.CrossFadeAlpha(0.0f, 2.5f, false);
    }

    public void SceneChange1()
    {
        Debug.Log("1");
        SceneManager.LoadScene(1);
    }

    public void SceneChange(string sceneName) {
        Debug.Log("2");
        SceneManager.LoadScene(sceneName);
    }

}
