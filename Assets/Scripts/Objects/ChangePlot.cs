using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public enum Direction
{
    None,
    Up,
    Down,
    Left,
    Right,
}

public class ChangePlot : MonoBehaviour
{
    // ��ҿ������ű�
    private PlayerController playerController;

    [Tooltip("�ƶ���ʱ��")]
    [SerializeField] private float changeDuration;

    [Header("��������")]
    [Tooltip("��С�ؿ�")]
    [SerializeField] private GameObject reducePlot;
    [Tooltip("��С�ؿ鷽��")]
    [SerializeField] private Direction reducePlotDirection;
    [Tooltip("�Ƿ�����С�ؿ���нӴ�")]
    [SerializeField] private bool isTriggerReducePlot;
    [Tooltip("��С�ؿ���С����")]
    [SerializeField] private float reducePlotReduceScale;
    [Space]
    [Tooltip("�Ŵ�ؿ�")]
    [SerializeField] private GameObject enlargePlot;
    [Tooltip("�Ŵ�ؿ鷽��")]
    [SerializeField] private Direction enlargePlotDirection;
    [Tooltip("�Ƿ���Ŵ�ؿ���нӴ�")]
    [SerializeField] private bool isTriggerEnlargePlot;
    [Tooltip("�Ŵ�ؿ�Ŵ����")]
    [SerializeField] private float enlargePlotEnlargeScale;

    // ��С�ؿ���������
    private Vector3 reducePlotDirectionVector;
    // �Ŵ�ؿ���������
    private Vector3 enlargePlotDirectionVector;

    // ��С�ؿ�Ŀ�ĵ�
    private Vector3 reducePlotTargetPoint;
    // �Ŵ�ؿ�Ŀ�ĵ�
    private Vector3 enlargePlotTargetPoint;

