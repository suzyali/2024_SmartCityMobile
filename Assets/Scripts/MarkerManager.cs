using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MarkerManager : MonoBehaviour
{
    

    [Header("�� ���� �Է�")]
    public RawImage mapRawImage;
    public string strBaseURL = "https://api.vworld.kr/req/image?service=image&request=getmap&key=";
    public string latitude = "";
    public string longitude = "";
    public int zoomLevel = 14;
    public int mapWidth;
    public int mapHeight;
    public string strAPIKey = "";

    [Header("����ü �� ��� ����")]
    public GeoPath geoPath;

    private void Start()
    {
        StartCoroutine(LoadMapWithMarker());
    }

    IEnumerator LoadMapWithMarker()
    {
        StringBuilder str = new StringBuilder();
        str.Append(strBaseURL);
        str.Append(strAPIKey);
        str.Append("&format=png");
        str.Append("&basemap=GRAPHIC");
        str.Append("&crs=epsg:4326");
        str.Append("&zoom=");
        str.Append(zoomLevel.ToString());
        str.Append("&size=");
        str.Append(mapWidth.ToString());
        str.Append(",");
        str.Append(mapHeight.ToString());

        // ��θ� ǥ���ϴ� �κ� �߰�
        if (geoPath.geoLocationPath.Count > 0)
        {
            str.Append("&marker=image:img01|point:");
            for (int i = 0; i < geoPath.geoLocationPath.Count; i++)
            {
                str.Append(geoPath.geoLocationPath[i].longtitude.ToString("F7"));
                str.Append(" ");
                str.Append(geoPath.geoLocationPath[i].latitude.ToString("F7"));

                if (i != geoPath.geoLocationPath.Count - 1)
                    str.Append(",");
            }
        }

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(str.ToString());
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            mapRawImage.texture = DownloadHandlerTexture.GetContent(request);
        }
    }

    // ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void AddCurrentLocationToPath()
    {
        StartCoroutine(GetCurrentLocation());
    }

    IEnumerator GetCurrentLocation()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.LogError("GPS not enabled by user.");
            yield break;
        }

        Input.location.Start(0.5f);

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait <= 0)
        {
            Debug.LogError("Timed out while initializing location service.");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogError("Unable to determine device location.");
            yield break;
        }

        // ���� ��ġ�� ����ü�� �߰�
        Location currentLocation;
        currentLocation.latitude = Input.location.lastData.latitude;
        currentLocation.longtitude = Input.location.lastData.longitude;
        geoPath.geoLocationPath.Add(currentLocation);

        Input.location.Stop();

        // �� �ٽ� �ҷ�����
        StartCoroutine(LoadMapWithMarker());
    }


    
}
