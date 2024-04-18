using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanelMove : MonoBehaviour
{
    public RectTransform panel; // �̵���ų �г��� RectTransform ������Ʈ

    public float duration = 0.2f; // �̵��ϴµ� �ɸ��� �ð�
    public Vector2 targetPosition; // �̵��� ��ǥ ��ġ

    private Vector2 initialPosition; // �ʱ� ��ġ
    private bool isMoving = false; // �̵� ������ ����

    private static MenuPanelMove instance; // Singleton �ν��Ͻ�

    void Start()
    {
        initialPosition = panel.anchoredPosition; // �ʱ� ��ġ ����
    }

    public void MovePanel()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveCoroutine(targetPosition));
        }
    }

    public void ResetPanel()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveCoroutine(initialPosition));
        }
    }


    public static MenuPanelMove Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MenuPanelMove>(); // ������ MenuPanelMove �ν��Ͻ� ã��
            }
            return instance;
        }
    }

    public void stopPanel()
    {
        if (!isMoving)
        {
            panel.anchoredPosition = initialPosition; // �г��� �ٷ� �ʱ� ��ġ�� �����մϴ�.
        }
    }

    // �ٸ� Ŭ�������� ȣ���� �� ����� static �޼���
    public static void StopPanelStatic()
    {
        if (Instance != null)
        {
            Instance.stopPanel(); // MenuPanelMove�� �ν��Ͻ��� �����ϸ� stopPanel �Լ� ȣ��
        }
        else
        {
            Debug.LogError("MenuPanelMove instance is null."); // �ν��Ͻ��� ���� ��� ���� �޽��� ���
        }
    }


    IEnumerator MoveCoroutine(Vector2 target)
    {
        isMoving = true; // �̵� ������ ǥ���մϴ�.

        Vector2 startPosition = panel.anchoredPosition; // ���� ��ġ�� �����մϴ�.
        float elapsedTime = 0f; // ��� �ð��� �ʱ�ȭ�մϴ�.
        while (elapsedTime < duration) // ������ �ð� ���� �ݺ��մϴ�.
        {
            // �г��� ��ġ�� �ð��� ���� �����Ͽ� �����Դϴ�.
            panel.anchoredPosition = Vector2.Lerp(startPosition, target, elapsedTime / duration);
            elapsedTime += Time.deltaTime; // ��� �ð��� �����մϴ�.
            yield return null; // �� �������� ��ٸ��ϴ�.
        }

        panel.anchoredPosition = target; // ���� ��ǥ ��ġ�� �����մϴ�.
        isMoving = false; // �̵��� �Ϸ�Ǿ����� ǥ���մϴ�.
    }
}
