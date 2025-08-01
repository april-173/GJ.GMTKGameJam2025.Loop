using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
    [Header("地图管理")]
    [Tooltip("关卡地图集合")]
    [SerializeField] private List<GameObject> maps = new List<GameObject>();
    [Space]
    [Tooltip("关卡地图缩放比例")]
    [SerializeField] private float mapsScaling;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++) 
        {
            // 遍历并获取所有子物体
            maps.Add(transform.GetChild(i).gameObject);
            // 遍历并并管理所有子物体碰撞器
            ColliderManager(maps[i]);
        }
    }

    /// <summary>
    /// 碰撞器管理
    /// </summary>
    /// <param name="map"></param>
    private void ColliderManager(GameObject map)
    {
        // 若地图尺寸为1则开启碰撞器
        if (map.transform.localScale == Vector3.one)
        {
            for (int i = 0; i < map.transform.childCount; i++)
            {
                map.transform.GetChild(i).gameObject.GetComponent<Collider2D>().enabled = true;
            }
            return;
        }
        // 若地图尺寸不为1则关闭碰撞器
        else
        {
            for (int i = 0; i < map.transform.childCount; i++)
            {
                map.transform.GetChild(i).gameObject.GetComponent<Collider2D>().enabled = false;
            }
        }

    }
}
