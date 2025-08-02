using UnityEngine;
using System.Collections;

public class Transition : MonoBehaviour
{
    [Header("场景过渡")]
    [Tooltip("场景过渡材质")]
    [SerializeField] private Material transitionMaterial;
    [Space]
    [Tooltip("场景过渡总时长")]
    [SerializeField] private float transitionDuration;

    // 玩家控制器组件
    private PlayerController playerController;

    private void Start()
    {
        // 确保材质存在：优先使用序列化字段，否则获取自身材质
        if (transitionMaterial == null)
        {
            transitionMaterial = GetComponent<SpriteRenderer>()?.material;
        }

        // 获取玩家控制器组件
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        StartCoroutine(StopTransition());
    }

    #region < 场景过渡 >

    /// <summary>
    /// 开启场景过渡
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartTransition()
    {
        // 玩家开启禁锢状态
        playerController.isConfinement = true;

        // 安全检测
        if (transitionMaterial == null) yield break;

        // 关卡过渡实际用时
        float transitionElapsed = 0;

        // 规范初始数值
        transitionMaterial.SetFloat("_Slider", 2);

        while (transitionElapsed < transitionDuration)
        {
            // 平滑变换数值
            transitionMaterial.SetFloat("_Slider", Mathf.Lerp(2, 0, transitionElapsed / transitionDuration));
            // 计时器
            transitionElapsed += Time.deltaTime;
            // 等待
            yield return null;
        }

        // 确保最终值精确
        transitionMaterial.SetFloat("_Slider", 0);
    }

    /// <summary>
    /// 关闭场景过渡
    /// </summary>
    /// <returns></returns>
    public IEnumerator StopTransition()
    {
        // 安全检测
        if (transitionMaterial == null) yield break;

        // 玩家开启禁锢状态
        playerController.isConfinement = true;

        // 关卡过渡实际用时
        float transitionElapsed = 0;
        
        // 规范初始数值 
        transitionMaterial.SetFloat("_Slider", 0);

        while (transitionElapsed < transitionDuration)
        {
            // 平滑变换数值
            transitionMaterial.SetFloat("_Slider", Mathf.Lerp(0, 2, transitionElapsed / transitionDuration));
            // 计时器
            transitionElapsed += Time.deltaTime;
            // 等待
            yield return null;
        }

        // 确保最终值精确
        transitionMaterial.SetFloat("_Slider", 2);

        // 玩家关闭禁锢状态
        playerController.isConfinement = false;

        // 允许玩家移动
        playerController.SwitchCanMove(true);
    }

    #endregion

    #region < 外部获取参数 >

    public float TransitionDuration
    {  get { return transitionDuration; } }

    #endregion
}
