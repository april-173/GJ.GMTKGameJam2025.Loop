using UnityEngine;

public class Origin : MonoBehaviour
{
    private void OnEnable()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = transform.position;
    }
}
