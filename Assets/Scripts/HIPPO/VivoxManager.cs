using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Vivox;
using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Netcode;
#if AUTH_PACKAGE_PRESENT
using Unity.Services.Authentication;
#endif



public class VivoxManager : MonoBehaviour
{
    [SerializeField]
    string _key = "OHHSzb3HKNNiHZRrsVZ5hp62pvISHnrw";
    [SerializeField]
    string _issuer = "14569-unity-96749-udash";
    [SerializeField]
    string _domain = "mtu1xp.vivox.com";
    [SerializeField]
    string _server = "https://unity.vivox.com/appconfig/14569-unity-96749-udash";

    public const string LobbyChannelName = "lobbyChannel";
    static object m_Lock = new object();
    static VivoxVoiceManager m_Instance;

    public static VivoxVoiceManager Instance
    {
        get
        {
            lock (m_Lock)
            {
                if (m_Instance == null)
                {
                    m_Instance = (VivoxVoiceManager)FindAnyObjectByType(typeof(VivoxVoiceManager));
                    if (m_Instance == null)
                    {
                        var singletonObejct = new GameObject();
                        m_Instance = singletonObejct.AddComponent<VivoxVoiceManager>();
                        singletonObejct.name = typeof(VivoxVoiceManager).ToString() + "(Singleton)";
                    }
                }
            }
            return m_Instance;
        }

    }

    async void Awake()
    {
        if (m_Instance != this && m_Instance != null)
        {
            Debug.LogWarning("Multiple VivoxVoiceManager detected in the scene. Only one VivoxVoiceManager can exist at a time. ");
            Destroy(this);
            return;
        }

        var options = new InitializationOptions();
        if (CheckManualCredentials())
        {
            options.SetVivoxCredentials(_server, _domain, _issuer, _key);

        }
        await UnityServices.InitializeAsync(options);
#if AUTH_PACKAGE_PRESENT
        if(!CheckManualCredentials())
        {
            AuthenticationService.Instance.ClearSessionToken();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
#endif
    
        await VivoxService.Instance.InitializeAsync();
    }
    bool CheckManualCredentials()
    {
        return !(string.IsNullOrEmpty(_issuer) && string.IsNullOrEmpty(_domain) && string.IsNullOrEmpty(_server));
    }
}