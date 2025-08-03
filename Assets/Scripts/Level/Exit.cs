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
            // �ڶ���ƽ̨��Windows��Mac��Linux�����˳�Ӧ�ó���
            Application.Quit();

            // ��Unity�༭����ֹͣ����ģʽ
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