    private void OnEnable()
    {
        //��ȡ��ҿ������ű�
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Start()
    {
        PlotDirectionVectorAcquire();
    }

    private void Update()
    {
        TriggerInspect();
        TargetPointAcquire();
        ChangePlotSystem();
    }



    #region < �����Զ���ȡ >

    /// <summary>
    /// �ؿ鷽��������ȡ
    /// </summary>
    private void PlotDirectionVectorAcquire()
    {
        // ���ݵؿ鷽�����ͻ�ȡ��Ӧ������

        switch (reducePlotDirection)
        {
            case Direction.Up:
                reducePlotDirectionVector = Vector2.up;
                break;
            case Direction.Down:
                reducePlotDirectionVector = Vector2.down;
                break;
            case Direction.Left:
                reducePlotDirectionVector = Vector2.left;
                break;
            case Direction.Right:
                reducePlotDirectionVector = Vector2.right;
                break;
        }

        switch (enlargePlotDirection)
        {
            case Direction.Up:
                enlargePlotDirectionVector = Vector2.up;
                break;
            case Direction.Down:
                enlargePlotDirectionVector = Vector2.down;
                break;
            case Direction.Left:
                enlargePlotDirectionVector = Vector2.left;
                break;
            case Direction.Right:
                enlargePlotDirectionVector = Vector2.right;
                break;
        }
    }

    /// <summary>
    /// Ŀ�ĵػ�ȡ
    /// </summary>
    private void TargetPointAcquire()
    {
        // ����ؿ��Ŀ��λ��
        reducePlotTargetPoint = reducePlot.transform.position + (0.5f + 1.5f * reducePlotReduceScale) * reducePlotDirectionVector;

        enlargePlotTargetPoint = enlargePlot.transform.position + (1.5f + 0.5f * enlargePlotEnlargeScale) * enlargePlotDirectionVector;
    }

    #endregion

    #region < ��Ҵ������ >

    /// <summary>
    /// �������
    /// </summary>
    private void TriggerInspect()
    {
        // ��С�ؿ��Ƿ�����ҽӴ�
        if (playerController.transform.position == reducePlot.transform.position) 
        {
            isTriggerReducePlot = true;
        }
        else
        {
            isTriggerReducePlot = false;
        }

        // �Ŵ�ؿ��Ƿ�����ҽӴ�
        if (playerController.transform.position == enlargePlot.transform.position)
        {
            isTriggerEnlargePlot = true;
        }
        else
        {
            isTriggerEnlargePlot = false;
        }
    }

    #endregion

    #region < ���Ĺ���ϵͳ >

    /// <summary>
    /// �任�ؿ�ϵͳ
    /// </summary>
    private void ChangePlotSystem()
    {
        // ��ҽӴ���С�ؿ�
        if (isTriggerReducePlot && !playerController.isConfinement) 
        {
            // ��ҽ��еؿ鷽�������ʱִ�еؿ鹦��
            if (reducePlotDirectionVector.x != 0 && reducePlotDirectionVector.x == playerController.moveInput.x) 
            {
                StartCoroutine(Reduce());
            }
            else if (reducePlotDirectionVector.y != 0 && reducePlotDirectionVector.y == playerController.moveInput.y) 
            {
                StartCoroutine(Reduce());
            }
        }

        // ��ҽӴ��Ŵ�ؿ�
        if (isTriggerEnlargePlot && !playerController.isConfinement) 
        {
            // ��ҽ��еؿ鷽�������ʱִ�еؿ鹦��
            if (enlargePlotDirectionVector.x != 0 && enlargePlotDirectionVector.x == playerController.moveInput.x)
            {
                StartCoroutine(Enlarge());
            }
            else if (enlargePlotDirectionVector.y != 0 && enlargePlotDirectionVector.y == playerController.moveInput.y) 
            {
                StartCoroutine(Enlarge());
            }
        }
    }

    /// <summary>
    /// ��С
    /// </summary>
    /// <returns></returns>
    private IEnumerator Reduce()
    {
        // ������ҽ���
        playerController.isConfinement = true;

        // ʵ����ʱ
        float changeElapsed = 0f;

        while (changeElapsed < changeDuration)
        {
            float t = changeElapsed / changeDuration;

            // �������λ��
            playerController.transform.position = Vector3.Lerp(reducePlot.transform.position, reducePlotTargetPoint, t);
            // ������ҳߴ�
            playerController.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * reducePlotReduceScale, t);

            // ��ʱ������
            changeElapsed += Time.deltaTime;
            // ÿִ֡��
            yield return null;
        }
        // ȷ��λ�þ�ȷ
        playerController.transform.position = reducePlotTargetPoint;
        // ȷ���ߴ羫ȷ
        playerController.transform.localScale = Vector3.one * reducePlotReduceScale;

        // �ȴ�
        yield return null;

        // �任�������˲�����Ŵ�ؿ�λ��
        playerController.transform.position = enlargePlot.transform.position;
        // �任�Ӵ���ҳߴ�ָ�
        playerController.transform.localScale = Vector3.one;

        // �ȴ�
        yield return null;

        // �ر���ҽ���
        playerController.isConfinement = false;
        // ��������ƶ�
        playerController.SwitchCanMove(true);
    }

    /// <summary>
    /// �Ŵ�
    /// </summary>
    /// <returns></returns>
    private IEnumerator Enlarge()
    {
        // ������ҽ���
        playerController.isConfinement = true;

        // ʵ����ʱ
        float changeElapsed = 0f;

        while (changeElapsed < changeDuration)
        {
            float t = changeElapsed / changeDuration;

            // �������λ��
            playerController.transform.position = Vector3.Lerp(enlargePlot.transform.position, enlargePlotTargetPoint, t);
            // ������ҳߴ�
            playerController.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * enlargePlotEnlargeScale, t);

            // ��ʱ������
            changeElapsed += Time.deltaTime;
            // ÿִ֡��
            yield return null;
        }
        // ȷ��λ�þ�ȷ
        playerController.transform.position = enlargePlotTargetPoint;
        // ȷ���ߴ羫ȷ
        playerController.transform.localScale = Vector3.one * enlargePlotEnlargeScale;

        // �ȴ�
        yield return null;

        // �任�������˲������С�ؿ�λ��
        playerController.transform.position = reducePlot.transform.position;
        // �任�Ӵ���ҳߴ�ָ�
        playerController.transform.localScale = Vector3.one;

        // �ȴ�
        yield return null;

        // �ر���ҽ���
        playerController.isConfinement = false;
        // ��������ƶ�
        playerController.SwitchCanMove(true);
    }

    #endregion
}
