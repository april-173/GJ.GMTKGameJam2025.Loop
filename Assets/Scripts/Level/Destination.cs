using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Destination : MonoBehaviour
{
    // 玩家实体
    private GameObject player;
    // 摄像机震动脚本
    private CameraShake cameraShake;
    // 过渡脚本
    private Transition transition;

    // 是否处于触发状态
    private bool isTrigger;

    // 当前Level组件
    private Level currentLevel;
    // 当前LevelManager组件
    private LevelsManager currentLevelsManager;

    private void Start()
    {
        // 获取摄像机震动脚本
        cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        // 获取过渡脚本
        transition = GameObject.FindGameObjectWithTag("Transition").GetComponent<Transition>();

        // 获取Level组件
        currentLevel = transform.parent.parent.GetComponent<Level>();
        // 获取LevelsManager组件
        currentLevelsManager = transform.parent.parent.parent.GetComponent<LevelsManager>();
    }

    private void OnEnable()
    {
        // 获取玩家实体
        player = GameObject.FindGameObjectWithTag("Player");
        // 关闭触发状态
        isTrigger = false;
    }

    private void Update()
    {
        // 玩家位置与终点位置一致时启动切换关卡协程
        if(player.transform.position == transform.position)
        {
            StartCoroutine(LevelSwitch());
        }
    }


    /// <summary>
    /// 关卡切换协程
    /// </summary>
    /// <returns></returns>
    private IEnumerator LevelSwitch()
    {
        // 若未处于触发状态
        if(!isTrigger)
        {
            // 开启触发状态
            isTrigger = true;
            // 禁锢玩家
            player.GetComponent<PlayerController>().isConfinement = true;
            // 相机震动
            cameraShake.TriggerShake();
            // 更改关卡管理器中的目标关卡
            currentLevelsManager.targetLevelNumber = currentLevel.levelNumber + 1;
        }

        yield return null;

    }
}
