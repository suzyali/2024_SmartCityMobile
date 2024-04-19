using UnityEngine;
using UnityEngine.UI;
using SFB;
using DevionGames.LoginSystem; // Standalone File Browser 사용

public class UIManager : MonoBehaviour
{
    public FileTransferManager fileTransferManager; // 파일 전송 매니저 참조
   
    // 버튼 클릭 시 파일 업로드 기능 호출
    public void OnUploadButtonClicked()
    {
        // 파일 선택 대화 상자 열기
        var paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", "", false);
        if (paths.Length > 0)
        {
            // 첫 번째 선택한 파일을 업로드합니다.
            string filePath = paths[0];
            string temporaryKey = DevionGames.LoginSystem.LoginManager.TemporaryKey;
            fileTransferManager.UploadFile(filePath,temporaryKey); // 파일 업로드 메서드 호출
        }
    }

    // 버튼 클릭 시 파일 다운로드 기능 호출
    public void OnDownloadButtonClicked()
    {
        // 다운로드할 파일명 지정
        string fileName = "HelloWolrd.txt"; // 실제 파일명으로 변경

        // 파일 다운로드 메서드 호출
        fileTransferManager.DownloadFile(fileName);
    }
}
