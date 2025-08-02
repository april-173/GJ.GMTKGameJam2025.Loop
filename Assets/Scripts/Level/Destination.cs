using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Destination : MonoBehaviour
{
    // ���ʵ��
    private GameObject player;
    // ������𶯽ű�
    private CameraShake cameraShake;
    // ���ɽű�
    private Transition transition;

    // �Ƿ��ڴ���״̬
    private bool isTrigger;

    // ��ǰLevel���
    private Level currentLevel;
    // ��ǰLevelManager���
    private LevelsManager currentLevelsManager;

    private void Start()
    {
        // ��ȡ������𶯽ű�
        cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        // ��ȡ���ɽű�
        transition = GameObject.FindGameObjectWithTag("Transition").GetComponent<Transition>();

        // ��ȡLevel���
        currentLevel = transform.parent.parent.GetComponent<Level>();
        // ��ȡLevelsManager���
        currentLevelsManager = transform.parent.parent.parent.GetComponent<LevelsManager>();
    }

    private void OnEnable()
    {
        // ��ȡ���ʵ��
        player = GameObject.FindGameObjectWithTag("Player");
        // �رմ���״̬
        isTrigger = false;
    }

    private void Update()
    {
        // ���λ�����յ�λ��һ��ʱ�����л��ؿ�Э��
        if(player.transform.position == transform.position)
        {
            StartCoroutine(LevelSwitch());
        }
    }


    /// <summary>
    /// �ؿ��л�Э��
    /// </summary>
    /// <returns></returns>
    private IEnumerator LevelSwitch()
    {
        // ��δ���ڴ���״̬
        if(!isTrigger)
        {
            // ��������״̬
            isTrigger = true;
            // �������
            player.GetComponent<PlayerController>().isConfinement = true;
            // �����
            cameraShake.TriggerShake();
            // ���Ĺؿ��������е�Ŀ��ؿ�
            currentLevelsManager.targetLevelNumber = currentLevel.levelNumber + 1;
        }

        yield return null;

    }
}
