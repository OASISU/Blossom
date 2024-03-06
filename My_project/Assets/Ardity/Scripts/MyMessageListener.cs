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
        yield return new WaitForSeconds(time); // �־��� �ð���ŭ ���

        Debug.Log("Delayed Log at time: " + time);
    }
    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        // �޽����� ������ ������ StartTime�� 0���� 3���� ����
        if (streamPlayer != null)
        {
            streamPlayer.StartTime = 0.0f;
            streamPlayer.EndTime = 3.0f;
            streamPlayer.CurrentTime = 0.0f;

            // UpdateImmediately�� ȣ���Ͽ� ��� ������Ʈ
            streamPlayer.UpdateImmediately(0.0f);
            StartCoroutine(DelayedLog(1.0f)); // 1�� �Ŀ� DelayedLog ����
            streamPlayer.UpdateImmediately(1.0f);
            StartCoroutine(DelayedLog(1.0f)); // 2�� �Ŀ� DelayedLog ����
            streamPlayer.UpdateImmediately(2.0f);
            StartCoroutine(DelayedLog(1.0f)); // 3�� �Ŀ� DelayedLog ����

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
