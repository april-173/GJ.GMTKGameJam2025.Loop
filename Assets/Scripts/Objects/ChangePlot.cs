using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public enum Direction
{
    None,
    Up,
    Down,
    Left,
    Right,
}

public class ChangePlot : MonoBehaviour
{
    // 玩家控制器脚本
    private PlayerController playerController;

    [Tooltip("移动总时长")]
    [SerializeField] private float changeDuration;

    [Header("基础属性")]
    [Tooltip("缩小地块")]
    [SerializeField] private GameObject reducePlot;
    [Tooltip("缩小地块方向")]
    [SerializeField] private Direction reducePlotDirection;
    [Tooltip("是否与缩小地块进行接触")]
    [SerializeField] private bool isTriggerReducePlot;
    [Tooltip("缩小地块缩小比例")]
    [SerializeField] private float reducePlotReduceScale;
    [Space]
    [Tooltip("放大地块")]
    [SerializeField] private GameObject enlargePlot;
    [Tooltip("放大地块方向")]
    [SerializeField] private Direction enlargePlotDirection;
    [Tooltip("是否与放大地块进行接触")]
    [SerializeField] private bool isTriggerEnlargePlot;
    [Tooltip("放大地块放大比例")]
    [SerializeField] private float enlargePlotEnlargeScale;

    // 缩小地块向量方向
    private Vector3 reducePlotDirectionVector;
    // 放大地块向量方向
    private Vector3 enlargePlotDirectionVector;

    // 缩小地块目的地
    private Vector3 reducePlotTargetPoint;
    // 放大地块目的地
    private Vector3 enlargePlotTargetPoint;

