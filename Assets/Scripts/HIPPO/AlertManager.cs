using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NativeWinAlert.Error("게임 그만하고 밥먹어라", "Warning");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
