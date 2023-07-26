using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text.RegularExpressions;
using System.Net;

public class WorldTimeAPI : MonoBehaviour
{
    public static WorldTimeAPI _instance;

    public string worldRealTime;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    //Json
    struct Timedata {
        //public string client_ip;
        public string datetime;
    }
    const string API_URL = "https://worldtimeapi.org/api/ip";
    [HideInInspector]public bool IsTimeLoaded=false;
    private DateTime _currentDateTime = DateTime.Now;
    void Start()
    {
        StartCoroutine(GetRealDateTimeFromAPI());
    }
    public DateTime GetCurrentDateTime()
    {
        return _currentDateTime.AddSeconds(Time.realtimeSinceStartup);
    }
    IEnumerator GetRealDateTimeFromAPI()
    {
        UnityWebRequest webrequest =UnityWebRequest.Get(API_URL);
        yield return webrequest.SendWebRequest();
        if (webrequest.isDone)
        {
            Timedata timedata = JsonUtility.FromJson<Timedata>(webrequest.downloadHandler.text);
            _currentDateTime = ParseDateTime(timedata.datetime);
            IsTimeLoaded = true;
        }
        else if (webrequest.isNetworkError)
        {
            Debug.LogWarning("Error:"+ webrequest.error);
        }
        webrequest.Dispose();
    }
    DateTime ParseDateTime(string datetime)
    {
        string date=Regex.Match(datetime, @"^\d{4}-\d{2}-\d{2}").Value;
        string time=Regex.Match(datetime, @"\d{2}:\d{2}:\d{2}").Value;
        return DateTime.Parse(string.Format("{0} {1}",date,time));
    }
    private void Update()
    {
        if (IsTimeLoaded)
        {
            DateTime currentDateTime=GetCurrentDateTime();
            worldRealTime = currentDateTime.ToString();
        }
    }
}




/*
 * {
 * "abbreviation":"JST",
 * "client_ip":"160.86.227.197",
 * "datetime":"2023-07-11T10:46:07.742483+09:00",
 * "day_of_week":2,
 * "day_of_year":192,
 * "dst":false,"dst_from":null,
 * "dst_offset":0,
 * "dst_until":null,
 * "raw_offset":32400,
 * "timezone":"Asia/Tokyo",
 * "unixtime":1689039967,
 * "utc_datetime":"2023-07-11T01:46:07.742483+00:00",
 * "utc_offset":"+09:00",
 * "week_number":28}
 */