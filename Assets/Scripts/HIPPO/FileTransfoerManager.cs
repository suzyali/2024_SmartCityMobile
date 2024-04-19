using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Collections;

public class FileTransferManager : MonoBehaviour
{
    // ���� ������ ó���� ������ URL
    public string serverURL = "https://smartdisastertwin.com/";

    // ������ ���ε��մϴ�.
    public void UploadFile(string filePath, string temporaryKey)
    {
        StartCoroutine(UploadCoroutine(filePath,temporaryKey));
    }

    // ������ �ٿ�ε��մϴ�.
    public void DownloadFile(string fileName)
    {
        StartCoroutine(DownloadCoroutine(fileName));
    }

    IEnumerator UploadCoroutine(string filePath, string temporaryKey)
    {
        // ������ ����Ʈ �迭�� �о�ɴϴ�.
        byte[] fileData = File.ReadAllBytes(filePath);
        string fileName = Path.GetFileName(filePath);

        // ���ε��� ���� �����͸� ������ �� ����
        WWWForm form = new WWWForm(); 
        form.AddBinaryData("file", fileData, fileName, "application/octet-stream");
       
        // �ӽ� Ű�� �� �ʵ忡 �߰��մϴ�.
        form.AddField("temporary_key", temporaryKey);

        // ������ ���ε� ��û ������
        UnityWebRequest request = UnityWebRequest.Post(serverURL + "upload/", form);
        yield return request.SendWebRequest();

        // ���ε� ��� Ȯ��
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
        // �����κ��� ���� �ٿ�ε� ��û ������
        UnityWebRequest request = UnityWebRequest.Get(serverURL + "download/" + fileName);
        yield return request.SendWebRequest();

        // �ٿ�ε� ��� Ȯ��
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("File download failed: " + request.error);
        }
        else
        {
            // �ٿ�ε��� ������ ���ÿ� ����
            string filePath = Path.Combine(Application.persistentDataPath, fileName);
            File.WriteAllBytes(filePath, request.downloadHandler.data);
            Debug.Log("File downloaded successfully to: " + filePath);
        }
    }
   
}
