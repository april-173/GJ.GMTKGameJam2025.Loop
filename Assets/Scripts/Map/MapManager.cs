using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
    [Header("��ͼ����")]
    [Tooltip("�ؿ���ͼ����")]
    [SerializeField] private List<GameObject> maps = new List<GameObject>();
    [Space]
    [Tooltip("�ؿ���ͼ���ű���")]
    [SerializeField] private float mapsScaling;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++) 
        {
            // ��������ȡ����������
            maps.Add(transform.GetChild(i).gameObject);
            // ������������������������ײ��
            ColliderManager(maps[i]);
        }
    }

    /// <summary>
    /// ��ײ������
    /// </summary>
    /// <param name="map"></param>
    private void ColliderManager(GameObject map)
    {
        // ����ͼ�ߴ�Ϊ1������ײ��
        if (map.transform.localScale == Vector3.one)
        {
            for (int i = 0; i < map.transform.childCount; i++)
            {
                map.transform.GetChild(i).gameObject.GetComponent<Collider2D>().enabled = true;
            }
            return;
        }
        // ����ͼ�ߴ粻Ϊ1��ر���ײ��
        else
        {
            for (int i = 0; i < map.transform.childCount; i++)
            {
                map.transform.GetChild(i).gameObject.GetComponent<Collider2D>().enabled = false;
            }
        }

    }
}
