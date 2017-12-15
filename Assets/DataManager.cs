using UnityEngine;
using System.Collections;
using System.Collections.Generic; //리스트를 사용하기위해
using System.Xml; //xml을 사용하기위해


public class Monster_Item
{
    //xml파일에 들어가있는 정보의 종류
    public int Rank;
    public int Id;
}


public class DataManager : MonoBehaviour {

    // Resources/XML/TestItem.XML 파일.
    string xmlFileName = "QaAItem";

    void Start()
    {
        LoadXML(xmlFileName);
    }

    private void LoadXML(string _fileName)
    {
        TextAsset txtAsset = (TextAsset)Resources.Load("XML/" + _fileName);
        XmlDocument xmlDoc = new XmlDocument();
        Debug.Log(txtAsset.text);
        xmlDoc.LoadXml(txtAsset.text);

        // 하나씩 가져오기 테스트 예제.
        XmlNodeList qaATable = xmlDoc.GetElementsByTagName("questionId");
        foreach (XmlNode qaA in qaATable)
        {
            Debug.Log("[one by one] questionId : " + qaA.InnerText);
        }

        // 전체 아이템 가져오기 예제.
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
            
        }
    }

}