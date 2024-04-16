using UnityEngine;
using UnityEditor;
using jp.luida.PSDToMobileUI.controller;

namespace jp.luida.PSDToMobileUI
{
    public class UILayout : UILayoutBase
    {
        [MenuItem("Window/PSD to Mobile UI")]
        public static void Open()
        {
            GetWindow<UILayout>("PSD to Mobile UI");
        }

        override protected void OnEnable()
        {
            UILayoutBasicSetting.PrepareGUICommonResources();
            Actions.OnBasicSettingFileChanged += UILayoutBasicSetting.OnBasicSettingFileChanged;
            base.OnEnable();
        }

        override protected void OnDisable()
        {
            Actions.OnBasicSettingFileChanged -= UILayoutBasicSetting.OnBasicSettingFileChanged;
            base.OnDisable();
        }

        override protected void OpenBasicSetting()
        {
            UILayoutBasicSetting.Open();
        }

        override public void SetComponentToCanvas(GameObject target)
        {
            target.AddComponent<PTMCanvasAdjuster>();
        }
    }
}
