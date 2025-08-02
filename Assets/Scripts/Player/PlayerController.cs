using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [Header("�������")]
    [Tooltip("������뷽��")]
    public Vector2 moveInput;
    [Tooltip("X�����ʱ��")]
    [SerializeField] private float moveInputXTimer;
    [Tooltip("Y�����ʱ��")]
    [SerializeField] private float moveInputYTimer;

    [Header("�ؿ���")]
    [Tooltip("�����С")]
    public Vector2 checkPointSize;
    [Tooltip("���ͼ��")]
    public LayerMask checkLayer;
    [Tooltip("�ϲ����")]
    public Transform upCheckPoint;
    [Tooltip("�²����")]
    public Transform downCheckPoint;
    [Tooltip("������")]
    public Transform leftCheckPoint;
    [Tooltip("�Ҳ����")]
    public Transform rightCheckPoint;

    [Header("�ƶ�Ȩ��")]
    [Tooltip("�Ƿ������ƶ�")]
    [SerializeField] private bool canMove;
    [Space]
    [Tooltip("�Ƿ����������ƶ�")]
    [SerializeField] private bool canUpMove;
    [Tooltip("�Ƿ����������ƶ�")]
    [SerializeField] private bool canDownMove;
    [Tooltip("�Ƿ����������ƶ�")]
    [SerializeField] private bool canLeftMove;
    [Tooltip("�Ƿ����������ƶ�")]
    [SerializeField] private bool canRightMove;
    [Space]
    [Tooltip("�Ƿ�Ϊ����״̬")]
    public bool isConfinement;

    [Header("�ϰ����")]
    [Tooltip("�ϰ����ͼ��")]
    [SerializeField] private LayerMask obstacleCheckLayer;

    [Header("�ƶ����")]
    [Tooltip("�ƶ���ʱ")]
    [SerializeField] private float moveDuration;
    [Space]
    [Tooltip("�ƶ�����")]
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

    #region < ��ʱ�� >

    /// <summary>
    /// ��ʱ��
    /// </summary>
    private void Timer()
    {
        // X�������ʱ����������¼��סX�᷽�����ʱ�䣬���ɿ�X�᷽�������
        if (moveInput.x != 0)
        {
            moveInputXTimer += Time.deltaTime;
        }
        else
        {
            moveInputXTimer = 0;
        }

        // Y�������ʱ����������¼��סY�᷽�����ʱ�䣬���ɿ�Y�᷽�������
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

    #region < ��������ȡ >
    /// <summary>
    ///��ȡ�����ȡ
    /// </summary>
    private void GetInput()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");    // ��ȡ���ˮƽ�ƶ�����
        moveInput.y = Input.GetAxisRaw("Vertical");      // ��ȡ��Ҵ�ֱ�ƶ�����

    }
    #endregion

    #region < �ؿ���ϵͳ >

    /// <summary>
    /// �ؿ���ϵͳ
    /// </summary>
    private void PlotCheckSystem()
    {
        // ��ȡ����ĸ���λ�Ӵ�����ײ�����
        Collider2D upTouchCol = PlotColliderAcquisition(upCheckPoint);
        Collider2D downTouchCol = PlotColliderAcquisition(downCheckPoint);
        Collider2D leftTouchCol = PlotColliderAcquisition(leftCheckPoint);
        Collider2D rightTouchCol = PlotColliderAcquisition(rightCheckPoint);
        // �����Ƿ��ȡ�����ƶ��ؿ����ײ���ж��Ƿ��������Ӧ�����ƶ�
        if (upTouchCol != null) { canUpMove = true; } else { canUpMove = false; }
        if (downTouchCol != null) { canDownMove = true; } else { canDownMove = false; }
        if (leftTouchCol != null) { canLeftMove = true; } else { canLeftMove = false; }
        if (rightTouchCol != null) { canRightMove = true; } else { canRightMove = false; }

    }

    /// <summary>
    /// �ؿ���ײ����ȡ
    /// </summary>
    /// <param name="checkPoint">����λ��</param>
    /// <returns>����Ӵ�����ײ��</returns>
    private Collider2D PlotColliderAcquisition(Transform checkPoint)
    {
        // ��ȡ�� checkPoint.position Ϊ���ģ� checkPointSize Ϊ��Χ�� 0 Ϊ��ת�Ƕȣ�ֻ������ checkLayer ͼ��ĽӴ�������ײ�����
        Collider2D TouchCol = Physics2D.OverlapBox(
            checkPoint.position,
            checkPointSize,
            0,
            checkLayer
            );

        return TouchCol;
    }

    #endregion

    #region < �ƶ�Ȩ�޹��� >

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

    #region < �ϰ����ϵͳ >

    /// <summary>
    /// �ϰ����ϵͳ
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
    /// �ϰ���ײ����ȡ
    /// </summary>
    /// <param name="checkPoint">����λ��</param>
    /// <returns>����Ӵ�����ײ��</returns>
    private Collider2D ObstacleColliderAcquisition(Transform checkPoint)
    {
        // ��ȡ�� checkPoint.position Ϊ���ģ� checkPointSize Ϊ��Χ�� 0 Ϊ��ת�Ƕȣ�ֻ������ obstacleCheckLayer ͼ��ĽӴ�������ײ�����
        Collider2D TouchCol = Physics2D.OverlapBox(
            checkPoint.position,
            checkPointSize,
            0,
            obstacleCheckLayer
            );

        return TouchCol;
    }

    #endregion

    #region < �ƶ�����ϵͳ >

    /// <summary>
    /// �ƶ�����ϵͳ
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
        /// �ƶ������ȡ
        /// </summary>
        private void MoveDirection()
    {
        moveDirection = moveInput;

        // ���ݾ��巽����ƶ�Ȩ������ʵ���ƶ�����
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

        // ��ҳ�����ĳ��������ƶ�ʱ������ҳ�����������е���
        // ���ʵ���ƶ�����ʼ��ƫ���ڳ�������ʱ���ٵ��Ǹ�����
        // ����޷�б���ƶ�
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
    /// ƽ���ƶ�Э�̺���
    /// </summary>
    /// <param name="targetPosition">Ŀ��λ��</param>
    /// <returns></returns>
    private IEnumerator Move()
    {
        // �����ƶ�״̬
        SwitchCanMove(false);

        // ��¼��ʼλ��
        Vector3 startPosition = transform.position;
        // �����ƶ�����ȷ��Ŀ��λ��
        Vector3 targetPosition = startPosition + new Vector3(moveDirection.x, moveDirection.y, 0).normalized;

        // �ƶ�ʵ����ʱ
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            // �����ֵ���ȣ�ʹ��ƽ���Ļ���������
            float t = EaseOutQuad(elapsedTime / moveDuration);
            //float t = elapsedTime / moveDuration;
            // ����λ��
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            // ��ʱ
            elapsedTime += Time.deltaTime;
            // ÿִ֡��
            yield return null; 
        }

        // ȷ����ȷ����Ŀ��λ��
        transform.position = targetPosition;

        transform.position = new Vector3(Mathf.RoundToInt(transform.position.x),Mathf.RoundToInt(transform.position.y),Mathf.RoundToInt(transform.position.z));

        // �ȴ�
        yield return null;

        // ����ƶ�����
        SwitchCanMove(true);
    }

    // �����������ȿ������
    private float EaseOutQuad(float x)
    {
        return 1 - (1 - x) * (1 - x);
    }

    #endregion

}
