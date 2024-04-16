using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapMenuPanelController : MonoBehaviour, IDragHandler, IEndDragHandler
{

    
    private RectTransform panelRectTransform;
    private Vector2 originalPointerPosition;
    private Vector3 originalPanelPosition;
    private bool isDragging = false;
    private Vector3 targetPosition;
    private RectTransform canvasRectTransform;
    
    public float swipeThreshold = 50f; // 스와이프를 감지하기 위한 최소 이동 거리
    public float animationSpeed = 10f; // 패널의 이동 속도
    

    void Start()
    {
        panelRectTransform = GetComponent<RectTransform>();
        originalPanelPosition = panelRectTransform.localPosition;
        targetPosition = originalPanelPosition;


        // 캔버스 RectTransform 찾기

        Canvas canvas = GetComponentInParent<Canvas>();

        if (canvas != null)
        {
            canvasRectTransform = canvas.GetComponent<RectTransform>();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging)
        {
            originalPointerPosition = eventData.position;
            isDragging = true;
        }

        float deltaY = eventData.position.y - originalPointerPosition.y;
        targetPosition.y = originalPanelPosition.y + deltaY;
        panelRectTransform.localPosition = ClampPositionInCanvas(targetPosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        // 스와이프 거리가 일정 이상인 경우 패널 위치를 업데이트하고 아니면 원래 위치로 복귀
        if (Mathf.Abs(panelRectTransform.localPosition.y - originalPanelPosition.y) > swipeThreshold)
        {
            if (panelRectTransform.localPosition.y > originalPanelPosition.y)
                targetPosition.y = canvasRectTransform.rect.height - panelRectTransform.rect.height / 2f;
            else
                targetPosition.y = -canvasRectTransform.rect.height + panelRectTransform.rect.height / 2f;
        }
        else
        {
            targetPosition = originalPanelPosition;
        }

        panelRectTransform.localPosition = ClampPositionInCanvas(targetPosition);
    }

    void Update()
    {
        panelRectTransform.localPosition = Vector3.Lerp(panelRectTransform.localPosition, targetPosition, Time.deltaTime * animationSpeed);
    }
    // 패널 위치를 캔버스 안으로 제한하는 메서드

    private Vector3 ClampPositionInCanvas(Vector3 position)

    {

        if (canvasRectTransform == null)

            return position;



        Vector3[] canvasCorners = new Vector3[4];

        canvasRectTransform.GetWorldCorners(canvasCorners);



        float topY = canvasCorners[1].y; // 캔버스의 상단 Y 좌표


        // 패널의 높이를 고려하여 캔버스 위에 벗어나지 않도록 제한
        float panelHeight = panelRectTransform.rect.height;

        position.y = Mathf.Clamp(position.y, -topY + panelHeight / 2f, 0f);

        return position;

    }



}