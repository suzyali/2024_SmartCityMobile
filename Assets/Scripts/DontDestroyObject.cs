using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyObject : MonoBehaviour
{
    private static DontDestroyObject instance;

    private void Awake()
    {
        // 이미 싱글턴 오브젝트가 존재하는지 확인합니다.
        if (instance == null)
        {
            // 싱글턴 오브젝트가 존재하지 않으면 이 오브젝트를 설정하고 파괴되지 않도록 설정합니다.
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 이미 싱글턴 오브젝트가 존재하면 이 오브젝트를 파괴합니다.
            Destroy(gameObject);
        }
    }

}
