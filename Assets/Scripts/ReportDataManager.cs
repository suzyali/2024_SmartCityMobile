using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using TMPro;

public class ReportDataClass

{
    public string reportID; // 자동
    public string reportNumber; //자동
    public string reportDayTime; //자동
    public string reportType; // 입력
    public Coordinate coordinate;
    public string reportImageFileType; // 자동
    public string reportImageFileName; // 자동
    public string reportStatus; // 자동
    public string reportData; // 입력

    //좌표 : {위도, 경도}
    [System.Serializable]
    public class Coordinate
    {
        public string latitude;
        public string longitude;
    }

    // 생성자 추가
    public ReportDataClass()
    {
        coordinate = new Coordinate(); // Coordinate 객체 초기화
    }

}

public class ReportDataManager : MonoBehaviour
{

    ReportDataClass nowReport = new ReportDataClass();

    string username;
    string imagePath;

    // SetSavePath 메서드 변수 선언
    string Datapath;
    string filename;
    string targetFolder;

    // Unity 컴포넌트 선언
    public TMP_Dropdown reportTypeDropdown; // Dropdown UI 요소에 대한 참조
    public TMP_InputField reportDataInputField;
    public RawImage imageDisplay; // 이미지를 표시할 RawImage 컴포넌트



    public static ReportDataManager instance;

    private void Awake()
    {
        //싱글톤
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



    // 저장 경로 설정
    private void SetSavePath()
    {
#if UNITY_ANDROID
        targetFolder = "/storage/emulated/0/Documents/";

#elif UNITY_EDITOR
    targetFolder = Application.persistentDataPath + "/";

#else
    // 다른 플랫폼에 대한 처리
    Debug.LogWarning("Unsupported platform. Using default save path.");
    targetFolder = Application.persistentDataPath;
#endif

        // 파일명 설정
        //filename = "Report" + DateTime.Now.ToString("MMddHHmmss") + ".txt";
        filename = "ReportNew.txt";

        // 파일 경로 조합
        Datapath = Path.Combine(targetFolder, filename);
    }





    public void SaveReportData()
    {

        /*
        // 이미지가 선택되지 않은 경우
        if (string.IsNullOrEmpty(imagePath))
        {
            Debug.LogWarning("No image selected!");
            return;
        }

        */


        // 아이디의 앞 두 자리 가져오기 (임시로 하드 코딩)
        string userIdPrefix = username.Substring(0, Mathf.Min(2, username.Length));

        SetSavePath();


        nowReport.reportID = username;
        nowReport.reportNumber = userIdPrefix + DateTime.Now.ToString("MMddHHmm");
        nowReport.reportDayTime = DateTime.Now.ToString();
        nowReport.reportType = reportTypeDropdown.options[reportTypeDropdown.value].text;
        nowReport.coordinate = new ReportDataClass.Coordinate();
        nowReport.coordinate.latitude = "위도";
        nowReport.coordinate.longitude = "경도";
        nowReport.reportImageFileType = "png";
        nowReport.reportImageFileName = imagePath;
        nowReport.reportStatus = "접수";
        nowReport.reportData = reportDataInputField.text;

        string data = JsonUtility.ToJson(nowReport);

        // 파일이 존재하지 않으면 파일을 생성하고, 파일이 이미 존재하면 기존 파일의 끝에 데이터를 추가합니다.
        if (!File.Exists(Datapath))
        {
            File.WriteAllText(Datapath, data);
        }
        else
        {
            File.AppendAllText(Datapath, data);
        }

        //입력 창 초기화.
        reportDataInputField.text = "";
        // 이미지 경로 초기화 및 로드 없애기.
        imagePath = null;

        if (imagePath != null)
        {
            LoadAndDisplayImage(imagePath);
        }

        
    }


    // 버튼 클릭 시 호출되는 함수
    public void PickPhoto()
    {
        // Unity 에디터에서는 작동하지 않음
        if (Application.isEditor)
        {
            Debug.LogWarning("NativeGallery function is not available in the Unity Editor.");
            return;
        }

        // 사진을 선택하기 위한 갤러리 열기
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            // 선택된 사진의 위치를 imagePath 변수에 저장
            imagePath = path;

            // 선택된 사진의 위치를 화면에 표시
            Debug.Log("Selected Image Path: " + imagePath);
            // 선택된 사진을 Unity에 로드하여 표시
            LoadAndDisplayImage(imagePath);

        }, "Select a PNG image", "image/png");

        // 권한이 거부되었을 때 처리
        if (permission != NativeGallery.Permission.Granted)
        {
            Debug.LogWarning("Permission denied!");
        }

    }


    // 선택된 사진을 Unity에 로드하여 표시하는 함수
    private void LoadAndDisplayImage(string imagePath)
    {
        // 이미지를 텍스처로 로드
        Texture2D texture = NativeGallery.LoadImageAtPath(imagePath);

        if (texture == null)
        {
            Debug.LogError("Failed to load texture from path: " + imagePath);
            return;
        }

        // RawImage 컴포넌트에 텍스처를 설정하여 이미지를 표시
        imageDisplay.texture = texture;
    }

}

