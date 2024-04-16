using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class CamaraManager : MonoBehaviour
{
    // ī�޶� ȭ���� ǥ���� ���� ������Ʈ
    // ����! ����Ƽ Inspector���� �����Ǿ�� ��
    // ����! Renderer ���۳�Ʈ�� �����ؾ� ��
    public GameObject objectTarget = null;

    // ī�޶� �Է��� ���� WebCamTexture
    protected WebCamTexture textureWebCam = null;

    void Start()
    {
        // ���� ��� ������ ī�޶��� ����Ʈ
        WebCamDevice[] devices = WebCamTexture.devices;

        // ����� ī�޶� ����
        // ���� ó�� �˻��Ǵ� �ĸ� ī�޶� ���
        int selectedCameraIndex = -1;
        for (int i = 0; i < devices.Length; i++)
        {
            // ��� ������ ī�޶� �α�
            Debug.Log("Available Webcam: " + devices[i].name + ((devices[i].isFrontFacing) ? "(Front)" : "(Back)"));

            // �ĸ� ī�޶����� üũ
            if (devices[i].isFrontFacing == false)
            {
                // �ش� ī�޶� ����
                selectedCameraIndex = i;
                break;
            }
        }

        // WebCamTexture ����
        if (selectedCameraIndex >= 0)
        {
            // ���õ� ī�޶� ���� ���ο� WebCamTexture�� ����
            textureWebCam = new WebCamTexture(devices[selectedCameraIndex].name);

            // ���ϴ� FPS�� ����
            if (textureWebCam != null)
            {
                textureWebCam.requestedFPS = 60;
            }
        }

        // objectTarget���� ī�޶� ǥ�õǵ��� ����
        if (textureWebCam != null)
        {
            // objectTarget�� ���Ե� Renderer
            Renderer render = objectTarget.GetComponent<Renderer>();

            // �ش� Renderer�� mainTexture�� WebCamTexture�� ����
            render.material.mainTexture = textureWebCam;
        }
    }

    void OnDestroy()
    {
        // WebCamTexture ���ҽ� ��ȯ
        if (textureWebCam != null)
        {
            textureWebCam.Stop();
            WebCamTexture.Destroy(textureWebCam);
            textureWebCam = null;
        }
    }

    // Play ��ư�� ������ ��
    // ����! ����Ƽ Inspector���� ��ư ���� �ʿ�
    public void OnPlayButtonClick()
    {
        // ī�޶� ���� ����
        if (textureWebCam != null)
        {
            textureWebCam.Play();
        }
    }

    // Stop ��ư�� ������ ��
    // ����! ����Ƽ Inspector���� ��ư ���� �ʿ�
    public void OnStopButtonClick()
    {
        // ī�޶� ���� ����
        if (textureWebCam != null)
        {
            textureWebCam.Stop();
        }
    }
}