using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;

/// <summary>
/// Author: István Farkas
/// <para>
/// </para>
/// </summary>

public class IpHelper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    [ContextMenu("GetIP")]
    public void testIP()
    {
        // Debug.Log(GetIP());

        Debug.Log(IPAddress.IPv6Any);
        //string strHostName = "";
        //strHostName = System.Net.Dns.GetHostName();

        //IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);

        //IPAddress[] addresses = ipEntry.AddressList;
        //foreach (var i in addresses)
        //{
        //    Debug.Log(i.ToString());
        //}


    }

    public string GetIIP(string hostName)
    {
        Ping ping = new Ping(hostName);

        if (ping.isDone)
        {
            foreach (var ip in ping.ip)
            {
                Debug.Log(ip.ToString());
            }
        }

        return "";
    }

    public string GetIP()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                ip.MapToIPv4();
                Debug.Log("IPV4: " + ip.MapToIPv6());
                return ip.ToString();
            }

        }


        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }


    public string GetExternalIp()
    {
        string _publicIP = new WebClient().DownloadString("https://api.ipify.org");
        return _publicIP;
    }
}
