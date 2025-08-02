using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [Header("跟随设置")]
    [Tooltip("主玩家（需要跟随的目标）")]
    [SerializeField] private Transform mainPlayer;
    [Tooltip("相较于主玩家的比例")]
    [SerializeField] private float followScale;

    // 存储上一帧的位置和旋转
    private Vector3 lastPlayerPosition;
    private Quaternion lastPlayerRotation;

    void Start()
    {
        if (mainPlayer == null) return;

        // 初始化记录值
        lastPlayerPosition = mainPlayer.position;
        lastPlayerRotation = mainPlayer.rotation;

        //// 设置初始位置与缩放
        //transform.position = mainPlayer.position * followScale; 
        //transform.localScale = mainPlayer.localScale * followScale;
    }

    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (mainPlayer == null) return;

        // 1. 更新缩放
        transform.localScale = mainPlayer.localScale * followScale;

        // 2. 计算玩家位移和旋转变化
        Vector3 playerDeltaPosition = mainPlayer.position - lastPlayerPosition;
        Quaternion playerDeltaRotation = mainPlayer.rotation * Quaternion.Inverse(lastPlayerRotation);

        // 3. 将玩家位移转换到局部空间
        Vector3 localMovement = Quaternion.Inverse(lastPlayerRotation) * playerDeltaPosition;

        // 4. 应用比例缩放
        Vector3 scaledLocalMovement = localMovement * followScale;

        // 5. 转换到跟随物体的世界空间方向
        Vector3 worldMovement = transform.rotation * scaledLocalMovement;

        // 6. 应用移动
        transform.position += worldMovement;

        // 7. 更新记录值
        lastPlayerPosition = mainPlayer.position;
        lastPlayerRotation = mainPlayer.rotation;
    }
}
