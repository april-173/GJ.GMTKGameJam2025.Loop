using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{

    public GameObject about;

    // 过渡实体
    private GameObject transition;

    private void Start()
    {
        // 寻找并获取transition
        transition = GameObject.FindGameObjectWithTag("Transition");
    }

    public void StartGame()
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
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// 关于游戏
    /// </summary>
    public void AboutGame()
    {
        about.SetActive(!about.activeSelf);
    }

    /// <summary>
    /// 基本退出方法
    /// </summary>
    public void ExitGame()
    {
        // 在独立平台（Windows、Mac、Linux）上退出应用程序
        Application.Quit();

        // 在Unity编辑器中停止播放模式
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
