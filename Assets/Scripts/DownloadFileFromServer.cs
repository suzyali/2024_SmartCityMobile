using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class DownloadFileFromServer : MonoBehaviour
{
    // �ٿ�ε��� ������ URL
    public string baseUrl = "https://smartdisastertwin.com/api/download/";
    public string fileName = "ReportNew.json"; // �ٿ�ε��Ϸ��� ���� �̸�
    public string savePath = "/Data/download/"; // ������ ������ ���
    public string temporaryKey = "035710854fdd1cbfdac321b1fdf3095c8ea4845d7a9d9052d005f4ee643dece0";
    void Start()
    {
        StartCoroutine(DownloadFile(baseUrl + fileName));
    }

    IEnumerator DownloadFile(string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Authorization", $"Bearer {temporaryKey}"); // ���� Ű ����
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error downloading file: " + request.error);
            }
            else
            {
                // ���� �ٿ�ε� ����
                Debug.Log("File downloaded successfully");

                // ������ ���� �ý��ۿ� ����
                // ������ ���� �ý��ۿ� ���� (����Ƽ ȯ�� ��� ���)
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
