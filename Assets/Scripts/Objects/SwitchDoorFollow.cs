using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwitchDoorFollow : MonoBehaviour
{

    [Header("��������")]
    [Tooltip("�����壨��Ҫ�����Ŀ�꣩")]
    [SerializeField] private GameObject mainObject;
    [Tooltip("�����������ߴ�")]
    [SerializeField] private float followScale;

    [Header("��������")]
    [Tooltip("������״̬")]
    [SerializeField] private bool isEnable;
    [Space]
    [Tooltip("������")]
    [SerializeField] private GameObject switchDoor;
    [Tooltip("�������أ����ʣ�")]
    [SerializeField] private GameObject onSwitch;
    [Tooltip("�رտ��أ����ʣ�")]
    [SerializeField] private GameObject offSwitch;

    [Header("��������ת")]
    [Tooltip("��תȨ��")]
    [SerializeField] private bool canRotate = true;
    [Tooltip("��ת��ʱ��")]
    [SerializeField] private float rotateDuration;
    [Tooltip("��ת�Ƕ�")]
    [SerializeField] private float rotateAngle;

    private void Update()
    {
        SwitchDoorFunction();

        switchDoor.transform.localScale = mainObject.transform.localScale * followScale;
        switchDoor.GetComponent<SpriteRenderer>().color = mainObject.GetComponent<SpriteRenderer>().color;

        // ���ظ���������任����
        onSwitch.GetComponent<SpriteRenderer>().sprite = mainObject.GetComponent<SwitchDoor>().onSwitch.GetComponent<SpriteRenderer>().sprite;
        offSwitch.GetComponent<SpriteRenderer>().sprite = mainObject.GetComponent<SwitchDoor>().offSwitch.GetComponent<SpriteRenderer>().sprite;
    }

    #region < ���Ĺ���ϵͳ >

    /// <summary>
    /// ���������״̬
    /// </summary>
    /// <param name="state"></param>
    private void ChangeSwitchDoorState(bool state)
    {
        isEnable = state;
    }

    /// <summary>
    /// �����Ÿ�����Ĺ���
    /// </summary>
    private void SwitchDoorFunction()
    {
        // δ����״̬��״̬�������岻��ͬʱ
        if (!isEnable && isEnable != mainObject.GetComponent<SwitchDoor>().isEnable) 
        {
            if (canRotate)
            {
                // ����������
                StartCoroutine(SwitchDoorOpen());
                return;
            }
        }
        // ����״̬��״̬�������岻��ͬʱ
        if (isEnable && isEnable != mainObject.GetComponent<SwitchDoor>().isEnable)
        {
            if (canRotate)
            {
                // �رջ�����
                StartCoroutine(SwitchDoorClose());
                return;
            }

        }
    }

    /// <summary>
    /// �����ſ���Э��
    /// </summary>
    /// <returns></returns>
    private IEnumerator SwitchDoorOpen()
    {
        // ��תʵ����ʱ
        float rotateElapsed = 0;
        // ��ʼ��ת
        Quaternion startRotation = switchDoor.transform.rotation;
        // ��ֹ��ת
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, rotateAngle);

        // ����״̬����
        ChangeSwitchDoorState(true);

        while (rotateElapsed < rotateDuration)
        {
            // ��ֹ��ת
            canRotate = false;

            // �����ֵ������0~1��
            float t = rotateElapsed / rotateDuration;

            // ��ѡ����ӻ���Ч������ƽ��������
            t = Mathf.SmoothStep(0, 1, t);

            // Ӧ�������ֵ��ת
            switchDoor.transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);

            // ��ʱ��
            rotateElapsed += Time.deltaTime;
            // �ȴ�
            yield return null;
        }
        // ȷ����ȷ����Ŀ��Ƕ�
        switchDoor.transform.rotation = endRotation;

        // ������ת
        canRotate = true;

        yield return null;
    }

    /// <summary>
    /// �����Źر�Э��
    /// </summary>
    /// <returns></returns>
    private IEnumerator SwitchDoorClose()
    {
        // ��תʵ����ʱ
        float rotateElapsed = 0;
        // ��ʼ��ת
        Quaternion startRotation = switchDoor.transform.rotation;
        // ��ֹ��ת
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, -rotateAngle);

        // ����״̬����
        ChangeSwitchDoorState(false);

        while (rotateElapsed < rotateDuration)
        {
            // ��ֹ��ת
            canRotate = false;

            // �����ֵ������0~1��
            float t = rotateElapsed / rotateDuration;

            // ��ѡ����ӻ���Ч������ƽ��������
            t = Mathf.SmoothStep(0, 1, t);

            // Ӧ�������ֵ��ת
            switchDoor.transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);

            // ��ʱ��
            rotateElapsed += Time.deltaTime;
            // �ȴ�
            yield return null;
        }

        // ȷ����ȷ����Ŀ��Ƕ�
        switchDoor.transform.rotation = endRotation;

        // ������ת
        canRotate = true;

        yield return null;
    }

    #endregion

}
