using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    [Tooltip("Volume引用")]
    [SerializeField] private Volume globalVolume;

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

    [Header("暗角设置")]
    [Tooltip("暗角开启状态")]
    [SerializeField] private bool vignetteActive = false;
    [Tooltip("暗角")]
    [SerializeField] private Vignette vignette;
    [Tooltip("暗角按钮图像")]
    [SerializeField] private Image vignetteButtonImage;

    [Header("BGM设置")]
    [Tooltip("BGM开启状态")]
    [SerializeField] private bool BGMActive = true;
    [Tooltip("暗角")]
    [SerializeField] private GameObject BGM;
    [Tooltip("暗角按钮图像")]
    [SerializeField] private Image BGMButtonImage;

    [Header("SE设置")]
    [Tooltip("SE开启状态")]
    [SerializeField] private bool SEActive = true;
    [Tooltip("暗角")]
    [SerializeField] private AudioManager audioManager;
    [Tooltip("暗角按钮图像")]
    [SerializeField] private Image SEButtonImage;

    // 过渡实体
    private GameObject transition;

    private void Start()
    {
        // 寻找并获取transition
        transition = GameObject.FindGameObjectWithTag("Transition");

        GetVolumeComponents();
        vignette.active = vignetteActive;
    }

    private void Update()
    {
        // 根据网格状态实时更新网格按钮显示的图像
        gridButtonImage.GetComponent<Image>().sprite = gridActive ? turnOnImage : turnOffImage;
        // 根据扫描线状态实时更新网格按钮显示的图像
        scanningLineButtonImage.GetComponent<Image>().sprite = scanningLineActive ? turnOnImage : turnOffImage;
        // 根据暗角状态实时更新网格按钮显示的图像
        vignetteButtonImage.GetComponent<Image>().sprite = vignetteActive ? turnOnImage : turnOffImage;
        // 根据BGM状态实时更新网格按钮显示的图像
        BGMButtonImage.GetComponent<Image>().sprite = BGMActive ? turnOnImage : turnOffImage;
        // 根据SE状态实时更新网格按钮显示的图像
        SEButtonImage.GetComponent<Image>().sprite = SEActive ? turnOnImage : turnOffImage;
    }

    private void GetVolumeComponents()
    {
        if (globalVolume.profile == null)
        {
            return;
        }

        // 获取Vignette效果
        globalVolume.profile.TryGet(out vignette);
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

    /// <summary>
    /// 切换暗角显示
    /// </summary>
    public void ToggleVignette()
    {
        vignetteActive = !vignetteActive;
        vignette.active = vignetteActive;
    }

    /// <summary>
    /// 切换BGM显示
    /// </summary>
    public void ToggleBGM()
    {
        // 切换网格显示状态
        BGMActive = !BGMActive;
        // 更改BGM状态
        BGM.SetActive(BGMActive);
    }

    /// <summary>
    /// 切换音效显示
    /// </summary>
    public void ToggleSE()
    {
        // 切换网格显示状态
        SEActive = !SEActive;
        // 更改SE播放状态
        audioManager.canPlay = SEActive;
    }


    public void MainMenu()
    {
        StartCoroutine(ChangeScene());
    }

    private IEnumerator ChangeScene()
    {
        // 等待时间
        float WaitingTime = transition.GetComponent<Transition>().TransitionDuration;
        // 开启关卡过渡
        StartCoroutine(transition.GetComponent<Transition>().StartTransition());
        // 等待
        yield return new WaitForSeconds(WaitingTime);
        // 切换场景
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// 基本退出方法
    /// </summary>
    public void QuitGame()
    {
        // 在独立平台（Windows、Mac、Linux）上退出应用程序
        Application.Quit();

        // 在Unity编辑器中停止播放模式
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }
}
