using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyObject : MonoBehaviour
{
    private static DontDestroyObject instance;

    private void Awake()
    {
        // �̹� �̱��� ������Ʈ�� �����ϴ��� Ȯ���մϴ�.
        if (instance == null)
        {
            // �̱��� ������Ʈ�� �������� ������ �� ������Ʈ�� �����ϰ� �ı����� �ʵ��� �����մϴ�.
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // �̹� �̱��� ������Ʈ�� �����ϸ� �� ������Ʈ�� �ı��մϴ�.
            Destroy(gameObject);
        }
    }

}
