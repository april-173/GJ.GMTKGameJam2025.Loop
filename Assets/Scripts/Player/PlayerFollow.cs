using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [Header("跟随设置")]
    [Tooltip("主玩家（需要跟随的目标）")]
    [SerializeField] private GameObject mainPlayer;
    [Tooltip("相较于主玩家的比例")]
    [SerializeField] private float followScale;

    private void Update()
    {
        transform.position = mainPlayer.transform.position * followScale;
        transform.rotation = mainPlayer.transform.rotation;
        transform.localScale = mainPlayer.transform.localScale * followScale;
    }
}
