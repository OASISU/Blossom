using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Formats.Alembic.Importer;
public class MyMessageListener : MonoBehaviour
{
    public AlembicStreamPlayer streamPlayer;
    // Use this for initialization
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator DelayedLog(float time)
    {
        yield return new WaitForSeconds(time); // 주어진 시간만큼 대기

        Debug.Log("Delayed Log at time: " + time);
    }
    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        // 메시지를 받으면 무조건 StartTime을 0부터 3으로 설정
        if (streamPlayer != null)
        {
            streamPlayer.StartTime = 0.0f;
            streamPlayer.EndTime = 3.0f;
            streamPlayer.CurrentTime = 0.0f;

            // UpdateImmediately를 호출하여 즉시 업데이트
            streamPlayer.UpdateImmediately(0.0f);
            StartCoroutine(DelayedLog(1.0f)); // 1초 후에 DelayedLog 실행
            streamPlayer.UpdateImmediately(1.0f);
            StartCoroutine(DelayedLog(1.0f)); // 2초 후에 DelayedLog 실행
            streamPlayer.UpdateImmediately(2.0f);
            StartCoroutine(DelayedLog(1.0f)); // 3초 후에 DelayedLog 실행

        }

        Debug.Log("Arrived: " + msg);
    }
    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }
}
