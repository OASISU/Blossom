using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseMsgListener : MonoBehaviour
{
    //심박수 관련 변수
    private bool isCollectingData = false;
    private List<int> heartRates = new List<int>();

    //애니메이터
    private List<Animator> pulseFlowerAnimators = new List<Animator>();
    private List<Animator> textFlowerAnimators = new List<Animator>();

    //꽃 오브젝트
    public GameObject flower1;
    public GameObject flower2;
    public GameObject flower3;
    public GameObject flower4;
    public GameObject flower5;

    // heart_0 게임 오브젝트
    public GameObject heart0;

    //Light 컴포넌트 참조 추가
    public Light sceneLight;

    // 애니메이션 재생 여부를 추적하는 플래그
    private bool animationPlaying = false;  

    void Update()
    {
        if (animationPlaying && heart0.activeSelf)
        {
            Animator animator = heart0.GetComponent<Animator>();
            if (animator != null)
            {
                AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                // 애니메이션의 normalizedTime이 1에 가까워지면 (애니메이션이 종료되면)
                if (stateInfo.normalizedTime >= 1.0f && !animator.IsInTransition(0))
                {
                    heart0.SetActive(false);  // heart0 비활성화
                    animationPlaying = false; // 애니메이션 재생 상태 업데이트
                }
            }
        }
    }
    void Start()
    {


        // "PulseFlower" 오브젝트들의 Animator를 별도로 저장
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
            //FlowerText들 찾아서 애니메이터 저장
            GameObject flowerText = GameObject.Find("FlowerText (" + i + ")");
            if (flowerText != null)
            {
                Animator animator = flowerText.GetComponent<Animator>();
                if (animator != null)
                {
                    textFlowerAnimators.Add(animator);
                }
            }
            SetObjectAndChildrenVisibility("FlowerText (" + (i) + ")", false);
        }
        flower1.SetActive(false);
        flower2.SetActive(false);
        flower3.SetActive(false);
        flower4.SetActive(false);
        flower5.SetActive(false);

        // heart0 및 fadePanel 초기에 보이지 않도록 설정
        if (heart0 != null)
            heart0.SetActive(false);

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
                // 꽃 애니메이션 처음으로 되돌려놓기
                foreach (Animator animator in pulseFlowerAnimators)
                {
                    if (animator != null)
                    {
                        animator.SetBool("isPulsed", false);
                    }
                }

                // 텍스트 애니메이션 처음으로 되돌려놓기
                foreach (Animator animator in textFlowerAnimators)
                {
                    if (animator != null)
                    {
                        animator.SetBool("isPulsed", false);
                    }
                }

                // 조명을 어두워지게 함
                StartCoroutine(FadeLight(0.2f, 1f)); 

                //하트 애니메이션 시작
                if (heart0 != null)
                {
                    heart0.SetActive(true);
                    Animator heartAnimator = heart0.GetComponent<Animator>();
                    if (heartAnimator != null)
                    {
                        heartAnimator.SetTrigger("StartAnimation");
                        animationPlaying = true;  // 애니메이션 재생 시작을 표시
                    }
                }

                for (int i = 1; i <= 5; i++)
                {
                    SetObjectAndChildrenVisibility("FlowerText (" + (i) + ")", false);
                }
                flower1.SetActive(false);
                flower2.SetActive(false);
                flower3.SetActive(false);
                flower4.SetActive(false);
                flower5.SetActive(false);


                Debug.Log("평균내기 시작");
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

            // 오브젝트 활성화 조건에 따라 처리
            ShowObjectBasedOnAverage(average);
        }

        isCollectingData = false;
    }

    void ShowObjectBasedOnAverage(float average)
    {
        StartCoroutine(FadeLight(79793.78f, 1f)); // 조명을 다시 밝게 함

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

            StartCoroutine(HideObjectAfterTime("PulseFlower (" + (index + 1) + ")", 30));
            StartCoroutine(HideObjectAfterTime2("FlowerText (" + (index + 1) + ")", 30));
            
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
                Debug.Log(renderer.gameObject.name + "의 enabled" + renderer.enabled);
            }



        }
        else
        {
            Debug.Log("Could not find object: " + objectName);
        }
    }

    IEnumerator HideObjectAfterTime(string objectName, float delay)
    {
        // 지정된 시간만큼 기다림
        yield return new WaitForSeconds(delay);

        // 지정된 시간이 지난 후 오브젝트를 숨깁니다.
        SetObjectAndChildrenVisibility(objectName, false);
        // 꽃 애니메이션 처음으로 되돌려놓기
        foreach (Animator animator in pulseFlowerAnimators)
        {
            if (animator != null)
            {
                animator.SetBool("isPulsed", false);
            }
        }
    }

    IEnumerator HideObjectAfterTime2(string objectName, float delay)
    {
        // 지정된 시간만큼 기다림
        yield return new WaitForSeconds(delay);

        // 지정된 시간이 지난 후 오브젝트를 숨깁니다.
        SetObjectAndChildrenVisibility(objectName, false);
        // 꽃 애니메이션 처음으로 되돌려놓기
        foreach (Animator animator in textFlowerAnimators)
        {
            if (animator != null)
            {
                animator.SetBool("isPulsed", false);
            }
        }
    }

    IEnumerator FadeLight(float targetIntensity, float duration)
    {
        float startIntensity = sceneLight.intensity;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            sceneLight.intensity = Mathf.Lerp(startIntensity, targetIntensity, time / duration);
            Debug.Log("Fading light to " + sceneLight.intensity); // 진행 상황 로그
            yield return null;
        }
        sceneLight.intensity = targetIntensity;
        Debug.Log("Current Light Intensity: " + sceneLight.intensity);

        Debug.Log("Light intensity set to " + targetIntensity); // 최종 인텐시티 로그
    }


}