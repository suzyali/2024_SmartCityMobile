using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{

    // ȭ�� �� ��ȯ ��ũ��Ʈ


    //��� ������ ��ȯ
    public void BasicSceneChange()
    {
        SceneManager.LoadScene("MapBasicScene");
    }

    //ä�� ������ ��ȯ
    public void ChatSceneChange()
    {
        SceneManager.LoadScene("ReportChatScene");
    }


    public void SceneChange2()
    {
        SceneManager.LoadScene("basicScene_Sensor");
    }

    public void SceneChange3()
    {
        SceneManager.LoadScene("CameraScene");
    }


}
