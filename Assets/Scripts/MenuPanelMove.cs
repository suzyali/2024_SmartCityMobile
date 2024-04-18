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


    public void stopPanel()
    {
        if (!isMoving)
        {
            panel.anchoredPosition = initialPosition; // �г��� �ٷ� �ʱ� ��ġ�� �����մϴ�.
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
