using UnityEngine;
using System.Collections;

public class Transition : MonoBehaviour
{
    [Header("��������")]
    [Tooltip("�������ɲ���")]
    [SerializeField] private Material transitionMaterial;
    [Space]
    [Tooltip("����������ʱ��")]
    [SerializeField] private float transitionDuration;

    // ��ҿ��������
    private PlayerController playerController;

    private void Start()
    {
        // ȷ�����ʴ��ڣ�����ʹ�����л��ֶΣ������ȡ�������
        if (transitionMaterial == null)
        {
            transitionMaterial = GetComponent<SpriteRenderer>()?.material;
        }

        // ��ȡ��ҿ��������
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        StartCoroutine(StopTransition());
    }

    #region < �������� >

    /// <summary>
    /// ������������
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartTransition()
    {
        // ��ҿ�������״̬
        playerController.isConfinement = true;

        // ��ȫ���
        if (transitionMaterial == null) yield break;

        // �ؿ�����ʵ����ʱ
        float transitionElapsed = 0;

        // �淶��ʼ��ֵ
        transitionMaterial.SetFloat("_Slider", 2);

        while (transitionElapsed < transitionDuration)
        {
            // ƽ���任��ֵ
            transitionMaterial.SetFloat("_Slider", Mathf.Lerp(2, 0, transitionElapsed / transitionDuration));
            // ��ʱ��
            transitionElapsed += Time.deltaTime;
            // �ȴ�
            yield return null;
        }

        // ȷ������ֵ��ȷ
        transitionMaterial.SetFloat("_Slider", 0);
    }

    /// <summary>
    /// �رճ�������
    /// </summary>
    /// <returns></returns>
    public IEnumerator StopTransition()
    {
        // ��ȫ���
        if (transitionMaterial == null) yield break;

        // ��ҿ�������״̬
        playerController.isConfinement = true;

        // �ؿ�����ʵ����ʱ
        float transitionElapsed = 0;
        
        // �淶��ʼ��ֵ 
        transitionMaterial.SetFloat("_Slider", 0);

        while (transitionElapsed < transitionDuration)
        {
            // ƽ���任��ֵ
            transitionMaterial.SetFloat("_Slider", Mathf.Lerp(0, 2, transitionElapsed / transitionDuration));
            // ��ʱ��
            transitionElapsed += Time.deltaTime;
            // �ȴ�
            yield return null;
        }

        // ȷ������ֵ��ȷ
        transitionMaterial.SetFloat("_Slider", 2);

        // ��ҹرս���״̬
        playerController.isConfinement = false;

        // ��������ƶ�
        playerController.SwitchCanMove(true);
    }

    #endregion

    #region < �ⲿ��ȡ���� >

    public float TransitionDuration
    {  get { return transitionDuration; } }

    #endregion
}
