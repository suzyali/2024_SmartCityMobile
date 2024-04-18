using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanelMove : MonoBehaviour
{
    public RectTransform panel; // 이동시킬 패널의 RectTransform 컴포넌트

    public float duration = 0.2f; // 이동하는데 걸리는 시간
    public Vector2 targetPosition; // 이동할 목표 위치

    private Vector2 initialPosition; // 초기 위치
    private bool isMoving = false; // 이동 중인지 여부

    private static MenuPanelMove instance; // Singleton 인스턴스

    void Start()
    {
        initialPosition = panel.anchoredPosition; // 초기 위치 저장
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
                instance = FindObjectOfType<MenuPanelMove>(); // 씬에서 MenuPanelMove 인스턴스 찾기
            }
            return instance;
        }
    }

    public void stopPanel()
    {
        if (!isMoving)
        {
            panel.anchoredPosition = initialPosition; // 패널을 바로 초기 위치로 설정합니다.
        }
    }

    // 다른 클래스에서 호출할 때 사용할 static 메서드
    public static void StopPanelStatic()
    {
        if (Instance != null)
        {
            Instance.stopPanel(); // MenuPanelMove의 인스턴스가 존재하면 stopPanel 함수 호출
        }
        else
        {
            Debug.LogError("MenuPanelMove instance is null."); // 인스턴스가 없는 경우 오류 메시지 출력
        }
    }


    IEnumerator MoveCoroutine(Vector2 target)
    {
        isMoving = true; // 이동 중임을 표시합니다.

        Vector2 startPosition = panel.anchoredPosition; // 현재 위치를 저장합니다.
        float elapsedTime = 0f; // 경과 시간을 초기화합니다.
        while (elapsedTime < duration) // 지정한 시간 동안 반복합니다.
        {
            // 패널의 위치를 시간에 따라 보간하여 움직입니다.
            panel.anchoredPosition = Vector2.Lerp(startPosition, target, elapsedTime / duration);
            elapsedTime += Time.deltaTime; // 경과 시간을 누적합니다.
            yield return null; // 한 프레임을 기다립니다.
        }

        panel.anchoredPosition = target; // 최종 목표 위치로 설정합니다.
        isMoving = false; // 이동이 완료되었음을 표시합니다.
    }
}
