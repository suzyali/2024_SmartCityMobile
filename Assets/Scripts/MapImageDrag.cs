using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapImageDrag : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform canvasRect; // 캔버스의 RectTransform
    public RectTransform imageRect; // 이미지의 RectTransform
    private Vector2 pointerOffset; // 마우스 클릭 시 이미지 내에서의 오프셋

    private Vector2 minImagePos; // 이미지의 최소 위치 (캔버스의 왼쪽 아래 모서리)
    private Vector2 maxImagePos; // 이미지의 최대 위치 (캔버스의 오른쪽 위 모서리)

    private void Start()
    {

        if (canvasRect == null || imageRect == null)
        {
            Debug.LogError("CanvasRect 또는 ImageRect가 할당되지 않았습니다. 올바로 설정하세요.");
            return;
        }

        // 이미지의 최소 위치와 최대 위치 설정
        SetMinMaxImagePos();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // 마우스 클릭 시 이미지 내에서의 오프셋 계산
        RectTransformUtility.ScreenPointToLocalPointInRectangle(imageRect, eventData.position, eventData.pressEventCamera, out pointerOffset);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canvasRect == null || imageRect == null)
            return;

        // 드래그 중인 이미지의 새로운 위치 계산
        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, eventData.position, eventData.pressEventCamera, out localPointerPosition))
        {
            // 이미지가 캔버스의 범위를 벗어나지 않도록 위치 제한

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



        // 캔버스의 크기 계산
        Vector2 canvasSize = canvasRect.rect.size;

        // 이미지의 크기 계산
        Vector2 imageSize = imageRect.rect.size;

        // 이미지가 캔버스를 벗어나지 않도록 최소 위치와 최대 위치 설정
        minImagePos = new Vector2(-(canvasSize.x / 2) + (imageSize.x / 2), -(canvasSize.y / 2) + (imageSize.y / 2));
        maxImagePos = new Vector2((canvasSize.x / 2) - (imageSize.x / 2), (canvasSize.y / 2) - (imageSize.y / 2));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // 마우스 클릭 해제 시, 추가 로직
    }


}
