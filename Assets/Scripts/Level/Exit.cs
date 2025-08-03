using UnityEngine;

public class Exit : MonoBehaviour
{
    private PlayerController playerController;

    private void OnEnable()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (playerController.transform.position == transform.position) 
        {
            // 在独立平台（Windows、Mac、Linux）上退出应用程序
            Application.Quit();

            // 在Unity编辑器中停止播放模式
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
