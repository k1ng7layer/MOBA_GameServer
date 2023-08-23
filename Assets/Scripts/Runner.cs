using System.Collections;
using System.Collections.Generic;
using System.Net;
using Cysharp.Threading.Tasks;
using PBUnityMultiplayer.Runtime.Configuration;
using PBUnityMultiplayer.Runtime.Configuration.Impl;
using PBUnityMultiplayer.Runtime.Core.NetworkManager.Impl;
using PBUnityMultiplayer.Runtime.Core.NetworkManager.Models;
using UnityEngine;

public class Runner : MonoBehaviour
{
    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private ScriptableNetworkConfiguration networkConfiguration;
    
    void Start()
    {
        var ipResult = IPAddress.TryParse(networkConfiguration.ServerIp, out var ip);
        
        var serverEndpoint = new IPEndPoint(ip, networkConfiguration.ServerPort);
        
        var connectionResult = networkManager.ConnectToServer(serverEndpoint, string.Empty);

        StartCoroutine(WaitForResult(connectionResult));
    }

    private IEnumerator WaitForResult(UniTask<ConnectResult> resultTask)
    {
        while (resultTask.Status != UniTaskStatus.Succeeded)
        {
            yield return null;
        }
        
        Debug.Log($"connectionResult = {resultTask.GetAwaiter().GetResult().ConnectionResult}");
    }
    
}