using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [Header("玩家输入")]
    [Tooltip("玩家输入方向")]
    public Vector2 moveInput;
    [Tooltip("X输入计时器")]
    [SerializeField] private float moveInputXTimer;
    [Tooltip("Y输入计时器")]
    [SerializeField] private float moveInputYTimer;

    [Header("地块检测")]
    [Tooltip("检测点大小")]
    public Vector2 checkPointSize;
    [Tooltip("检测图层")]
    public LayerMask checkLayer;
    [Tooltip("上侧检测点")]
    public Transform upCheckPoint;
    [Tooltip("下侧检测点")]
    public Transform downCheckPoint;
    [Tooltip("左侧检测点")]
    public Transform leftCheckPoint;
    [Tooltip("右侧检测点")]
    public Transform rightCheckPoint;

    [Header("移动权限")]
    [Tooltip("是否允许移动")]
    [SerializeField] private bool canMove;
    [Space]
    [Tooltip("是否允许向上移动")]
    [SerializeField] private bool canUpMove;
    [Tooltip("是否允许向下移动")]
    [SerializeField] private bool canDownMove;
    [Tooltip("是否允许向左移动")]
    [SerializeField] private bool canLeftMove;
    [Tooltip("是否允许向右移动")]
    [SerializeField] private bool canRightMove;
    [Space]
    [Tooltip("是否为禁锢状态")]
    public bool isConfinement;

    [Header("障碍检测")]
    [Tooltip("障碍检测图层")]
    [SerializeField] private LayerMask obstacleCheckLayer;

    [Header("移动相关")]
    [Tooltip("移动耗时")]
    [SerializeField] private float moveDuration;
    [Space]
    [Tooltip("移动方向")]
    [SerializeField] private Vector2 moveDirection;



    private void Update()
    {
        Timer();
        GetInput();
        PlotCheckSystem();
        ObstacleCheckSystem();
        MoveDirection();
        MoveController();
    }

    #region < 计时器 >

    /// <summary>
    /// 计时器
    /// </summary>
    private void Timer()
    {
        // X轴输入计时器：持续记录按住X轴方向键的时间，当松开X轴方向键归零
        if (moveInput.x != 0)
        {
            moveInputXTimer += Time.deltaTime;
        }
        else
        {
            moveInputXTimer = 0;
        }

        // Y轴输入计时器：持续记录按住Y轴方向键的时间，当松开Y轴方向键归零
        if (moveInput.y != 0)
        {
            moveInputYTimer += Time.deltaTime;
        }
        else
        {
            moveInputYTimer = 0;
        }
    }

    #endregion

    #region < 玩家输入获取 >
    /// <summary>
    ///获取输入获取
    /// </summary>
    private void GetInput()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");    // 获取玩家水平移动输入
        moveInput.y = Input.GetAxisRaw("Vertical");      // 获取玩家垂直移动输入

    }
    #endregion

    #region < 地块检测系统 >

    /// <summary>
    /// 地块检测系统
    /// </summary>
    private void PlotCheckSystem()
    {
        // 获取玩家四个方位接触的碰撞器组件
        Collider2D upTouchCol = PlotColliderAcquisition(upCheckPoint);
        Collider2D downTouchCol = PlotColliderAcquisition(downCheckPoint);
        Collider2D leftTouchCol = PlotColliderAcquisition(leftCheckPoint);
        Collider2D rightTouchCol = PlotColliderAcquisition(rightCheckPoint);
        // 根据是否获取到可移动地块的碰撞器判断是否可以往对应方向移动
        if (upTouchCol != null) { canUpMove = true; } else { canUpMove = false; }
        if (downTouchCol != null) { canDownMove = true; } else { canDownMove = false; }
        if (leftTouchCol != null) { canLeftMove = true; } else { canLeftMove = false; }
        if (rightTouchCol != null) { canRightMove = true; } else { canRightMove = false; }

    }

    /// <summary>
    /// 地块碰撞器获取
    /// </summary>
    /// <param name="checkPoint">检测点位置</param>
    /// <returns>检测点接触的碰撞器</returns>
    private Collider2D PlotColliderAcquisition(Transform checkPoint)
    {
        // 获取以 checkPoint.position 为中心， checkPointSize 为范围， 0 为旋转角度，只作用于 checkLayer 图层的接触到的碰撞器组件
        Collider2D TouchCol = Physics2D.OverlapBox(
            checkPoint.position,
            checkPointSize,
            0,
            checkLayer
            );

        return TouchCol;
    }

    #endregion

    #region < 移动权限管理 >

    public void SwitchCanMove(bool b)
    {
        if (isConfinement)
        {
            canMove = false;
            return;
        }
        else
        {
            canMove = b;
        }
    }

    #endregion

    #region < 障碍检测系统 >

    /// <summary>
    /// 障碍检测系统
    /// </summary>
    private void ObstacleCheckSystem()
    {
        Collider2D upObstacleCol = ObstacleColliderAcquisition(upCheckPoint);
        Collider2D downObstacleCol = ObstacleColliderAcquisition (downCheckPoint);
        Collider2D leftObstacleCol = ObstacleColliderAcquisition(leftCheckPoint);
        Collider2D rightObstacleCol = ObstacleColliderAcquisition(rightCheckPoint);

        if (upObstacleCol != null)
        {
            canUpMove = false;
        }
        if (downObstacleCol != null)
        {
            canDownMove = false;
        }
        if(leftObstacleCol != null)
        {
            canLeftMove = false;
        }
        if(rightObstacleCol != null)
        {
            canRightMove = false;
        }
    }

    /// <summary>
    /// 障碍碰撞器获取
    /// </summary>
    /// <param name="checkPoint">检测点位置</param>
    /// <returns>检测点接触的碰撞器</returns>
    private Collider2D ObstacleColliderAcquisition(Transform checkPoint)
    {
        // 获取以 checkPoint.position 为中心， checkPointSize 为范围， 0 为旋转角度，只作用于 obstacleCheckLayer 图层的接触到的碰撞器组件
        Collider2D TouchCol = Physics2D.OverlapBox(
            checkPoint.position,
            checkPointSize,
            0,
            obstacleCheckLayer
            );

        return TouchCol;
    }

    #endregion

    #region < 移动控制系统 >

    /// <summary>
    /// 移动控制系统
    /// </summary>
    private void MoveController()
    {
        if(!canMove)
        {
            return;
        }

        if (moveDirection != Vector2.zero) 
        {
            StartCoroutine(Move());
        }
    }

        /// <summary>
        /// 移动方向获取
        /// </summary>
        private void MoveDirection()
    {
        moveDirection = moveInput;

        // 根据具体方向的移动权限修正实际移动方向
        if (!canUpMove && moveDirection.y > 0) 
        {
            moveDirection.y = 0;
        }
        if(!canDownMove && moveDirection.y < 0)
        {
            moveDirection.y = 0;
        }
        if (!canLeftMove && moveDirection.x < 0)
        {
            moveDirection.x = 0;
        }
        if(!canRightMove && moveDirection.x > 0)
        {
            moveDirection.x = 0;
        }

        // 玩家持续对某方向进行移动时允许玩家朝其它方向进行调整
        // 玩家实际移动方向始终偏向于持续输入时间少的那个方向
        // 玩家无法斜向移动
        if (moveInputXTimer <= moveInputYTimer)
        {
            if (moveDirection.x != 0 && moveDirection.y != 0)
            {
                moveDirection = new Vector2(moveDirection.x, 0);
            }
        }
        else
        {
            if (moveDirection.x != 0 && moveDirection.y != 0)
            {
                moveDirection = new Vector2(0, moveDirection.y);
            }
        }
    }

    /// <summary>
    /// 平滑移动协程核心
    /// </summary>
    /// <param name="targetPosition">目标位置</param>
    /// <returns></returns>
    private IEnumerator Move()
    {
        // 锁定移动状态
        SwitchCanMove(false);

        // 记录起始位置
        Vector3 startPosition = transform.position;
        // 根据移动方向确定目标位置
        Vector3 targetPosition = startPosition + new Vector3(moveDirection.x, moveDirection.y, 0).normalized;

        // 移动实际用时
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            // 计算插值进度（使用平滑的缓动函数）
            float t = EaseOutQuad(elapsedTime / moveDuration);
            //float t = elapsedTime / moveDuration;
            // 更新位置
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            // 计时
            elapsedTime += Time.deltaTime;
            // 每帧执行
            yield return null; 
        }

        // 确保精确到达目标位置
        transform.position = targetPosition;

        transform.position = new Vector3(Mathf.RoundToInt(transform.position.x),Mathf.RoundToInt(transform.position.y),Mathf.RoundToInt(transform.position.z));

        // 等待
        yield return null;

        // 解除移动锁定
        SwitchCanMove(true);
    }

    // 缓动函数（先快后慢）
    private float EaseOutQuad(float x)
    {
        return 1 - (1 - x) * (1 - x);
    }

    #endregion

}
