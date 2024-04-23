using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DevionGames.LoginSystem;

public class DisplayUsername : MonoBehaviour
{
    public TMP_Text usernametextComponent; // UI Text 요소에 대한 참조

    private void Start()
    {
        // PlayerPrefs에서 저장된 username을 불러와서 UI Text에 표시
        string username = PlayerPrefs.GetString("Username", "");
        string TemporaryKey = PlayerPrefs.GetString("TemporaryKey", "");

        usernametextComponent.text = TemporaryKey;
    }



}
