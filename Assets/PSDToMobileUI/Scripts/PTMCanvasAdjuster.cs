using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace jp.luida.PSDToMobileUI
{
    public class PTMCanvasAdjuster : MonoBehaviour
    {
        void Awake()
        {
            if (!Application.isEditor)
            {
                var scaler = GetComponent<CanvasScaler>(); if (scaler == null) return;
                if (scaler.uiScaleMode != CanvasScaler.ScaleMode.ConstantPhysicalSize) return;
                scaler.physicalUnit = CanvasScaler.Unit.Points;
            }
        }
    }
}