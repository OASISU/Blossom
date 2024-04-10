using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseMsgListener : MonoBehaviour
{

    private bool isCollectingData = false;
    private List<int> heartRates = new List<int>();

    private List<Animator> pulseFlowerAnimators = new List<Animator>();
    private List<Animator> textFlowerAnimators = new List<Animator>();

    public GameObject flower1;
    public GameObject flower2;
    public GameObject flower3;
    public GameObject flower4;
    public GameObject flower5;


    void Start()
    {


        // "PulseFlower" ������Ʈ���� Animator�� ������ ����
        for (int i = 1; i <= 5; i++)
        {
            GameObject pulseFlower = GameObject.Find("PulseFlower (" + i + ")");
            if (pulseFlower != null)
            {
                Animator animator = pulseFlower.GetComponent<Animator>();
                if (animator != null)
                {
                    pulseFlowerAnimators.Add(animator);
                }
            }
            //FlowerText�� ã�Ƽ� �ִϸ����� ����
            GameObject flowerText = GameObject.Find("FlowerText (" + i + ")");
            if (flowerText != null)
            {
                Animator animator = flowerText.GetComponent<Animator>();
                if (animator != null)
                {
                    textFlowerAnimators.Add(animator);
                }
            }
        }
        flower1.SetActive(false);
        flower2.SetActive(false);
        flower3.SetActive(false);
        flower4.SetActive(false);
        flower5.SetActive(false);

    }


    public void OnMessageArrived(string msg)
    {

        try
        {
            int heartRate = int.Parse(msg);

            if (isCollectingData)
            {
                heartRates.Add(heartRate);
            }
            else if (heartRate <= 10)
            {
                // �� �ִϸ��̼� ó������ �ǵ�������
                foreach (Animator animator in pulseFlowerAnimators)
                {
                    if (animator != null)
                    {
                        animator.SetBool("isPulsed", false);
                    }
                }

                // �ؽ�Ʈ �ִϸ��̼� ó������ �ǵ�������
                foreach (Animator animator in textFlowerAnimators)
                {
                    if (animator != null)
                    {
                        animator.SetBool("isPulsed", false);
                    }
                }
                for(int i = 0; i < 5; i++)
                {
                    SetObjectAndChildrenVisibility("FlowerText (" + (i) + ")", false);
                }
                    flower1.SetActive(false);
                    flower2.SetActive(false);
                    flower3.SetActive(false);
                    flower4.SetActive(false);
                    flower5.SetActive(false);
                

                Debug.Log("��ճ��� ����");
                Debug.Log("Heart Rate: " + heartRate);
                isCollectingData = true;
                heartRates.Clear();
                heartRates.Add(heartRate);
                StartCoroutine(CollectDataFor5Seconds());
            }
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    IEnumerator CollectDataFor5Seconds()
    {
        yield return new WaitForSeconds(5);

        if (heartRates.Count > 0)
        {
            float average = 0;
            foreach (int rate in heartRates)
            {
                average += rate;
            }
            average /= heartRates.Count;

            Debug.Log("Average Heart Rate: " + average);

            // ������Ʈ Ȱ��ȭ ���ǿ� ���� ó��
            ShowObjectBasedOnAverage(average);
        }

        isCollectingData = false;
    }

    void ShowObjectBasedOnAverage(float average)
    {
        int index = -1;
        if (average < 320)
        {
            index = 0;
            flower1.SetActive(true);
        }
        else if (average >= 320 && average < 340)
        {
            index = 1;
            flower2.SetActive(true);
        }
        else if (average >= 340 && average < 360)
        {
            index = 2;
            flower3.SetActive(true);
        }
        else if (average >= 360 && average < 380)
        {
            index = 3;
            flower4.SetActive(true);
        }
        else if (average >= 380)
        {
            index = 4;
            flower5.SetActive(true);
        }

        if (index != -1 && index < pulseFlowerAnimators.Count)
        {
            pulseFlowerAnimators[index].SetBool("isPulsed", true);
            textFlowerAnimators[index].SetBool("isPulsed", true);
            SetObjectAndChildrenVisibility("PulseFlower (" + (index + 1) + ")", true);
            SetObjectAndChildrenVisibility("FlowerText (" + (index + 1) + ")", true);

            // 30�� �� ������Ʈ�� �ٽ� ����� �ڷ�ƾ ����
            StartCoroutine(HideObjectAfterTime("PulseFlower (" + (index + 1) + ")", 30));
            StartCoroutine(HideObjectAfterTime("FlowerText (" + (index + 1) + ")", 30));
        }
    }


    public void SetObjectAndChildrenVisibility(string objectName, bool visible)
    {
        GameObject obj = GameObject.Find(objectName);
        if (obj != null)
        {
            Debug.Log("Found object: " + objectName);
            Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                renderer.enabled = visible;
                Debug.Log("Setting visibility for: " + renderer.gameObject.name + " to " + visible);
                Debug.Log(renderer.gameObject.name + "�� enabled" + renderer.enabled);
            }



        }
        else
        {
            Debug.Log("Could not find object: " + objectName);
        }
    }

    IEnumerator HideObjectAfterTime(string objectName, float delay)
    {
        // ������ �ð���ŭ ��ٸ��ϴ�.
        yield return new WaitForSeconds(delay);

        // ������ �ð��� ���� �� ������Ʈ�� ����ϴ�.
        SetObjectAndChildrenVisibility(objectName, false);
    }

}