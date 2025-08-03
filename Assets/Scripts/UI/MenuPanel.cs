using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    [Tooltip("Volume����")]
    [SerializeField] private Volume globalVolume;

    [Header("��ťͼ��")]
    [Tooltip("����ͼ��")]
    [SerializeField] private Sprite turnOnImage;
    [Tooltip("�ر�ͼ��")]
    [SerializeField] private Sprite turnOffImage;

    [Header("��������")]
    [Tooltip("������״̬")]
    [SerializeField] private bool gridActive = true;
    [Tooltip("����")]
    [SerializeField] private GameObject grid;
    [Tooltip("����ťͼ��")]
    [SerializeField] private Image gridButtonImage;

    [Header("ɨ��������")]
    [Tooltip("ɨ���߿���״̬")]
    [SerializeField] private bool scanningLineActive = true;
    [Tooltip("ɨ����")]
    [SerializeField] private Material scanningLine;
    [Tooltip("ɨ���߰�ťͼ��")]
    [SerializeField] private Image scanningLineButtonImage;

    [Header("��������")]
    [Tooltip("���ǿ���״̬")]
    [SerializeField] private bool vignetteActive = false;
    [Tooltip("����")]
    [SerializeField] private Vignette vignette;
    [Tooltip("���ǰ�ťͼ��")]
    [SerializeField] private Image vignetteButtonImage;

    [Header("BGM����")]
    [Tooltip("BGM����״̬")]
    [SerializeField] private bool BGMActive = true;
    [Tooltip("����")]
    [SerializeField] private GameObject BGM;
    [Tooltip("���ǰ�ťͼ��")]
    [SerializeField] private Image BGMButtonImage;

    [Header("SE����")]
    [Tooltip("SE����״̬")]
    [SerializeField] private bool SEActive = true;
    [Tooltip("����")]
    [SerializeField] private AudioManager audioManager;
    [Tooltip("���ǰ�ťͼ��")]
    [SerializeField] private Image SEButtonImage;

    // ����ʵ��
    private GameObject transition;

    private void Start()
    {
        // Ѱ�Ҳ���ȡtransition
        transition = GameObject.FindGameObjectWithTag("Transition");

        GetVolumeComponents();
        vignette.active = vignetteActive;
    }

    private void Update()
    {
        // ��������״̬ʵʱ��������ť��ʾ��ͼ��
        gridButtonImage.GetComponent<Image>().sprite = gridActive ? turnOnImage : turnOffImage;
        // ����ɨ����״̬ʵʱ��������ť��ʾ��ͼ��
        scanningLineButtonImage.GetComponent<Image>().sprite = scanningLineActive ? turnOnImage : turnOffImage;
        // ���ݰ���״̬ʵʱ��������ť��ʾ��ͼ��
        vignetteButtonImage.GetComponent<Image>().sprite = vignetteActive ? turnOnImage : turnOffImage;
        // ����BGM״̬ʵʱ��������ť��ʾ��ͼ��
        BGMButtonImage.GetComponent<Image>().sprite = BGMActive ? turnOnImage : turnOffImage;
        // ����SE״̬ʵʱ��������ť��ʾ��ͼ��
        SEButtonImage.GetComponent<Image>().sprite = SEActive ? turnOnImage : turnOffImage;
    }

    private void GetVolumeComponents()
    {
        if (globalVolume.profile == null)
        {
            return;
        }

        // ��ȡVignetteЧ��
        globalVolume.profile.TryGet(out vignette);
    }

    /// <summary>
    /// �л�������ʾ
    /// </summary>
    public void ToggleGrid()
    {
        // �л�������ʾ״̬
        gridActive = !gridActive;
        grid.SetActive(gridActive);
    }

    /// <summary>
    /// �л�ɨ������ʾ
    /// </summary>
    public void ToggleScanningLine()
    {
        // �л�������ʾ״̬
        scanningLineActive = !scanningLineActive;
        // ����ɨ�����߶�����
        if(scanningLineActive)
        {
            scanningLine.SetFloat("_NumberOfScanLines", 200);
        }
        else
        {
            scanningLine.SetFloat("_NumberOfScanLines", 0);
        }
    }

    /// <summary>
    /// �л�������ʾ
    /// </summary>
    public void ToggleVignette()
    {
        vignetteActive = !vignetteActive;
        vignette.active = vignetteActive;
    }

    /// <summary>
    /// �л�BGM��ʾ
    /// </summary>
    public void ToggleBGM()
    {
        // �л�������ʾ״̬
        BGMActive = !BGMActive;
        // ����BGM״̬
        BGM.SetActive(BGMActive);
    }

    /// <summary>
    /// �л���Ч��ʾ
    /// </summary>
    public void ToggleSE()
    {
        // �л�������ʾ״̬
        SEActive = !SEActive;
        // ����SE����״̬
        audioManager.canPlay = SEActive;
    }


    public void MainMenu()
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
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// �����˳�����
    /// </summary>
    public void QuitGame()
    {
        // �ڶ���ƽ̨��Windows��Mac��Linux�����˳�Ӧ�ó���
        Application.Quit();

        // ��Unity�༭����ֹͣ����ģʽ
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }
}
