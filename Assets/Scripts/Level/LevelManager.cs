using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelsManager : MonoBehaviour
{
    [Header("关卡切换")]
    [Tooltip("关卡集合")]
    [SerializeField] private List<GameObject> levels = new List<GameObject>();
    [Space]
    [Tooltip("当前关卡编号")]
    public int currentLevelNumber = 0;
    [Tooltip("目标关卡编号")]
    public int targetLevelNumber = 0;
    [Space]
    [Tooltip("是否允许切换关卡")]
    [SerializeField] private bool canSwitch = true;

    // 过渡实体
    private GameObject transition;

    private void Start()
    {

        // 遍历子物体
        for (int i = 0; i < transform.childCount; i++)
        {
            // 获取子物体
            levels.Add(transform.GetChild(i).gameObject);
            // 给予每一个子物体一个关卡编号
            levels[i].GetComponent<Level>().levelNumber = i;
        }

        // 寻找并获取transition
        transition = GameObject.FindGameObjectWithTag("Transition");

        // 显示关卡合集中的第一个关卡
        SwitchLevel(0);
    }

    private void Update()
    {
        if (canSwitch && targetLevelNumber != currentLevelNumber)
        {
            StartCoroutine(SwitchLevelController(targetLevelNumber));
        }
    }

    #region < 关卡切换 >

    /// <summary>
    /// 切换关卡
    /// </summary>
    /// <param name="number">关卡编号</param>
    private void SwitchLevel(int number)
    {
        // 遍历关卡合集
        for (int i = 0; i < levels.Count; i++)
        {
            // 关闭所有关卡的显示
            levels[i].SetActive(false);
        }

        // 显示指定关卡
        levels[number].SetActive(true);
        // 更新关卡编号
        currentLevelNumber = number;
        targetLevelNumber = number;
    }

    /// <summary>
    /// 关卡切换控制协程
    /// </summary>
    /// <param name="number">关卡编号</param>
    /// <returns></returns>
    public IEnumerator SwitchLevelController(int number)
    {
        // 禁止切换关卡
        canSwitch = false;

        // 等待时间
        float WaitingTime = transition.GetComponent<Transition>().TransitionDuration;

        // 关卡编号小于关卡数目时
        if (number < levels.Count)
        {
            // 开启关卡过渡
            StartCoroutine(transition.GetComponent<Transition>().StartTransition());
            // 等待
            yield return new WaitForSeconds(WaitingTime);
            // 切换关卡
            SwitchLevel(number);
            // 关闭关卡过渡
            StartCoroutine(transition.GetComponent<Transition>().StopTransition());
        }

        // 允许切换关卡
        canSwitch = true;
    }

    #endregion
}
