using DevionGames.UIWidgets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DevionGames.LoginSystem
{
    public class RegistrationWindow : UIWidget
    {
        public override string[] Callbacks
        {
            get
            {
                List<string> callbacks = new List<string>(base.Callbacks);
                callbacks.Add("OnAccountCreated");
                callbacks.Add("OnFailedToCreateAccount");
                return callbacks.ToArray();
            }
        }

        [Header("Reference")]
        [SerializeField]
        protected TMP_InputField username;
        [SerializeField]
        protected TMP_InputField password;
        [SerializeField]
        protected TMP_InputField confirmPassword;
        [SerializeField]
        protected TMP_InputField email;
        [SerializeField]
        protected TMP_InputField employeeNumber;
        [SerializeField]
        protected TMP_InputField contactNumber;
        [SerializeField]
        protected TMP_InputField fullname;
        [SerializeField]
        protected TMP_InputField affiliation;
        [SerializeField]
        protected TMP_InputField organization;
        [SerializeField]
        protected TMP_InputField responsibility;
        [SerializeField]
        protected Toggle termsOfUse;
        [SerializeField]
        protected Button registerButton;
        [SerializeField]
        protected GameObject loadingIndicator;

        protected override void OnStart()
        {
            base.OnStart();
            if (loadingIndicator != null)
            {
                loadingIndicator.SetActive(false);
            }

            EventHandler.Register("OnAccountCreated", OnAccountCreated);
            EventHandler.Register("OnFailedToCreateAccount", OnFailedToCreateAccount);

            registerButton.onClick.AddListener(CreateAccountUsingFields);
        }

        private void CreateAccountUsingFields()
        {
            if (string.IsNullOrEmpty(username.text) ||
                string.IsNullOrEmpty(password.text) ||
                string.IsNullOrEmpty(confirmPassword.text) ||
                string.IsNullOrEmpty(email.text) ||
                string.IsNullOrEmpty(employeeNumber.text) ||
                string.IsNullOrEmpty(contactNumber.text) ||
                string.IsNullOrEmpty(fullname.text) ||
                string.IsNullOrEmpty(affiliation.text) ||
                string.IsNullOrEmpty(organization.text) ||
                string.IsNullOrEmpty(responsibility.text))
            {
                LoginManager.Notifications.emptyField.Show(delegate (int result) { Show(); }, "OK");
                Close();
                return;
            }

            if (password.text != confirmPassword.text)
            {
                password.text = "";
                confirmPassword.text = "";
                LoginManager.Notifications.passwordMatch.Show(delegate (int result) { Show(); }, "OK");
                Close();
                return;
            }

            if (!LoginManager.ValidateEmail(email.text))
            {
                email.text = "";
                LoginManager.Notifications.invalidEmail.Show(delegate (int result) { Show(); }, "OK");
                Close();
                return;
            }

            if (!termsOfUse.isOn)
            {
                LoginManager.Notifications.termsOfUse.Show(delegate (int result) { Show(); }, "OK");
                Close();
                return;
            }
            registerButton.interactable = false;
            if (loadingIndicator != null)
            {
                loadingIndicator.SetActive(true);
            }
            LoginManager.CreateAccount(username.text, password.text, email.text, employeeNumber.text, contactNumber.text, fullname.text, affiliation.text, organization.text, responsibility.text);
        }


        private void OnAccountCreated()
        {
            Execute("OnAccountCreated", new CallbackEventData());
            LoginManager.Notifications.accountCreated.Show(delegate (int result) { LoginManager.UI.loginWindow.Show(); }, "OK");
            registerButton.interactable = true;
            if (loadingIndicator != null)
            {
                loadingIndicator.SetActive(false);
            }
            Close();
        }

        private void OnFailedToCreateAccount()
        {
            Execute("OnFailedToCreateAccount", new CallbackEventData());
            username.text = "";
            LoginManager.Notifications.userExists.Show(delegate (int result) { Show(); }, "OK");
            registerButton.interactable = true;
            if (loadingIndicator != null)
            {
                loadingIndicator.SetActive(false);
            }
            Close();
        }
    }
}

// 이 부분에서 서버로 데이터를 전송하는 코드를 작성합니다.
// 예를 들어, 여기에서 UnityWebRequest를 사용하여 데이터를 전송할 수 있습니다.
// 다음은 데이터를 서버로 전송하는 예시 코드입니다(UnityWebRequest 사용).
/*
WWWForm form = new WWWForm();
form.AddField("username", username.text);
form.AddField("password", password.text);
form.AddField("email", email.text);
form.AddField("employeeNumber", employeeNumber.text);
form.AddField("contactNumber", contactNumber.text);
form.AddField("fullname", fullname.text);
form.AddField("affiliation", affiliation.text);
form.AddField("organization", organization.text);
form.AddField("responsibility", responsibility.text);

// 여기서는 URL을 "your_server_endpoint"로 가정합니다. 실제 서버 엔드포인트로 교체해야 합니다.
UnityWebRequest www = UnityWebRequest.Post("your_server_endpoint", form);
yield return www.SendWebRequest();

if (www.result != UnityWebRequest.Result.Success) {
    Debug.LogError(www.error);
    OnFailedToCreate

*/