using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Collections;

public class FileTransferManager : MonoBehaviour
{
    // 파일 전송을 처리할 서버의 URL
    public string serverURL = "https://smartdisastertwin.com/";

    // 파일을 업로드합니다.
    public void UploadFile(string filePath, string temporaryKey)
    {
        StartCoroutine(UploadCoroutine(filePath,temporaryKey));
    }

    // 파일을 다운로드합니다.
    public void DownloadFile(string fileName)
    {
        StartCoroutine(DownloadCoroutine(fileName));
    }

    IEnumerator UploadCoroutine(string filePath, string temporaryKey)
    {
        // 파일을 바이트 배열로 읽어옵니다.
        byte[] fileData = File.ReadAllBytes(filePath);
        string fileName = Path.GetFileName(filePath);

        // 업로드할 파일 데이터를 포함한 폼 생성
        WWWForm form = new WWWForm(); 
        form.AddBinaryData("file", fileData, fileName, "application/octet-stream");
       
        // 임시 키를 폼 필드에 추가합니다.
        form.AddField("temporary_key", temporaryKey);

        // 서버로 업로드 요청 보내기
        UnityWebRequest request = UnityWebRequest.Post(serverURL + "upload/", form);
        yield return request.SendWebRequest();

        // 업로드 결과 확인
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("File upload failed: " + request.error);
        }
        else
        {
            Debug.Log("File uploaded successfully!");
        }
    }

    IEnumerator DownloadCoroutine(string fileName)
    {
        // 서버로부터 파일 다운로드 요청 보내기
        UnityWebRequest request = UnityWebRequest.Get(serverURL + "download/" + fileName);
        yield return request.SendWebRequest();

        // 다운로드 결과 확인
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("File download failed: " + request.error);
        }
        else
        {
            // 다운로드한 파일을 로컬에 저장
            string filePath = Path.Combine(Application.persistentDataPath, fileName);
            File.WriteAllBytes(filePath, request.downloadHandler.data);
            Debug.Log("File downloaded successfully to: " + filePath);
        }
    }
   
}
