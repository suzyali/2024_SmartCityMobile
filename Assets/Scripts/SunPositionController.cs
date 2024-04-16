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

    public Slider sunSlider; // �ν����Ϳ��� �����ϼ���.
    public Transform sun; // �ν����Ϳ��� �����ϼ���.

    void Update()
    {
        float sunPosition = sunSlider.value * 360f;
        sun.rotation = Quaternion.Euler(sunPosition, 0, 0); // �¾��� ȸ���� �����̴� ���� ���� �����մϴ�.
    }
}