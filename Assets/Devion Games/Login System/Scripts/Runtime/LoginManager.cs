using DevionGames.LoginSystem.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


// 서버 응답에 대응하는 클래스 정의
[System.Serializable]
public class LoginResponse
{
    public bool success;
    public string temporary_key;
    public string refresh_key;
    public string expiration_time;
    public string message;

}

namespace DevionGames.LoginSystem
{

    public class LoginManager : MonoBehaviour
    {


        private static LoginManager m_Current;
        private string temporaryKey; // 임시 키 저장 변수 추가

        public static LoginManager current
        {
            get
            {
                Assert.IsNotNull(m_Current, "Requires Login Manager.Create one from Tools > Devion Games > Login System > Create Login Manager!");
                return m_Current;
            }
        }

        // 임시 키 접근자 추가
        public static string TemporaryKey
        {
            get
            {
                if (m_Current != null)
                {
                    return m_Current.temporaryKey;
                }
                return string.Empty;
            }
        }



        private void Awake()
        {
            if (m_Current != null)
            {
                if (DefaultSettings.debug)
                    Debug.Log("Multiple LoginManager in scene...this is not supported. Destroying instance!");
                Destroy(gameObject);
                return;
            }
            else
            {
                m_Current = this;
                if (DefaultSettings.debug)
                    Debug.Log("LoginManager initialized.");
            }
        }

        private void Start()
        {
            if (DefaultSettings.skipLogin)
            {
                if (DefaultSettings.debug)
                    Debug.Log("Login System is disabled...Loading " + DefaultSettings.sceneToLoad + " scene.");
                UnityEngine.SceneManagement.SceneManager.LoadScene(DefaultSettings.sceneToLoad);
            }
        }

        [SerializeField]
        private LoginConfigurations m_Configurations = null;

        public static LoginConfigurations Configurations
        {
            get
            {
                if (current != null)
                {
                    Assert.IsNotNull(current.m_Configurations, "Please assign Login Configurations to the Login Manager!");
                    return current.m_Configurations;
                }
                return null;
            }
        }

        private static Default m_DefaultSettings;
        public static Default DefaultSettings
        {
            get
            {
                if (m_DefaultSettings == null)
                {
                    m_DefaultSettings = GetSetting<Default>();
                }
                return m_DefaultSettings;
            }
        }

        private static UI m_UI;
        public static UI UI
        {
            get
            {
                if (m_UI == null)
                {
                    m_UI = GetSetting<UI>();
                }
                return m_UI;
            }
        }

        private static Notifications m_Notifications;
        public static Notifications Notifications
        {
            get
            {
                if (m_Notifications == null)
                {
                    m_Notifications = GetSetting<Notifications>();
                }
                return m_Notifications;
            }
        }

        private static Server m_Server;
        public static Server Server
        {
            get
            {
                if (m_Server == null)
                {
                    m_Server = GetSetting<Server>();
                }
                return m_Server;
            }
        }

        private static T GetSetting<T>() where T : Configuration.Settings
        {
            if (Configurations != null)
            {
                return (T)Configurations.settings.Where(x => x.GetType() == typeof(T)).FirstOrDefault();
            }
            return default(T);
        }

        public static void CreateAccount(string username, string password, string email, string employeeNumber, string contactNumber, string fullname, string affiliation, string organization, string responsibility)
        {
            if (current != null)
            {
                current.StartCoroutine(CreateAccountInternal(username, password, email, employeeNumber, contactNumber, fullname, affiliation, organization, responsibility));
            }
        }

        private static IEnumerator CreateAccountInternal(string username, string password, string email, string employeeNumber, string contactNumber, string fullname, string affiliation, string organization, string responsibility)
        {
            if (Configurations == null)
            {
                EventHandler.Execute("OnFailedToCreateAccount");
                yield break;
            }
            if (DefaultSettings.debug)
                Debug.Log("[CreateAccount]: Trying to register a new account.");

            WWWForm newForm = new WWWForm();
            newForm.AddField("name", username);
            newForm.AddField("password", password);
            newForm.AddField("email", email);
            newForm.AddField("employee_number", employeeNumber);
            newForm.AddField("contact_number", contactNumber);
            newForm.AddField("fullname", fullname);
            newForm.AddField("affiliation", affiliation);
            newForm.AddField("organization", organization);
            newForm.AddField("responsibility", responsibility);

            using (UnityWebRequest www = UnityWebRequest.Post("https://smartdisastertwin.com/createAccount2.php", newForm))
            {
                yield return www.SendWebRequest();
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    if (www.downloadHandler.text.Contains("true"))
					{
						if (LoginManager.DefaultSettings.debug)
							Debug.Log("[CreateAccount] Account creation was successfull!");
						EventHandler.Execute("OnAccountCreated");
					}else {
						if (LoginManager.DefaultSettings.debug)
							Debug.Log("[CreateAccount] Failed to create account.");
                            
						EventHandler.Execute("OnFailedToCreateAccount");
					}
				}
			}
		}

		/// <summary>
		/// Logins the account.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="password">Password.</param>
		public static void LoginAccount(string username, string password)
		{
			if (LoginManager.current != null)
			{
				LoginManager.current.StartCoroutine(LoginAccountInternal(username, password));
			}
		}

