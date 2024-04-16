using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class WebCamTextureSave : MonoBehaviour
{
    public RawImage cameraView;

    private WebCamTexture webCamTexture;

    private void Start()
    {
        webCamTexture = new WebCamTexture();
        cameraView.texture = webCamTexture;
        cameraView.material.mainTexture = webCamTexture;
        webCamTexture.Play();
    }

    public void TakePhoto()
    {
        Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
        photo.SetPixels(webCamTexture.GetPixels());
        photo.Apply();

        // 여기서 photo를 저장하거나 다른 곳에서 처리합니다.
        byte[] bytes = photo.EncodeToPNG();
        string fileName = "photo_unity" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";

        //string path = Application.persistentDataPath + "/" + fileName;
        string targetFolder = "/storage/emulated/0/DCIM/";
        string path = Path.Combine(targetFolder, fileName);



        Debug.Log("Saving photo to: " + path); // 경로를 확인하기 위한 디버그 로그
        System.IO.File.WriteAllBytes(path, bytes);
    }
}
