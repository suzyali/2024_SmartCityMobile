using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class UploadFilesInFolder : MonoBehaviour
{
    public string folderPath = "Assets/Data/upload";
    public string uploadUrl = "https://smartdisastertwin.com/api/upload/";
    public string temporaryKey = "035710854fdd1cbfdac321b1fdf3095c8ea4845d7a9d9052d005f4ee643dece0";

    public void UpDataJson()
    {
        StartCoroutine(UploadFiles());
    }

    IEnumerator UploadFiles()
    {
        string[] files = Directory.GetFiles(Application.dataPath + folderPath);
        foreach (string filePath in files)
        {
            if (Path.GetExtension(filePath).ToLower() == ".meta")
            {
                continue;
            }

            byte[] fileBytes = File.ReadAllBytes(filePath);
            WWWForm form = new WWWForm();
            form.AddBinaryData("file", fileBytes, Path.GetFileName(filePath));

            using (UnityWebRequest request = UnityWebRequest.Post(uploadUrl, form))
            {
                request.SetRequestHeader("Authorization", $"Bearer {temporaryKey}");
                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"Error uploading file {filePath}: {request.error}");
                }
                else
                {
                    Debug.Log($"File {filePath} uploaded successfully. Response: {request.downloadHandler.text}");
                }
            }
        }
    }
}
