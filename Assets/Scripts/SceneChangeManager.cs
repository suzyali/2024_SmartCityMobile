using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{

    // 화면 씬 전환 스크립트

    //홈 씬으로 전환
    public void HomeSceneChange()
    {
        SceneManager.LoadScene("basicScene_Sensor");
    }

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

    //카메라 씬으로 전환
    public void CameraSceneChange()
    {
        SceneManager.LoadScene("CameraScene");
    }



    //어플리케이션 reset
    public void SceneRestaret()
    {
        SceneManager.LoadScene("login");

    }


    //어플리케이션 종료
    public void SceneQuit()
    {
        Application.Quit();

    }





}
