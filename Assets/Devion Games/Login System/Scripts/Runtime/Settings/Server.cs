﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DevionGames.LoginSystem.Configuration
{
    [System.Serializable]
    public class Server : Settings
    {
        public override string Name
        {
            get
            {
                return "Server";
            }
        }


        [Header("Server Settings:")]
        //https://deviongames.com/modules/demo/LoginSystem/php/ 
        public string serverAddress = "http://127.0.0.1:8080/php"; 
        public string createAccount = "createAccount.php";
        public string login = "login.php";
        public string recoverPassword = "recoverPassword.php";
        public string resetPassword = "resetPassword.php";
        public string accountKey = "Account";

    }
}