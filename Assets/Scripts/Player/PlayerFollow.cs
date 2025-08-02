using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [Header("��������")]
    [Tooltip("����ң���Ҫ�����Ŀ�꣩")]
    [SerializeField] private Transform mainPlayer;
    [Tooltip("���������ҵı���")]
    [SerializeField] private float followScale;

    // �洢��һ֡��λ�ú���ת
    private Vector3 lastPlayerPosition;
    private Quaternion lastPlayerRotation;

    void Start()
    {
        if (mainPlayer == null) return;

        // ��ʼ����¼ֵ
        lastPlayerPosition = mainPlayer.position;
        lastPlayerRotation = mainPlayer.rotation;

        //// ���ó�ʼλ��������
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

        // 1. ��������
        transform.localScale = mainPlayer.localScale * followScale;

        // 2. �������λ�ƺ���ת�仯
        Vector3 playerDeltaPosition = mainPlayer.position - lastPlayerPosition;
        Quaternion playerDeltaRotation = mainPlayer.rotation * Quaternion.Inverse(lastPlayerRotation);

        // 3. �����λ��ת�����ֲ��ռ�
        Vector3 localMovement = Quaternion.Inverse(lastPlayerRotation) * playerDeltaPosition;

        // 4. Ӧ�ñ�������
        Vector3 scaledLocalMovement = localMovement * followScale;

        // 5. ת�����������������ռ䷽��
        Vector3 worldMovement = transform.rotation * scaledLocalMovement;

        // 6. Ӧ���ƶ�
        transform.position += worldMovement;

        // 7. ���¼�¼ֵ
        lastPlayerPosition = mainPlayer.position;
        lastPlayerRotation = mainPlayer.rotation;
    }
}
