using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [Header("��������")]
    [Tooltip("����ң���Ҫ�����Ŀ�꣩")]
    [SerializeField] private GameObject mainPlayer;
    [Tooltip("���������ҵı���")]
    [SerializeField] private float followScale;

    private void Update()
    {
        transform.position = mainPlayer.transform.position * followScale;
        transform.rotation = mainPlayer.transform.rotation;
        transform.localScale = mainPlayer.transform.localScale * followScale;
    }
}
