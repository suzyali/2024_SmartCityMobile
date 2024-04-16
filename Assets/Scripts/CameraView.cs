using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;


public class CameraView : MonoBehaviour
{
   
    [Header("Setting")]
    public Vector2 requestedRatio; //�����ϰ��� �ϴ� ī�޶� ����
    public int requestedFPS; //�����ϰ��� �ϴ� ������

    [Header("Component")]
    public RawImage webCamRawImage; //Canvas�� �÷��� RawImage

    [Header("Data")]
    public WebCamTexture webCamTexture; //Camera ȭ���� ���� Texture

    private void Start()
    {
        SetWebCam();
    }
    private void SetWebCam() //WebCam�� �����ϴ� �Լ�
    {
        if (Permission.HasUserAuthorizedPermission(Permission.Camera)) //�̹� ī�޶� ������ ȹ���� ��쿡��
            CreateWebCamTexture(); //�ٷ� WebCamTexture�� �����ϴ� �Լ� ����
        else //ī�޶� ������ ���� ��쿡��
        {
            PermissionCallbacks permissionCallbacks = new();
            permissionCallbacks.PermissionGranted += CreateWebCamTexture; //���� ȹ�� �� ����� �Լ��� WebCamTexture�� �����ϴ� �Լ� �߰�
            Permission.RequestUserPermission(Permission.Camera, permissionCallbacks); //ī�޶� ������ ��û
        }
    }

    private void CreateWebCamTexture(string permissionName = null) //WebCamTexture�� �����ϴ� �Լ�
    {
        if (webCamTexture) //�̹� WebCamTexture�� �����ϴ� ��쿡��
        {
            Destroy(webCamTexture); //�����Ѵ� (�޸� ����)
            webCamTexture = null;
        }

        WebCamDevice[] webCamDevices = WebCamTexture.devices; //���� ������ �� �ִ� ī�޶� ������ ��� �����´�
        if (webCamDevices.Length == 0) return; //����, ī�޶� ���ٸ� ����

        int backCamIndex = -1; //�Ĺ� ī�޶� �����ϱ� ���� ����
        for (int i = 0, l = webCamDevices.Length; i < l; ++i) //ī�޶� Ž���ϸ鼭
        {
            if (!webCamDevices[i].isFrontFacing) //�Ĺ� ī�޶� �߰��ϸ�
            {
                backCamIndex = i; //�ε��� ����
                break; //�ݺ��� ����������
            }
        }

        if (backCamIndex != -1) //�Ĺ� ī�޶� �߰�������
        {
            int requestedWidth = Screen.width; //�����ϰ��� �ϴ� ���� �ȼ� ���� ���� (���� ȭ���� ���� �ȼ��� �⺻������ ����)
            int requestedHeight = Screen.height; //�����ϰ��� �ϴ� ���� �ȼ� ���� ���� (���� ȭ���� ���� �ȼ��� �⺻������ ����)
            for (int i = 0, l = webCamDevices[backCamIndex].availableResolutions.Length; i < l; ++i) //���� ���õ� �Ĺ� ī�޶� Ȱ���� �� �ִ� �ػ󵵸� Ž���ϸ鼭
            {
                Resolution resolution = webCamDevices[backCamIndex].availableResolutions[i];
                if (GetAspectRatio((int)requestedRatio.x, (int)requestedRatio.y).Equals(GetAspectRatio(resolution.width, resolution.height))) //�����ϰ��� �ϴ� ������ ��ġ�ϴ� �ػ󵵸� �߰��ϸ�
                {
                    requestedWidth = resolution.width; //�����ϰ��� �ϴ� ���� �ȼ��� ����
                    requestedHeight = resolution.height; //�����ϰ��� �ϴ� ���� �ȼ��� ����
                    break; //�ݺ��� ����������
                }
            }

            webCamTexture = new WebCamTexture(webCamDevices[backCamIndex].name, requestedWidth, requestedHeight, requestedFPS); //ī�޶� �̸����� WebCamTexture ����
            webCamTexture.filterMode = FilterMode.Trilinear;
            webCamTexture.Play(); //ī�޶� ���

            webCamRawImage.texture = webCamTexture; //RawImage�� �Ҵ��ϱ�
        }
    }
    private string GetAspectRatio(int width, int height, bool allowPortrait = false) //������ ��ȯ�ϴ� �Լ�
    {
        if (!allowPortrait && width < height) Swap(ref width, ref height); //���ΰ� ������ �ʴµ�, (���� < ����)�̸� ������ ��ȯ
        float r = (float)width / height; //���� ����
        return r.ToString("F2"); //�Ҽ��� ��°���� �߶� ���ڿ��� ��ȯ
    }
    private void Swap<T>(ref T a, ref T b) //�� �������� ��ȯ�ϴ� �Լ�
    {
        T tmp = a;
        a = b;
        b = tmp;
    }

    private void Update()
    {
        UpdateWebCamRawImage();
    }
    private void UpdateWebCamRawImage() //RawImage�� �����ϴ� �Լ�
    {
        if (!webCamTexture) return; //WebCamTexture�� �������� ������ ����

        int videoRotAngle = webCamTexture.videoRotationAngle;
        webCamRawImage.transform.localEulerAngles = new Vector3(0, 0, -videoRotAngle); //ī�޶� ȸ�� ������ �ݿ�

        int width, height;
        if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown) //���� ȭ���̸�
        {
            width = Screen.width; //���θ� �����ϰ�
            height = Screen.width * webCamTexture.width / webCamTexture.height; //WebCamTexture�� ������ ���� ���θ� ����
        }
        else //���� ȭ���̸�
        {
            height = Screen.height; //���θ� �����ϰ�
            width = Screen.height * webCamTexture.width / webCamTexture.height; //WebCamTexture�� ������ ���� ���θ� ����
        }

        if (Mathf.Abs(videoRotAngle) % 180 != 0f) Swap(ref width, ref height); //WebCamTexture ��ü�� ȸ���Ǿ��ִ� ��� ����/���� ���� ��ȯ
        webCamRawImage.rectTransform.sizeDelta = new Vector2(width, height); //RawImage�� size�� ����
    }
}