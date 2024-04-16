using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SunPositionController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Slider sunSlider; // 인스펙터에서 지정하세요.
    public Transform sun; // 인스펙터에서 지정하세요.

    void Update()
    {
        float sunPosition = sunSlider.value * 360f;
        sun.rotation = Quaternion.Euler(sunPosition, 0, 0); // 태양의 회전을 슬라이더 값에 따라 조절합니다.
    }
}