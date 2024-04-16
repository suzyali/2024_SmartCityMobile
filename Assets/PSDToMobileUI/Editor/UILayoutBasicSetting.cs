using UnityEngine;
using UnityEditor;
using jp.luida.PSDToMobileUI.view.guicommon;
using jp.luida.PSDToMobileUI.controller;

namespace jp.luida.PSDToMobileUI
{
    [InitializeOnLoad]
    public class UILayoutBasicSetting : UILayoutBasicSettingBase
    {
        class TextureByteLoader : GUICommonResources.ITextureByteLoader
        {
            public void LoadBytes(Texture2D texture, byte[] bytes)
            {
                texture.LoadImage(bytes);
            }
        }

        public static void Open()
        {
            GetWindow<UILayoutBasicSetting>("Basic Setting");
        }

        public static void PrepareGUICommonResources()
        {
            if (hasResourcesPrepared) return;
            GUICommonResources.Prepare(new TextureByteLoader());
        }

        static UILayoutBasicSetting()
        {
            LoadSettingFileInner();
        }

        static void LoadSettingFileInner()
        {
            LoadSettingFile();
            if (!tmpBasicSetting.HasSaved)
                EditorApplication.update += OpenFirst;
        }

        static void OpenFirst()
        {
            EditorApplication.update -= OpenFirst;
            UILayout.Open();
            Open();
        }

        override protected void OnEnable()
        {
            PrepareGUICommonResources();
            Actions.OnBasicSettingFileChanged += OnBasicSettingFileChanged;
            base.OnEnable();
        }

        override protected void OnDisable()
        {
            Actions.OnBasicSettingFileChanged -= OnBasicSettingFileChanged;
            base.OnDisable();
        }

        public static void OnBasicSettingFileChanged()
        {
            if (!AcceptBasicSettingFileChanged()) return;
            LoadSettingFileInner();
        }
    }
}