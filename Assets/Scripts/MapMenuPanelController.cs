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
    
    public float swipeThreshold = 50f; // ���������� �����ϱ� ���� �ּ� �̵� �Ÿ�
    public float animationSpeed = 10f; // �г��� �̵� �ӵ�
    

    void Start()
    {
        panelRectTransform = GetComponent<RectTransform>();
        originalPanelPosition = panelRectTransform.localPosition;
        targetPosition = originalPanelPosition;


        // ĵ���� RectTransform ã��

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
        // �������� �Ÿ��� ���� �̻��� ��� �г� ��ġ�� ������Ʈ�ϰ� �ƴϸ� ���� ��ġ�� ����
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
    // �г� ��ġ�� ĵ���� ������ �����ϴ� �޼���

    private Vector3 ClampPositionInCanvas(Vector3 position)

    {

        if (canvasRectTransform == null)

            return position;



        Vector3[] canvasCorners = new Vector3[4];

        canvasRectTransform.GetWorldCorners(canvasCorners);



        float topY = canvasCorners[1].y; // ĵ������ ��� Y ��ǥ


        // �г��� ���̸� ����Ͽ� ĵ���� ���� ����� �ʵ��� ����
        float panelHeight = panelRectTransform.rect.height;

        position.y = Mathf.Clamp(position.y, -topY + panelHeight / 2f, 0f);

        return position;

    }



}