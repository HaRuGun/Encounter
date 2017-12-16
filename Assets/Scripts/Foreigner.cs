using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foreigner : MonoBehaviour
{
    public Sprite[] Chars;

    public GameObject TextureNormal;
    public GameObject TextureAngry;
    public GameObject TextureSurprise;
    
    public void SetChar(int CharNum)
    {
        TextureNormal.GetComponent<SpriteRenderer>().sprite = Chars[CharNum * 3];
        TextureAngry.GetComponent<SpriteRenderer>().sprite = Chars[CharNum * 3 + 1];
        TextureSurprise.GetComponent<SpriteRenderer>().sprite = Chars[CharNum * 3 + 2];
    }

    public void FaceNormal()
    {
        TextureNormal.SetActive(true);
        TextureAngry.SetActive(false);
        TextureSurprise.SetActive(false);
    }

    public void FaceAngry()
    {
        TextureNormal.SetActive(false);
        TextureAngry.SetActive(true);
        TextureSurprise.SetActive(false);
    }

    public void FaceSurprise()
    {
        TextureNormal.SetActive(false);
        TextureAngry.SetActive(false);
        TextureSurprise.SetActive(true);
    }
}