		private static IEnumerator LoginAccountInternal(string username, string password)
		{
           
            if (LoginManager.Configurations == null)
			{
				EventHandler.Execute("OnFailedToLogin");
				yield break;
			}
			if (LoginManager.DefaultSettings.debug)
				Debug.Log("[LoginAccount] Trying to login using username: " + username + " and password: " + password + "!");

			WWWForm newForm = new WWWForm();
			newForm.AddField("username", username);
			newForm.AddField("password", password);

            //(LoginManager.Server.serverAddress + "/" + LoginManager.Server.login, newForm)
            using (UnityWebRequest www = UnityWebRequest.Post("https://smartdisastertwin.com/login2.php", newForm))

            {
				yield return www.SendWebRequest();
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log("Network Error: " + www.error);
                }
                else
				{
                    string jsonResponse = www.downloadHandler.text;
                    LoginResponse response = JsonUtility.FromJson<LoginResponse>(jsonResponse);

                    if (response.success)
                    {
                        // 로그인 성공 시 처리
                        PlayerPrefs.SetString(LoginManager.Server.accountKey, username);
                        PlayerPrefs.SetString("Username", username);
                        PlayerPrefs.SetString("TemporaryKey", response.temporary_key); // temporary_key 저장
                        // 로그인 성공 시 사용자 이름 저장
                        PlayerPrefs.Save();
                        if (LoginManager.DefaultSettings.debug)
                        {
                            //m_Current.temporaryKey = response.temporary_key;
                            Debug.Log("[LoginAccount] Login was successful!");
                            Debug.Log(LoginManager.Server.accountKey);
                            Debug.Log("server meassge:" + jsonResponse);
                            EventHandler.Execute("OnLogin");
                            // 여기에 새로운 씬 이름을 넣어 씬 전환
                            SceneManager.LoadScene("basicScene_Sensor");
                            //Debug.Log("씬 전환이 완료했습니다");
                            
                            
                            
                        }
                    }
                    else
                    {
                        // 로그인 실패 시 처리
                        if (LoginManager.DefaultSettings.debug)
                        {
                            Debug.Log("[LoginAccount] Failed to login.");
                            Debug.Log(response.message); // 실패 메시지 출력
                        }
                        EventHandler.Execute("OnFailedToLogin");
                    }
                }
			}
		}

		/// <summary>
		/// Recovers the password.
		/// </summary>
		/// <param name="email">Email.</param>
		public static void RecoverPassword(string email)
		{
			if (LoginManager.current != null)
			{
				LoginManager.current.StartCoroutine(RecoverPasswordInternal(email));
			}
		}

		private static IEnumerator RecoverPasswordInternal(string email)
		{
			if (LoginManager.Configurations == null)
			{
				EventHandler.Execute("OnFailedToRecoverPassword");
				yield break;
			}
			if (LoginManager.DefaultSettings.debug)
				Debug.Log("[RecoverPassword] Trying to recover password using email: " + email + "!");

			WWWForm newForm = new WWWForm();
			newForm.AddField("email", email);
            //(LoginManager.Server.serverAddress + "/" + LoginManager.Server.recoverPassword, newForm)
            using (UnityWebRequest www = UnityWebRequest.Post(LoginManager.Server.serverAddress + "/" + LoginManager.Server.recoverPassword, newForm))
			{
				yield return www.SendWebRequest();
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
                else
				{
					if (www.downloadHandler.text.Contains("true"))
					{
						EventHandler.Execute("OnPasswordRecovered");
						if (LoginManager.DefaultSettings.debug)
							Debug.Log("[RecoverPassword] Password recovered successfull!");
					}
					else
					{
						if (LoginManager.DefaultSettings.debug)
							Debug.Log("[RecoverPassword] Failed to recover password.");
						EventHandler.Execute("OnFailedToRecoverPassword");
					}
				}
			}
		}

		/// <summary>
		/// Resets the password.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="password">Password.</param>
		public static void ResetPassword(string username, string password)
		{
			if (LoginManager.current != null)
			{
				LoginManager.current.StartCoroutine(ResetPasswordInternal(username, password));
			}
		}

		private static IEnumerator ResetPasswordInternal(string username, string password)
		{
			if (LoginManager.Configurations == null)
			{
				EventHandler.Execute("OnFailedToResetPassword");
				yield break;
			}
			if (LoginManager.DefaultSettings.debug)
				Debug.Log("[ResetPassword] Trying to reset password using username: " + username + " and password: " + password + "!");

			WWWForm newForm = new WWWForm();
			newForm.AddField("name", username);
			newForm.AddField("password", password);

			using (UnityWebRequest www = UnityWebRequest.Post(LoginManager.Server.serverAddress + "/" + LoginManager.Server.resetPassword, newForm))
			{
				yield return www.SendWebRequest();
                if (www.result != UnityWebRequest.Result.Success) // Unity 2020.1 이상에서 사용 //(www.isNetworkError || www.isHttpError) 구버전 이제는 사용안함
                {
					Debug.Log(www.error);
				}
				else
				{
					if (www.downloadHandler.text.Contains("true"))
					{
						if (LoginManager.DefaultSettings.debug)
							Debug.Log("[RecoverPassword] Password resetted!");
						EventHandler.Execute("OnPasswordResetted");
				
					}
					else
					{
						if (LoginManager.DefaultSettings.debug)
							Debug.Log("Failed to reset password.");
							Debug.Log(www.downloadHandler.text);
                        EventHandler.Execute("OnFailedToResetPassword");
					}
				}
			}
		}

        /// <summary>
        /// Validates the email.
        /// </summary>
        /// <returns><c>true</c>, if email was validated, <c>false</c> otherwise.</returns>
        /// <param name="email">Email.</param>
        public static bool ValidateEmail(string email)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            System.Text.RegularExpressions.Match match = regex.Match(email);
            if (match.Success)
            {
                if (DefaultSettings.debug)
                    Debug.Log("Email validation was successful for email: " + email + "!");
            }
            else
            {
                if (DefaultSettings.debug)
                    Debug.Log("Email validation failed for email: " + email + "!");
            }

            return match.Success;
        }
    }
}