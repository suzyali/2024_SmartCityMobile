using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{

    // 화면 씬 전환 스크립트


    //등록 씬으로 전환
    public void BasicSceneChange()
    {
        SceneManager.LoadScene("MapBasicScene");
    }

    //채팅 씬으로 전환
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
