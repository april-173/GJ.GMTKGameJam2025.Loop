using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
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

    private void Update()
    {
        // ��������״̬ʵʱ��������ť��ʾ��ͼ��
        gridButtonImage.GetComponent<Image>().sprite = gridActive ? turnOnImage : turnOffImage;
        // ����ɨ����״̬ʵʱ��������ť��ʾ��ͼ��
        scanningLineButtonImage.GetComponent<Image>().sprite = scanningLineActive ? turnOnImage : turnOffImage;
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
}
