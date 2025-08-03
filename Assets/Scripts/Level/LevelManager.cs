using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelsManager : MonoBehaviour
{
    [Header("�ؿ��л�")]
    [Tooltip("�ؿ�����")]
    [SerializeField] private List<GameObject> levels = new List<GameObject>();
    [Space]
    [Tooltip("��ǰ�ؿ����")]
    public int currentLevelNumber = 0;
    [Tooltip("Ŀ��ؿ����")]
    public int targetLevelNumber = 0;
    [Space]
    [Tooltip("�Ƿ������л��ؿ�")]
    [SerializeField] private bool canSwitch = true;

    // ����ʵ��
    private GameObject transition;

    private void Start()
    {

        // ����������
        for (int i = 0; i < transform.childCount; i++)
        {
            // ��ȡ������
            levels.Add(transform.GetChild(i).gameObject);
            // ����ÿһ��������һ���ؿ����
            levels[i].GetComponent<Level>().levelNumber = i;
        }

        // Ѱ�Ҳ���ȡtransition
        transition = GameObject.FindGameObjectWithTag("Transition");

        // ��ʾ�ؿ��ϼ��еĵ�һ���ؿ�
        SwitchLevel(0);
    }

    private void Update()
    {
        if (canSwitch && targetLevelNumber != currentLevelNumber)
        {
            StartCoroutine(SwitchLevelController(targetLevelNumber));
        }
    }

    #region < �ؿ��л� >

    /// <summary>
    /// �л��ؿ�
    /// </summary>
    /// <param name="number">�ؿ����</param>
    private void SwitchLevel(int number)
    {
        // �����ؿ��ϼ�
        for (int i = 0; i < levels.Count; i++)
        {
            // �ر����йؿ�����ʾ
            levels[i].SetActive(false);
        }

        // ��ʾָ���ؿ�
        levels[number].SetActive(true);
        // ���¹ؿ����
        currentLevelNumber = number;
        targetLevelNumber = number;
    }

    /// <summary>
    /// �ؿ��л�����Э��
    /// </summary>
    /// <param name="number">�ؿ����</param>
    /// <returns></returns>
    public IEnumerator SwitchLevelController(int number)
    {
        // ��ֹ�л��ؿ�
        canSwitch = false;

        // �ȴ�ʱ��
        float WaitingTime = transition.GetComponent<Transition>().TransitionDuration;

        // �ؿ����С�ڹؿ���Ŀʱ
        if (number < levels.Count)
        {
            // �����ؿ�����
            StartCoroutine(transition.GetComponent<Transition>().StartTransition());
            // �ȴ�
            yield return new WaitForSeconds(WaitingTime);
            // �л��ؿ�
            SwitchLevel(number);
            // �رչؿ�����
            StartCoroutine(transition.GetComponent<Transition>().StopTransition());
        }

        // �����л��ؿ�
        canSwitch = true;
    }

    #endregion
}
