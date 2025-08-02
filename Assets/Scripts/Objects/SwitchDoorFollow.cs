using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwitchDoorFollow : MonoBehaviour
{

    [Header("跟随设置")]
    [Tooltip("主物体（需要跟随的目标）")]
    [SerializeField] private GameObject mainObject;
    [Tooltip("相较于主物体尺寸")]
    [SerializeField] private float followScale;

    [Header("基础属性")]
    [Tooltip("机关门状态")]
    [SerializeField] private bool isEnable;
    [Space]
    [Tooltip("机关门")]
    [SerializeField] private GameObject switchDoor;
    [Tooltip("开启开关（名词）")]
    [SerializeField] private GameObject onSwitch;
    [Tooltip("关闭开关（名词）")]
    [SerializeField] private GameObject offSwitch;

    [Header("机关门旋转")]
    [Tooltip("旋转权限")]
    [SerializeField] private bool canRotate = true;
    [Tooltip("旋转总时长")]
    [SerializeField] private float rotateDuration;
    [Tooltip("旋转角度")]
    [SerializeField] private float rotateAngle;

    private void Update()
    {
        SwitchDoorFunction();

        switchDoor.transform.localScale = mainObject.transform.localScale * followScale;
        switchDoor.GetComponent<SpriteRenderer>().color = mainObject.GetComponent<SpriteRenderer>().color;

        // 开关跟随主物体变换精灵
        onSwitch.GetComponent<SpriteRenderer>().sprite = mainObject.GetComponent<SwitchDoor>().onSwitch.GetComponent<SpriteRenderer>().sprite;
        offSwitch.GetComponent<SpriteRenderer>().sprite = mainObject.GetComponent<SwitchDoor>().offSwitch.GetComponent<SpriteRenderer>().sprite;
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
    /// 机关门跟随核心功能
    /// </summary>
    private void SwitchDoorFunction()
    {
        // 未开启状态且状态与主物体不相同时
        if (!isEnable && isEnable != mainObject.GetComponent<SwitchDoor>().isEnable) 
        {
            if (canRotate)
            {
                // 开启机关门
                StartCoroutine(SwitchDoorOpen());
                return;
            }
        }
        // 开启状态且状态与主物体不相同时
        if (isEnable && isEnable != mainObject.GetComponent<SwitchDoor>().isEnable)
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
        Quaternion startRotation = switchDoor.transform.rotation;
        // 终止旋转
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, rotateAngle);

        // 开关状态更新
        ChangeSwitchDoorState(true);

        while (rotateElapsed < rotateDuration)
        {
            // 禁止旋转
            canRotate = false;

            // 计算插值比例（0~1）
            float t = rotateElapsed / rotateDuration;

            // 可选：添加缓动效果（如平滑步进）
            t = Mathf.SmoothStep(0, 1, t);

            // 应用球面插值旋转
            switchDoor.transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);

            // 计时器
            rotateElapsed += Time.deltaTime;
            // 等待
            yield return null;
        }
        // 确保精确到达目标角度
        switchDoor.transform.rotation = endRotation;

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
        Quaternion startRotation = switchDoor.transform.rotation;
        // 终止旋转
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, -rotateAngle);

        // 开关状态更新
        ChangeSwitchDoorState(false);

        while (rotateElapsed < rotateDuration)
        {
            // 禁止旋转
            canRotate = false;

            // 计算插值比例（0~1）
            float t = rotateElapsed / rotateDuration;

            // 可选：添加缓动效果（如平滑步进）
            t = Mathf.SmoothStep(0, 1, t);

            // 应用球面插值旋转
            switchDoor.transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);

            // 计时器
            rotateElapsed += Time.deltaTime;
            // 等待
            yield return null;
        }

        // 确保精确到达目标角度
        switchDoor.transform.rotation = endRotation;

        // 允许旋转
        canRotate = true;

        yield return null;
    }

    #endregion

}
