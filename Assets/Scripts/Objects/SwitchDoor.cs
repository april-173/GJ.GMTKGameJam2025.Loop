using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwitchDoor : MonoBehaviour
{
    // 玩家控制器组件
    private PlayerController playerController;

    // 碰撞器组件
    private Collider2D col;
    // 精灵渲染器组件
    private SpriteRenderer sr;

    [Header("机关门状态")]
    [Tooltip("机关状态")]
    public bool isEnable;
    [Tooltip("机关初始状态")]
    public bool originalState;
    [Space]
    [Tooltip("开启开关（名词）")]
    public GameObject onSwitch;
    [Tooltip("关闭开关（名词）")]
    public GameObject offSwitch;
    [Space]
    [Tooltip("开关启用状态精灵")]
    [SerializeField] private Sprite switchOnSprite;
    [Tooltip("开关停用状态精灵")]
    [SerializeField] private Sprite switchOffSprite;
    [Space]
    [Tooltip("接触开启开关")]
    [SerializeField] private bool isTriggerOnSwitch;
    [Tooltip("接触关闭开关")]
    [SerializeField] private bool isTriggerOffSwitch;

    [Header("机关门旋转")]
    [Tooltip("旋转权限")]
    [SerializeField] private bool canRotate = true;
    [Tooltip("旋转总时长")]
    [SerializeField] private float rotateDuration;
    [Tooltip("旋转角度")]
    [SerializeField] private float rotateAngle;

    private void Start()
    {
        // 获取相关组件
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

        //获取玩家控制器脚本
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }


    private void Update()
    {
        SpriteUpdate();
        SwitchDoorFunction();
        TriggerInspect();
    }

    #region < 核心功能系统 >

    /// <summary>
    /// 变更机关门状态
    /// </summary>
    /// <param name="state"></param>
    private void ChangeSwitchDoorState(bool state)
    {
        isEnable = state;
    }

    /// <summary>
    /// 精灵更新
    /// </summary>
    private void SpriteUpdate()
    {
        // 根据机关门状态更改开关显示的精灵
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
    /// 接触监测
    /// </summary>
    private void TriggerInspect()
    {
        // 根据玩家接触情况更改接触状态
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
    /// 机关们核心功能
    /// </summary>
    private void SwitchDoorFunction()
    {
        // 机关门关闭时玩家接触开启开关
        if(!isEnable && isTriggerOnSwitch)
        {
            if (canRotate)
            {
                // 开启机关门
                StartCoroutine(SwitchDoorOpen());
                return;
            }
        }
        // 机关门开启时玩家接触关闭开关
        if (isEnable && isTriggerOffSwitch)
        {
            if (canRotate)
            {
                // 关闭机关门
                StartCoroutine(SwitchDoorClose());
                return;
            }

        }
    }

    /// <summary>
    /// 机关门开启协程
    /// </summary>
    /// <returns></returns>
    private IEnumerator SwitchDoorOpen()
    {
        // 旋转实际用时
        float rotateElapsed = 0;
        // 起始旋转
        Quaternion startRotation = transform.rotation;
        // 终止旋转
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, rotateAngle);

        // 记录位置
        Vector3 onSwitchPosition = onSwitch.transform.position;
        Vector3 offSwitchPosition = offSwitch.transform.position;

        // 脱离父物体（保持世界位置）
        onSwitch.transform.SetParent(transform.parent, true);
        offSwitch.transform.SetParent(transform.parent, true);

        // 开关状态更新
        ChangeSwitchDoorState(true);
        // 更新阻挡状态
        col.enabled = false;

        while (rotateElapsed < rotateDuration)
        {
            // 禁止旋转
            canRotate = false;

            // 计算插值比例（0~1）
            float t = rotateElapsed / rotateDuration;

            // 可选：添加缓动效果（如平滑步进）
            t = Mathf.SmoothStep(0, 1, t);

            // 应用球面插值旋转
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);

            // 计时器
            rotateElapsed += Time.deltaTime;
            // 等待
            yield return null;
        }
        // 确保精确到达目标角度
        transform.rotation = endRotation;

        // 颜色每帧变更值
        float changeColorValue = 0.05f;
        // 尺寸每帧变更值
        float changeScaleValue = 0.02f;
        // 循环10帧
        for (int i = 0; i < 10; i++)
        {
            // 透明度每帧递减
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - changeColorValue);
            // 尺寸每帧递减
            transform.localScale = new Vector3(transform.localScale.x - changeScaleValue, transform.localScale.y - changeScaleValue);
            // 等待
            yield return null;
        }

        // 重新设置父物体（不保持世界位置）
        onSwitch.transform.SetParent(transform, true);
        offSwitch.transform.SetParent(transform, true);

        // 恢复位置
        onSwitch.transform.position = new Vector3(Mathf.RoundToInt(onSwitchPosition.x), Mathf.RoundToInt(onSwitchPosition.y), Mathf.RoundToInt(onSwitchPosition.z));
        offSwitch.transform.position = new Vector3(Mathf.RoundToInt(offSwitchPosition.x), Mathf.RoundToInt(offSwitchPosition.y), Mathf.RoundToInt(offSwitchPosition.z));

        // 允许旋转
        canRotate = true;

        yield return null;
    }

    /// <summary>
    /// 机关门关闭协程
    /// </summary>
    /// <returns></returns>
    private IEnumerator SwitchDoorClose()
    {
        // 旋转实际用时
        float rotateElapsed = 0;
        // 起始旋转
        Quaternion startRotation = transform.rotation;
        // 终止旋转
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, -rotateAngle);

        // 记录位置
        Vector3 onSwitchPosition = onSwitch.transform.position;
        Vector3 offSwitchPosition = offSwitch.transform.position;

        // 脱离父物体（保持世界位置）
        onSwitch.transform.SetParent(transform.parent, true);
        offSwitch.transform.SetParent(transform.parent, true);

        // 开关状态更新
        ChangeSwitchDoorState(false);
        // 更新阻挡状态
        col.enabled = true;

        while (rotateElapsed < rotateDuration)
        {
            // 禁止旋转
            canRotate = false;

            // 计算插值比例（0~1）
            float t = rotateElapsed / rotateDuration;

            // 可选：添加缓动效果（如平滑步进）
            t = Mathf.SmoothStep(0, 1, t);

            // 应用球面插值旋转
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);

            // 计时器
            rotateElapsed += Time.deltaTime;
            // 等待
            yield return null;
        }

        // 确保精确到达目标角度
        transform.rotation = endRotation;

        // 颜色每帧变更值
        float changeColorValue = 0.05f;
        // 尺寸每帧变更值
        float changeScaleValue = 0.02f;
        // 循环10帧
        for (int i = 0; i < 10; i++)
        {
            // 透明度每帧递增
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a + changeColorValue);
            // 尺寸每帧递增
            transform.localScale = new Vector3(transform.localScale.x + changeScaleValue, transform.localScale.y + changeScaleValue);
            // 等待
            yield return null;
        }

        // 重新设置父物体（不保持世界位置）
        onSwitch.transform.SetParent(transform, true);
        offSwitch.transform.SetParent(transform, true);

        // 恢复位置
        onSwitch.transform.position = new Vector3(Mathf.RoundToInt(onSwitchPosition.x), Mathf.RoundToInt(onSwitchPosition.y), Mathf.RoundToInt(onSwitchPosition.z));
        offSwitch.transform.position = new Vector3(Mathf.RoundToInt(offSwitchPosition.x), Mathf.RoundToInt(offSwitchPosition.y), Mathf.RoundToInt(offSwitchPosition.z)); 

        // 允许旋转
        canRotate = true;

        yield return null;
    }

    #endregion

}
