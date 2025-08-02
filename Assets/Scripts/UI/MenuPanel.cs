using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    [Header("按钮图像")]
    [Tooltip("开启图像")]
    [SerializeField] private Sprite turnOnImage;
    [Tooltip("关闭图像")]
    [SerializeField] private Sprite turnOffImage;

    [Header("网格设置")]
    [Tooltip("网格开启状态")]
    [SerializeField] private bool gridActive = true;
    [Tooltip("网格")]
    [SerializeField] private GameObject grid;
    [Tooltip("网格按钮图像")]
    [SerializeField] private Image gridButtonImage;

    [Header("扫描线设置")]
    [Tooltip("扫描线开启状态")]
    [SerializeField] private bool scanningLineActive = true;
    [Tooltip("扫描线")]
    [SerializeField] private Material scanningLine;
    [Tooltip("扫描线按钮图像")]
    [SerializeField] private Image scanningLineButtonImage;

    private void Update()
    {
        // 根据网格状态实时更新网格按钮显示的图像
        gridButtonImage.GetComponent<Image>().sprite = gridActive ? turnOnImage : turnOffImage;
        // 根据扫描线状态实时更新网格按钮显示的图像
        scanningLineButtonImage.GetComponent<Image>().sprite = scanningLineActive ? turnOnImage : turnOffImage;
    }

    /// <summary>
    /// 切换网格显示
    /// </summary>
    public void ToggleGrid()
    {
        // 切换网格显示状态
        gridActive = !gridActive;
        grid.SetActive(gridActive);
    }

    /// <summary>
    /// 切换扫描线显示
    /// </summary>
    public void ToggleScanningLine()
    {
        // 切换网格显示状态
        scanningLineActive = !scanningLineActive;
        // 更改扫描线线段数量
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
