using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwitchDoor : MonoBehaviour
{
    // ��ҿ��������
    private PlayerController playerController;

    // ��ײ�����
    private Collider2D col;
    // ������Ⱦ�����
    private SpriteRenderer sr;

    [Header("������״̬")]
    [Tooltip("����״̬")]
    public bool isEnable;
    [Tooltip("���س�ʼ״̬")]
    public bool originalState;
    [Space]
    [Tooltip("�������أ����ʣ�")]
    public GameObject onSwitch;
    [Tooltip("�رտ��أ����ʣ�")]
    public GameObject offSwitch;
    [Space]
    [Tooltip("��������״̬����")]
    [SerializeField] private Sprite switchOnSprite;
    [Tooltip("����ͣ��״̬����")]
    [SerializeField] private Sprite switchOffSprite;
    [Space]
    [Tooltip("�Ӵ���������")]
    [SerializeField] private bool isTriggerOnSwitch;
    [Tooltip("�Ӵ��رտ���")]
    [SerializeField] private bool isTriggerOffSwitch;

    [Header("��������ת")]
    [Tooltip("��תȨ��")]
    [SerializeField] private bool canRotate = true;
    [Tooltip("��ת��ʱ��")]
    [SerializeField] private float rotateDuration;
    [Tooltip("��ת�Ƕ�")]
    [SerializeField] private float rotateAngle;

    private void Start()
    {
        // ��ȡ������
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        if (isEnable && isEnable != originalState)
        {
            StartCoroutine(SwitchDoorClose());
        }
        if (!isEnable && isEnable != originalState)
        {
            StartCoroutine(SwitchDoorOpen());
        }

        //��ȡ��ҿ������ű�
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }


    private void Update()
    {
        SpriteUpdate();
        SwitchDoorFunction();
        TriggerInspect();
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
    /// �������
    /// </summary>
    private void SpriteUpdate()
    {
        // ���ݻ�����״̬���Ŀ�����ʾ�ľ���
        if (isEnable)
        {
            onSwitch.gameObject.GetComponent<SpriteRenderer>().sprite = switchOnSprite;
            offSwitch.gameObject.GetComponent <SpriteRenderer>().sprite = switchOffSprite;
        }
        else
        {
            onSwitch.gameObject.GetComponent<SpriteRenderer>().sprite = switchOffSprite;
            offSwitch.gameObject.GetComponent<SpriteRenderer>().sprite = switchOnSprite;
        }
    }

    /// <summary>
    /// �Ӵ����
    /// </summary>
    private void TriggerInspect()
    {
        // ������ҽӴ�������ĽӴ�״̬
        if (playerController.transform.position == onSwitch.transform.position)
        {
            isTriggerOnSwitch = true;
        }
        else
        {
            isTriggerOnSwitch = false;
        }

        if (playerController.transform.position == offSwitch.transform.position) 
        {
            isTriggerOffSwitch = true;
        }
        else
        {
            isTriggerOffSwitch = false;
        }
    }

    /// <summary>
    /// �����Ǻ��Ĺ���
    /// </summary>
    private void SwitchDoorFunction()
    {
        // �����Źر�ʱ��ҽӴ���������
        if(!isEnable && isTriggerOnSwitch)
        {
            if (canRotate)
            {
                // ����������
                StartCoroutine(SwitchDoorOpen());
                return;
            }
        }
        // �����ſ���ʱ��ҽӴ��رտ���
        if (isEnable && isTriggerOffSwitch)
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
        Quaternion startRotation = transform.rotation;
        // ��ֹ��ת
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, rotateAngle);

        // ��¼λ��
        Vector3 onSwitchPosition = onSwitch.transform.position;
        Vector3 offSwitchPosition = offSwitch.transform.position;

        // ���븸���壨��������λ�ã�
        onSwitch.transform.SetParent(transform.parent, true);
        offSwitch.transform.SetParent(transform.parent, true);

        // ����״̬����
        ChangeSwitchDoorState(true);
        // �����赲״̬
        col.enabled = false;

        while (rotateElapsed < rotateDuration)
        {
            // ��ֹ��ת
            canRotate = false;

            // �����ֵ������0~1��
            float t = rotateElapsed / rotateDuration;

            // ��ѡ����ӻ���Ч������ƽ��������
            t = Mathf.SmoothStep(0, 1, t);

            // Ӧ�������ֵ��ת
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);

            // ��ʱ��
            rotateElapsed += Time.deltaTime;
            // �ȴ�
            yield return null;
        }
        // ȷ����ȷ����Ŀ��Ƕ�
        transform.rotation = endRotation;

        // ��ɫÿ֡���ֵ
        float changeColorValue = 0.05f;
        // �ߴ�ÿ֡���ֵ
        float changeScaleValue = 0.02f;
        // ѭ��10֡
        for (int i = 0; i < 10; i++)
        {
            // ͸����ÿ֡�ݼ�
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - changeColorValue);
            // �ߴ�ÿ֡�ݼ�
            transform.localScale = new Vector3(transform.localScale.x - changeScaleValue, transform.localScale.y - changeScaleValue);
            // �ȴ�
            yield return null;
        }

        // �������ø����壨����������λ�ã�
        onSwitch.transform.SetParent(transform, true);
        offSwitch.transform.SetParent(transform, true);

        // �ָ�λ��
        onSwitch.transform.position = new Vector3(Mathf.RoundToInt(onSwitchPosition.x), Mathf.RoundToInt(onSwitchPosition.y), Mathf.RoundToInt(onSwitchPosition.z));
        offSwitch.transform.position = new Vector3(Mathf.RoundToInt(offSwitchPosition.x), Mathf.RoundToInt(offSwitchPosition.y), Mathf.RoundToInt(offSwitchPosition.z));

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
        Quaternion startRotation = transform.rotation;
        // ��ֹ��ת
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, -rotateAngle);

        // ��¼λ��
        Vector3 onSwitchPosition = onSwitch.transform.position;
        Vector3 offSwitchPosition = offSwitch.transform.position;

        // ���븸���壨��������λ�ã�
        onSwitch.transform.SetParent(transform.parent, true);
        offSwitch.transform.SetParent(transform.parent, true);

        // ����״̬����
        ChangeSwitchDoorState(false);
        // �����赲״̬
        col.enabled = true;

        while (rotateElapsed < rotateDuration)
        {
            // ��ֹ��ת
            canRotate = false;

            // �����ֵ������0~1��
            float t = rotateElapsed / rotateDuration;

            // ��ѡ����ӻ���Ч������ƽ��������
            t = Mathf.SmoothStep(0, 1, t);

            // Ӧ�������ֵ��ת
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);

            // ��ʱ��
            rotateElapsed += Time.deltaTime;
            // �ȴ�
            yield return null;
        }

        // ȷ����ȷ����Ŀ��Ƕ�
        transform.rotation = endRotation;

        // ��ɫÿ֡���ֵ
        float changeColorValue = 0.05f;
        // �ߴ�ÿ֡���ֵ
        float changeScaleValue = 0.02f;
        // ѭ��10֡
        for (int i = 0; i < 10; i++)
        {
            // ͸����ÿ֡����
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a + changeColorValue);
            // �ߴ�ÿ֡����
            transform.localScale = new Vector3(transform.localScale.x + changeScaleValue, transform.localScale.y + changeScaleValue);
            // �ȴ�
            yield return null;
        }

        // �������ø����壨����������λ�ã�
        onSwitch.transform.SetParent(transform, true);
        offSwitch.transform.SetParent(transform, true);

        // �ָ�λ��
        onSwitch.transform.position = new Vector3(Mathf.RoundToInt(onSwitchPosition.x), Mathf.RoundToInt(onSwitchPosition.y), Mathf.RoundToInt(onSwitchPosition.z));
        offSwitch.transform.position = new Vector3(Mathf.RoundToInt(offSwitchPosition.x), Mathf.RoundToInt(offSwitchPosition.y), Mathf.RoundToInt(offSwitchPosition.z)); 

        // ������ת
        canRotate = true;

        yield return null;
    }

    #endregion

}
