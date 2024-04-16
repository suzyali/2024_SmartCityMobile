using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapImageDrag : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform canvasRect; // ĵ������ RectTransform
    public RectTransform imageRect; // �̹����� RectTransform
    private Vector2 pointerOffset; // ���콺 Ŭ�� �� �̹��� �������� ������

    private Vector2 minImagePos; // �̹����� �ּ� ��ġ (ĵ������ ���� �Ʒ� �𼭸�)
    private Vector2 maxImagePos; // �̹����� �ִ� ��ġ (ĵ������ ������ �� �𼭸�)

    private void Start()
    {

        if (canvasRect == null || imageRect == null)
        {
            Debug.LogError("CanvasRect �Ǵ� ImageRect�� �Ҵ���� �ʾҽ��ϴ�. �ùٷ� �����ϼ���.");
            return;
        }

        // �̹����� �ּ� ��ġ�� �ִ� ��ġ ����
        SetMinMaxImagePos();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // ���콺 Ŭ�� �� �̹��� �������� ������ ���
        RectTransformUtility.ScreenPointToLocalPointInRectangle(imageRect, eventData.position, eventData.pressEventCamera, out pointerOffset);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canvasRect == null || imageRect == null)
            return;

        // �巡�� ���� �̹����� ���ο� ��ġ ���
        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, eventData.position, eventData.pressEventCamera, out localPointerPosition))
        {
            // �̹����� ĵ������ ������ ����� �ʵ��� ��ġ ����

            Vector2 newPosition = localPointerPosition - pointerOffset;

            newPosition.x = Mathf.Clamp(newPosition.x, minImagePos.x, maxImagePos.x);

            newPosition.y = Mathf.Clamp(newPosition.y, minImagePos.y, maxImagePos.y);

            imageRect.localPosition = newPosition;
        }
    }

    private void SetMinMaxImagePos()
    {
        if (canvasRect == null || imageRect == null)

            return;



        // ĵ������ ũ�� ���
        Vector2 canvasSize = canvasRect.rect.size;

        // �̹����� ũ�� ���
        Vector2 imageSize = imageRect.rect.size;

        // �̹����� ĵ������ ����� �ʵ��� �ּ� ��ġ�� �ִ� ��ġ ����
        minImagePos = new Vector2(-(canvasSize.x / 2) + (imageSize.x / 2), -(canvasSize.y / 2) + (imageSize.y / 2));
        maxImagePos = new Vector2((canvasSize.x / 2) - (imageSize.x / 2), (canvasSize.y / 2) - (imageSize.y / 2));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // ���콺 Ŭ�� ���� ��, �߰� ����
    }


}
