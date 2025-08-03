using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionPanel : MonoBehaviour
{
    public LevelsManager levelsManager;

    [Header("°´Å¥Í¼Ïñ")]
    [Tooltip("¿ªÆôÍ¼Ïñ")]
    [SerializeField] private Sprite turnOnImage;
    [Tooltip("¹Ø±ÕÍ¼Ïñ")]
    [SerializeField] private Sprite turnOffImage;

    [Header("¹Ø¿¨°´Å¥Í¼Ïñ")]
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
