using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DevionGames.LoginSystem;

public class DisplayUsername : MonoBehaviour
{
    public TMP_Text usernametextComponent; // UI Text ��ҿ� ���� ����

    private void Start()
    {
        // PlayerPrefs���� ����� username�� �ҷ��ͼ� UI Text�� ǥ��
        string username = PlayerPrefs.GetString("Username", "");
        string TemporaryKey = PlayerPrefs.GetString("TemporaryKey", "");

        usernametextComponent.text = TemporaryKey;
    }



}
