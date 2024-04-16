using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI; // UI 작업을 위해 추가
using TMPro;

public class AccountCreator : MonoBehaviour
{
    public Button submitButton; // 인스펙터에서 할당할 버튼
    public TMP_InputField usernameField;
    public TMP_InputField passwordField;
    public TMP_InputField emailField;
    public TMP_InputField employeeNumberField;
    public TMP_InputField contactNumberField;
    public TMP_InputField fullnameField;
    public TMP_InputField affiliationField;
    public TMP_InputField organizationField;
    public TMP_InputField responsibilityField;

    // 서버의 createAccount.php 경로
    private string createAccountUrl = "http://203.252.134.51/createAccount2.php";

    void Start()
    {
        // 버튼에 이벤트 리스너 추가
        submitButton.onClick.AddListener(() => StartCoroutine(CreateAccount(
            usernameField.text,
            passwordField.text,
            emailField.text,
            employeeNumberField.text,
            contactNumberField.text,
            fullnameField.text,
            affiliationField.text,
            organizationField.text,
            responsibilityField.text)));
    }

    // 사용자 계정 생성을 시도하는 메소드
    public IEnumerator CreateAccount(string username, string password, string email,  string employeeNumber, string contactNumber, string fullname, string affiliation, string organization, string responsibility)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", username);
        form.AddField("password", password);
        form.AddField("email", email);
        form.AddField("employee_number", employeeNumber);
        form.AddField("contact_number", contactNumber);
        form.AddField("fullname", fullname);
        form.AddField("affiliation", affiliation);
        form.AddField("organization", organization);
        form.AddField("responsibility", responsibility);

        using (UnityWebRequest www = UnityWebRequest.Post(createAccountUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                // 서버 응답 처리
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
}
