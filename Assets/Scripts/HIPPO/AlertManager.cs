using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NativeWinAlert.Error("���� �׸��ϰ� ��Ծ��", "Warning");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
