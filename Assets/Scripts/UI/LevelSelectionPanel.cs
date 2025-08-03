using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionPanel : MonoBehaviour
{
    public LevelsManager levelsManager;

    [Header("��ťͼ��")]
    [Tooltip("����ͼ��")]
    [SerializeField] private Sprite turnOnImage;
    [Tooltip("�ر�ͼ��")]
    [SerializeField] private Sprite turnOffImage;

    [Header("�ؿ���ťͼ��")]
    [SerializeField] private Image[] levelButtonImage;

    private void Update()
    {
        for(int i = 0; i < levelButtonImage.Length; i++)
        {
            levelButtonImage[i].sprite = turnOffImage;

            if( i == levelsManager.targetLevelNumber)
            {
                levelButtonImage[i].sprite = turnOnImage;
            }
        }
    }
}
