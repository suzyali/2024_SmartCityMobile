using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VWorld", menuName = "VWorldPractice/GeoPath", order = 1)]
public class GeoPath : ScriptableObject
{
    [SerializeField]
    public List<Location> geoLocationPath = new List<Location>();

    public void AddLocation(double latitude, double longitude)
    {
        Location newLocation = new Location(latitude, longitude);
        geoLocationPath.Add(newLocation);
    }
}

[System.Serializable]
public struct Location
{
    public double latitude;
    public double longtitude;
    public Location(double lat, double lon)
    {
        latitude = lat;
        longtitude = lon;
    }
}