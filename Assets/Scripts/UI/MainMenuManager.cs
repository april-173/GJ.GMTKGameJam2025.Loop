using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{

    public GameObject about;

    // ����ʵ��
    private GameObject transition;

    private void Start()
    {
        // Ѱ�Ҳ���ȡtransition
        transition = GameObject.FindGameObjectWithTag("Transition");
    }

    public void StartGame()
    {
        StartCoroutine(ChangeScene());
    }

    private IEnumerator ChangeScene()
    {
        // �ȴ�ʱ��
        float WaitingTime = transition.GetComponent<Transition>().TransitionDuration;
        // �����ؿ�����
        StartCoroutine(transition.GetComponent<Transition>().StartTransition());
        // �ȴ�
        yield return new WaitForSeconds(WaitingTime);
        // �л�����
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// ������Ϸ
    /// </summary>
    public void AboutGame()
    {
        about.SetActive(!about.activeSelf);
    }

    /// <summary>
    /// �����˳�����
    /// </summary>
    public void ExitGame()
    {
        // �ڶ���ƽ̨��Windows��Mac��Linux�����˳�Ӧ�ó���
        Application.Quit();

        // ��Unity�༭����ֹͣ����ģʽ
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
