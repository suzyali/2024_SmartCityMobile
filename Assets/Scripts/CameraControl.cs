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
        if (Input.GetKeyDown(KeyCode.Tab)) // Tab 키를 눌렀을 때의 동작
        {
            // Tab 키를 누를 때마다 UI 상태 변경
            isCameraMovementEnabled = !isCameraMovementEnabled;
            uiCanvas.SetActive(!isCameraMovementEnabled);
        }
    }
}