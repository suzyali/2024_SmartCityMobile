using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class testserver : MonoBehaviour
{
     IEnumerator Start()
    {
        // �̰��� �����͸� ä����������.
        string username = "your_username";
        string password = "your_password";
        string email = "your_email";
        string employeeNumber = "your_employeeNumber";
        string contactNumber = "your_contactNumber";
        string fullname = "your_fullname";
        string affiliation = "your_affiliation";
        string organization = "your_organization";
        string responsibility = "your_responsibility";

        // WWWForm ��ü ���� �� ������ �߰�
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("email", email);
        form.AddField("employeeNumber", employeeNumber);
        form.AddField("contactNumber", contactNumber);
        form.AddField("fullname", fullname);
        form.AddField("affiliation", affiliation);
        form.AddField("organization", organization);
        form.AddField("responsibility", responsibility);

        // POST ��û ����
        using (UnityWebRequest www = UnityWebRequest.Post("https://smartdisastertwin.com/createAccount2.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                // ��� ó��
                Debug.Log("Form upload complete!");
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
}