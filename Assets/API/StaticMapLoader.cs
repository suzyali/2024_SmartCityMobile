using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Cerberus_Platform_API
{
    public class StaticMapLoader : MonoBehaviour
    {

        public RawImage mapRawImage;

        [Header("�� ���� �Է�")]
        public string strBaseURL = "http://api.vworld.kr/req/image?service=image&request=getmap&key=";
        public string latitude = "";
        public string longitude = "";
        public int zoomLevel = 14;
        public int mapWidth;
        public int mapHeight;
        public string strAPIKey = "";
        public GeoPath geoPath; // Path Scriptable Object

        private void Start()
        {
            StartCoroutine(VWorldMapLoad());
        }

        public void AddLocationFromInput()
        {
            // ������ �ε�
            StartCoroutine(VWorldMapLoad());
        }

        IEnumerator VWorldMapLoad()
        {
            // yield return null;

            StringBuilder str = new StringBuilder();
            str.Append(strBaseURL.ToString());
            str.Append(strAPIKey.ToString());
            str.Append("&format=png");
            str.Append("&basemap=GRAPHIC");
            str.Append("&center=");
            str.Append(longitude.ToString());
            str.Append(",");
            str.Append(latitude.ToString());
            str.Append("&crs=epsg:4326");
            str.Append("&zoom=");
            str.Append(zoomLevel.ToString());
            str.Append("&size=");
            str.Append(mapWidth.ToString());
            str.Append(",");
            str.Append(mapHeight.ToString());

            // ��θ� ǥ���ϴ� �κ� �߰�
            str.Append("&marker=image:img01|point:"); // ��� ��Ÿ�� ����
            for (int i = 0; i < geoPath.geoLocationPath.Count; i++) // ��� ����Ʈ�� �ݺ��Ͽ� �߰�
            {
                str.Append(geoPath.geoLocationPath[i].longtitude.ToString("F7")); // �浵 �� �߰�
                str.Append(" "); // ���� ��ȣ �߰�
                str.Append(geoPath.geoLocationPath[i].latitude.ToString("F7")); // ���� �� �߰�

                if (i == geoPath.geoLocationPath.Count - 1) // ������ ����Ʈ�� ��� �߰� ��ǥ�� ����
                {
                    break;
                }
                str.Append(","); // �߰� ��ǥ �߰�
            }

            Debug.Log(str.ToString());

            //Requset API Texture
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(str.ToString());

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(request.error);
            }
            else
            {
                mapRawImage.texture = DownloadHandlerTexture.GetContent(request);
            }
        }

    }
}