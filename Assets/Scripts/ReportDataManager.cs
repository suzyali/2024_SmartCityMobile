using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using TMPro;

public class ReportDataClass

{
    public string reportID; // �ڵ�
    public string reportNumber; //�ڵ�
    public string reportDayTime; //�ڵ�
    public string reportType; // �Է�
    public Coordinate coordinate;
    public string reportImageFileType; // �ڵ�
    public string reportImageFileName; // �ڵ�
    public string reportStatus; // �ڵ�
    public string reportData; // �Է�

    //��ǥ : {����, �浵}
    [System.Serializable]
    public class Coordinate
    {
        public string latitude;
        public string longitude;
    }

    // ������ �߰�
    public ReportDataClass()
    {
        coordinate = new Coordinate(); // Coordinate ��ü �ʱ�ȭ
    }

}

public class ReportDataManager : MonoBehaviour
{

    ReportDataClass nowReport = new ReportDataClass();

    string username;
    string imagePath;

    // SetSavePath �޼��� ���� ����
    string Datapath;
    string filename;
    string targetFolder;

    // Unity ������Ʈ ����
    public TMP_Dropdown reportTypeDropdown; // Dropdown UI ��ҿ� ���� ����
    public TMP_InputField reportDataInputField;
    public RawImage imageDisplay; // �̹����� ǥ���� RawImage ������Ʈ



    public static ReportDataManager instance;

    private void Awake()
    {
        //�̱���
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);


        username = PlayerPrefs.GetString("Username", "");


    }



    // ���� ��� ����
    private void SetSavePath()
    {
#if UNITY_ANDROID
        targetFolder = "/storage/emulated/0/Documents/";

#elif UNITY_EDITOR
    targetFolder = Application.persistentDataPath + "/";

#else
    // �ٸ� �÷����� ���� ó��
    Debug.LogWarning("Unsupported platform. Using default save path.");
    targetFolder = Application.persistentDataPath;
#endif

        // ���ϸ� ����
        //filename = "Report" + DateTime.Now.ToString("MMddHHmmss") + ".txt";
        filename = "ReportNew.txt";

        // ���� ��� ����
        Datapath = Path.Combine(targetFolder, filename);
    }





    public void SaveReportData()
    {

        /*
        // �̹����� ���õ��� ���� ���
        if (string.IsNullOrEmpty(imagePath))
        {
            Debug.LogWarning("No image selected!");
            return;
        }

        */


        // ���̵��� �� �� �ڸ� �������� (�ӽ÷� �ϵ� �ڵ�)
        string userIdPrefix = username.Substring(0, Mathf.Min(2, username.Length));

        SetSavePath();


        nowReport.reportID = username;
        nowReport.reportNumber = userIdPrefix + DateTime.Now.ToString("MMddHHmm");
        nowReport.reportDayTime = DateTime.Now.ToString();
        nowReport.reportType = reportTypeDropdown.options[reportTypeDropdown.value].text;
        nowReport.coordinate = new ReportDataClass.Coordinate();
        nowReport.coordinate.latitude = "����";
        nowReport.coordinate.longitude = "�浵";
        nowReport.reportImageFileType = "png";
        nowReport.reportImageFileName = imagePath;
        nowReport.reportStatus = "����";
        nowReport.reportData = reportDataInputField.text;

        string data = JsonUtility.ToJson(nowReport);

        // ������ �������� ������ ������ �����ϰ�, ������ �̹� �����ϸ� ���� ������ ���� �����͸� �߰��մϴ�.
        if (!File.Exists(Datapath))
        {
            File.WriteAllText(Datapath, data);
        }
        else
        {
            File.AppendAllText(Datapath, data);
        }

        //�Է� â �ʱ�ȭ.
        reportDataInputField.text = "";
        // �̹��� ��� �ʱ�ȭ �� �ε� ���ֱ�.
        imagePath = null;

        if (imagePath != null)
        {
            LoadAndDisplayImage(imagePath);
        }

        
    }


    // ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void PickPhoto()
    {
        // Unity �����Ϳ����� �۵����� ����
        if (Application.isEditor)
        {
            Debug.LogWarning("NativeGallery function is not available in the Unity Editor.");
            return;
        }

        // ������ �����ϱ� ���� ������ ����
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            // ���õ� ������ ��ġ�� imagePath ������ ����
            imagePath = path;

            // ���õ� ������ ��ġ�� ȭ�鿡 ǥ��
            Debug.Log("Selected Image Path: " + imagePath);
            // ���õ� ������ Unity�� �ε��Ͽ� ǥ��
            LoadAndDisplayImage(imagePath);

        }, "Select a PNG image", "image/png");

        // ������ �źεǾ��� �� ó��
        if (permission != NativeGallery.Permission.Granted)
        {
            Debug.LogWarning("Permission denied!");
        }

    }


    // ���õ� ������ Unity�� �ε��Ͽ� ǥ���ϴ� �Լ�
    private void LoadAndDisplayImage(string imagePath)
    {
        // �̹����� �ؽ�ó�� �ε�
        Texture2D texture = NativeGallery.LoadImageAtPath(imagePath);

        if (texture == null)
        {
            Debug.LogError("Failed to load texture from path: " + imagePath);
            return;
        }

        // RawImage ������Ʈ�� �ؽ�ó�� �����Ͽ� �̹����� ǥ��
        imageDisplay.texture = texture;
    }

}

