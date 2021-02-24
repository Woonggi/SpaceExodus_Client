using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;


public class NetworkManager : MonoBehaviour
{
    static readonly HttpClient client = new HttpClient();

    async void Get()
    {
        try
        {
            HttpResponseMessage response = await client.GetAsync("http://127.0.0.1:5882/");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            Debug.Log(responseBody);
        }
        catch(HttpRequestException e)
        {
            Debug.LogError("\nException Caught in Get");
        }
    }
    async void Post()
    {
        try
        {
            
        }
        catch(HttpRequestException e)
        {
            Debug.LogError("Exception Caught in Post");
        }
    }
    void Start()
    {
        Get();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
