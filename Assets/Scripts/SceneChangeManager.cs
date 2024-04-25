using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{

    // ȭ�� �� ��ȯ ��ũ��Ʈ

    //Ȩ ������ ��ȯ
    public void HomeSceneChange()
    {
        SceneManager.LoadScene("basicScene_Sensor");
    }

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

    //ī�޶� ������ ��ȯ
    public void CameraSceneChange()
    {
        SceneManager.LoadScene("CameraScene");
    }



    //���ø����̼� reset
    public void SceneRestaret()
    {
        SceneManager.LoadScene("login");

    }


    //���ø����̼� ����
    public void SceneQuit()
    {
        Application.Quit();

    }





}
