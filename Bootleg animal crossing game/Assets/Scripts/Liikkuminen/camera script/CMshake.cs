using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CMshake : MonoBehaviour
{
    

    public static CMshake Shake { get; private set; }

    CinemachineVirtualCamera CMcam;
    float shakeTimer;
    float shakerTimerTotal;
    float startingIntensity;

    private void Awake()
    {
        Shake = this;
        CMcam = GetComponent<CinemachineVirtualCamera>();

    }

    public void ShakeCam(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            CMcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        startingIntensity = intensity;
        shakeTimer = time;
        shakerTimerTotal = time;

    }



    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {// Timer over!!
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                CMcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
                Mathf.Lerp(startingIntensity, 0f, shakeTimer / shakerTimerTotal);
            }
        }
    }
}
