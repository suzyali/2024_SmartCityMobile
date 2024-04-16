using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{


    public void SceneChange2()
    {
        SceneManager.LoadScene("basicScene_Sensor");
    }

    public void SceneChange3()
    {
        SceneManager.LoadScene("CameraScene");
    }


}
