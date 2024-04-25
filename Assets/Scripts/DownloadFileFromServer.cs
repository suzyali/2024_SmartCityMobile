using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class DownloadFileFromServer : MonoBehaviour
{
    // 다운로드할 파일의 URL
    public string baseUrl = "https://smartdisastertwin.com/api/download/";
    public string fileName = "ReportNew.json"; // 다운로드하려는 파일 이름
    public string savePath = "/Data/download/"; // 파일을 저장할 경로
    public string temporaryKey = "035710854fdd1cbfdac321b1fdf3095c8ea4845d7a9d9052d005f4ee643dece0";
    void Start()
    {
        StartCoroutine(DownloadFile(baseUrl + fileName));
    }

    IEnumerator DownloadFile(string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Authorization", $"Bearer {temporaryKey}"); // 인증 키 설정
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error downloading file: " + request.error);
            }
            else
            {
                // 파일 다운로드 성공
                Debug.Log("File downloaded successfully");

                // 파일을 로컬 시스템에 저장
                // 파일을 로컬 시스템에 저장 (유니티 환경 경로 사용)
                string savePath = Path.Combine(Application.persistentDataPath, fileName);
                SaveFile(request.downloadHandler.data, savePath);
            }
        }
    }

    void SaveFile(byte[] data, string filePath)
    {
        try
        {
            File.WriteAllBytes(filePath, data);
            Debug.Log("File saved to " + filePath);
        }
        catch (IOException ex)
        {
            Debug.LogError("Error saving file: " + ex.Message);
        }
    }
}