    private void OnEnable()
    {
        //获取玩家控制器脚本
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Start()
    {
        PlotDirectionVectorAcquire();
    }

    private void Update()
    {
        TriggerInspect();
        TargetPointAcquire();
        ChangePlotSystem();
    }



    #region < 数据自动获取 >

    /// <summary>
    /// 地块方向向量获取
    /// </summary>
    private void PlotDirectionVectorAcquire()
    {
        // 根据地块方向类型获取对应的向量

        switch (reducePlotDirection)
        {
            case Direction.Up:
                reducePlotDirectionVector = Vector2.up;
                break;
            case Direction.Down:
                reducePlotDirectionVector = Vector2.down;
                break;
            case Direction.Left:
                reducePlotDirectionVector = Vector2.left;
                break;
            case Direction.Right:
                reducePlotDirectionVector = Vector2.right;
                break;
        }

        switch (enlargePlotDirection)
        {
            case Direction.Up:
                enlargePlotDirectionVector = Vector2.up;
                break;
            case Direction.Down:
                enlargePlotDirectionVector = Vector2.down;
                break;
            case Direction.Left:
                enlargePlotDirectionVector = Vector2.left;
                break;
            case Direction.Right:
                enlargePlotDirectionVector = Vector2.right;
                break;
        }
    }

    /// <summary>
    /// 目的地获取
    /// </summary>
    private void TargetPointAcquire()
    {
        // 计算地块的目标位置
        reducePlotTargetPoint = reducePlot.transform.position + (0.5f + 1.5f * reducePlotReduceScale) * reducePlotDirectionVector;

        enlargePlotTargetPoint = enlargePlot.transform.position + (1.5f + 0.5f * enlargePlotEnlargeScale) * enlargePlotDirectionVector;
    }

    #endregion

    #region < 玩家触发检测 >

    /// <summary>
    /// 触发检测
    /// </summary>
    private void TriggerInspect()
    {
        // 缩小地块是否与玩家接触
        if (playerController.transform.position == reducePlot.transform.position) 
        {
            isTriggerReducePlot = true;
        }
        else
        {
            isTriggerReducePlot = false;
        }

        // 放大地块是否与玩家接触
        if (playerController.transform.position == enlargePlot.transform.position)
        {
            isTriggerEnlargePlot = true;
        }
        else
        {
            isTriggerEnlargePlot = false;
        }
    }

    #endregion

    #region < 核心功能系统 >

    /// <summary>
    /// 变换地块系统
    /// </summary>
    private void ChangePlotSystem()
    {
        // 玩家接触缩小地块
        if (isTriggerReducePlot && !playerController.isConfinement) 
        {
            // 玩家进行地块方向的输入时执行地块功能
            if (reducePlotDirectionVector.x != 0 && reducePlotDirectionVector.x == playerController.moveInput.x) 
            {
                StartCoroutine(Reduce());
            }
            else if (reducePlotDirectionVector.y != 0 && reducePlotDirectionVector.y == playerController.moveInput.y) 
            {
                StartCoroutine(Reduce());
            }
        }

        // 玩家接触放大地块
        if (isTriggerEnlargePlot && !playerController.isConfinement) 
        {
            // 玩家进行地块方向的输入时执行地块功能
            if (enlargePlotDirectionVector.x != 0 && enlargePlotDirectionVector.x == playerController.moveInput.x)
            {
                StartCoroutine(Enlarge());
            }
            else if (enlargePlotDirectionVector.y != 0 && enlargePlotDirectionVector.y == playerController.moveInput.y) 
            {
                StartCoroutine(Enlarge());
            }
        }
    }

    /// <summary>
    /// 缩小
    /// </summary>
    /// <returns></returns>
    private IEnumerator Reduce()
    {
        // 开启玩家禁锢
        playerController.isConfinement = true;

        // 实际用时
        float changeElapsed = 0f;

        while (changeElapsed < changeDuration)
        {
            float t = changeElapsed / changeDuration;

            // 更新玩家位置
            playerController.transform.position = Vector3.Lerp(reducePlot.transform.position, reducePlotTargetPoint, t);
            // 更新玩家尺寸
            playerController.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * reducePlotReduceScale, t);

            // 计时器更新
            changeElapsed += Time.deltaTime;
            // 每帧执行
            yield return null;
        }
        // 确保位置精确
        playerController.transform.position = reducePlotTargetPoint;
        // 确保尺寸精确
        playerController.transform.localScale = Vector3.one * reducePlotReduceScale;

        // 等待
        yield return null;

        // 变换结束玩家瞬移至放大地块位置
        playerController.transform.position = enlargePlot.transform.position;
        // 变换接触玩家尺寸恢复
        playerController.transform.localScale = Vector3.one;

        // 等待
        yield return null;

        // 关闭玩家禁锢
        playerController.isConfinement = false;
        // 允许玩家移动
        playerController.SwitchCanMove(true);
    }

    /// <summary>
    /// 放大
    /// </summary>
    /// <returns></returns>
    private IEnumerator Enlarge()
    {
        // 开启玩家禁锢
        playerController.isConfinement = true;

        // 实际用时
        float changeElapsed = 0f;

        while (changeElapsed < changeDuration)
        {
            float t = changeElapsed / changeDuration;

            // 更新玩家位置
            playerController.transform.position = Vector3.Lerp(enlargePlot.transform.position, enlargePlotTargetPoint, t);
            // 更新玩家尺寸
            playerController.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * enlargePlotEnlargeScale, t);

            // 计时器更新
            changeElapsed += Time.deltaTime;
            // 每帧执行
            yield return null;
        }
        // 确保位置精确
        playerController.transform.position = enlargePlotTargetPoint;
        // 确保尺寸精确
        playerController.transform.localScale = Vector3.one * enlargePlotEnlargeScale;

        // 等待
        yield return null;

        // 变换结束玩家瞬移至缩小地块位置
        playerController.transform.position = reducePlot.transform.position;
        // 变换接触玩家尺寸恢复
        playerController.transform.localScale = Vector3.one;

        // 等待
        yield return null;

        // 关闭玩家禁锢
        playerController.isConfinement = false;
        // 允许玩家移动
        playerController.SwitchCanMove(true);
    }

    #endregion
}
