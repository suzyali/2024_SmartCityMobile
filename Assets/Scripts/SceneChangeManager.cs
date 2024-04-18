using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{



    // MenuPanelMove 스크립트에 대한 참조
    public MenuPanelMove menuPanelMove;

    // 화면 씬 전환 스크립트

    //홈 씬으로 전환
    public void HomeSceneChange()
    {
        SceneManager.LoadScene("basicScene_Sensor");

        // MenuPanelMove가 할당되어 있는 경우 stopPanel 함수 호출
        if (menuPanelMove != null)
        {
            menuPanelMove.stopPanel();
        }

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

    //어플리케이션 종료
    public void SceneQuit()
    {
        Application.Quit();

    }





}
