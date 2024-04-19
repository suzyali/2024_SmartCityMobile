using UnityEngine;
using UnityEngine.UI;
using SFB;
using DevionGames.LoginSystem; // Standalone File Browser ���

public class UIManager : MonoBehaviour
{
    public FileTransferManager fileTransferManager; // ���� ���� �Ŵ��� ����
   
    // ��ư Ŭ�� �� ���� ���ε� ��� ȣ��
    public void OnUploadButtonClicked()
    {
        // ���� ���� ��ȭ ���� ����
        var paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", "", false);
        if (paths.Length > 0)
        {
            // ù ��° ������ ������ ���ε��մϴ�.
            string filePath = paths[0];
            string temporaryKey = DevionGames.LoginSystem.LoginManager.TemporaryKey;
            fileTransferManager.UploadFile(filePath,temporaryKey); // ���� ���ε� �޼��� ȣ��
        }
    }

    // ��ư Ŭ�� �� ���� �ٿ�ε� ��� ȣ��
    public void OnDownloadButtonClicked()
    {
        // �ٿ�ε��� ���ϸ� ����
        string fileName = "HelloWolrd.txt"; // ���� ���ϸ����� ����

        // ���� �ٿ�ε� �޼��� ȣ��
        fileTransferManager.DownloadFile(fileName);
    }
}
