using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LocationHandler : MonoBehaviour
{
    [SerializeField] private double _longitude, _latitude;


    public double Longitude
    {
        get { return _longitude; }
        set { _longitude = value; }
    }

    public double Latitude
    {
        get { return _latitude; }
        set { _latitude = value; }
    }

    private void Start()
    {
        //Debug
#if UNITY_EDITOR

        //_longitude = 12.462724;
        //_latitude = 55.638539;

#endif
    }

    IEnumerator GetLocation()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {

            yield break;
        }


        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            Latitude = Input.location.lastData.latitude;
            Longitude = Input.location.lastData.longitude;

        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }
}