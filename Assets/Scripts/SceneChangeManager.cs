using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{



    // MenuPanelMove ��ũ��Ʈ�� ���� ����
    public MenuPanelMove menuPanelMove;

    // ȭ�� �� ��ȯ ��ũ��Ʈ

    //Ȩ ������ ��ȯ
    public void HomeSceneChange()
    {
        SceneManager.LoadScene("basicScene_Sensor");

        // MenuPanelMove�� �Ҵ�Ǿ� �ִ� ��� stopPanel �Լ� ȣ��
        if (menuPanelMove != null)
        {
            menuPanelMove.stopPanel();
        }

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

    //���ø����̼� ����
    public void SceneQuit()
    {
        Application.Quit();

    }





}
