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
        //splashImage.canvasRenderer.SetAlpha(0.0f);
        
        splashImage.CrossFadeAlpha(1.0f, 1.0f, true);
        yield return new WaitForSeconds(1.5f);
        splashImage.CrossFadeAlpha(0.5f, 1.0f, true);
        yield return new WaitForSeconds(1.5f);

        splashImage.CrossFadeAlpha(1.0f, 1.0f, true);
        yield return new WaitForSeconds(1.5f);
        splashImage.CrossFadeAlpha(0.5f, 1.0f, true);
        yield return new WaitForSeconds(1.5f);

        splashImage.CrossFadeAlpha(1.0f, 1.0f, true);
        yield return new WaitForSeconds(1.5f);
        splashImage.CrossFadeAlpha(0.5f, 1.0f, true);
        yield return new WaitForSeconds(1.5f);

        splashImage.CrossFadeAlpha(1.0f, 1.0f, true);
        yield return new WaitForSeconds(1.5f);
        splashImage.CrossFadeAlpha(0.5f, 1.0f, true);
        yield return new WaitForSeconds(1.5f);

        splashImage.CrossFadeAlpha(1.0f, 1.0f, true);
        yield return new WaitForSeconds(1.5f);
        splashImage.CrossFadeAlpha(0.5f, 1.0f, true);
        yield return new WaitForSeconds(1.5f);
    }

    public void FadeIn() {
        splashImage.CrossFadeAlpha(0.5f, 1.3f, true); 
    }

    public void FadeOut(){
        splashImage.CrossFadeAlpha(0.0f, 1.3f, true);
    }

    public void SceneChange1()
    {
        //  1초후에 넘어감
        StartCoroutine(WaitSecondAndLoadScene());
    }

    public void SceneChange(string sceneName) {
        
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator WaitSecondAndLoadScene()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(1);
    }

}
