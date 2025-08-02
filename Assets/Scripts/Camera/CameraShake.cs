using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class CameraShake : MonoBehaviour
{

    [Header("Ȩ�޹���")]
    [Tooltip("�Ƿ�������")]
    [SerializeField] private bool canShake;


    [Header("�����")]
    [Tooltip("�𶯳���ʱ��")]
    [SerializeField] private float shakeDuration;
    [Tooltip("�𶯷���")]
    [SerializeField] private float shakeMagnitude;
    [Tooltip("��˥���ٶ�")]
    [SerializeField] private float shakeDampingSpeed;
    [Tooltip("�����߿���")]
    [SerializeField] private AnimationCurve shakeCurve;

    private Vector3 initialPosition;         // ��ʼλ��
    private bool isShaking = false;          // ��״̬

    void Awake()
    {
        // �����ʼλ��
        initialPosition = transform.localPosition;

        // ����Ĭ�������ߣ����û����Inspector�����ã�
        if (shakeCurve == null || shakeCurve.length == 0)
        {
            shakeCurve = new AnimationCurve(
                new Keyframe(0f, 1f),
                new Keyframe(0.5f, -1f),
                new Keyframe(1f, 0f)
            );
            shakeCurve.preWrapMode = WrapMode.Clamp;
            shakeCurve.postWrapMode = WrapMode.Clamp;
        }
    }

    #region < �ⲿ���� >

    public void CanShakeChange(bool onoff)
    {
        canShake = onoff;
    }

    #endregion

    #region < ����� >

    /// <summary>
    /// ���������
    /// </summary>
    public void TriggerShake()
    {
        if (!isShaking && canShake)
        {
            StartCoroutine(Shake());
        }
    }

    /// <summary>
    /// ��Э��
    /// </summary>
    /// <returns></returns>
    private IEnumerator Shake()
    {
        isShaking = true;

        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            // ���㵱ǰ�𶯽��� (0-1)
            float progress = elapsedTime / shakeDuration;

            // �������߻�ȡ��ǰ�𶯷���
            float curveValue = shakeCurve.Evaluate(progress);

            // ����ƫ����
            Vector2 offset = Random.insideUnitCircle * shakeMagnitude * curveValue;

            // Ӧ��ƫ��
            transform.localPosition = initialPosition + (Vector3)offset;

            // ����ʱ��
            elapsedTime += Time.deltaTime * shakeDampingSpeed;

            yield return null;
        }

        // �ָ���ʼλ��
        transform.localPosition = initialPosition;
        isShaking = false;
    }

    #endregion
}

