using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour
{
  
    public GameObject uiCanvas; 
    public bool isCameraMovementEnabled = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) // Tab Ű�� ������ ���� ����
        {
            // Tab Ű�� ���� ������ UI ���� ����
            isCameraMovementEnabled = !isCameraMovementEnabled;
            uiCanvas.SetActive(!isCameraMovementEnabled);
        }
    }
}