using UnityEngine;
using UnityEngine.UI;

public class CharacterSystem : MonoBehaviour
{
    public int currentColorIndex = 0;
    public Material[] playerColors;
    public Image currentColorImage;

    public Text currentColorText;

    private void Start()
    {
        currentColorIndex = PlayerPrefs.GetInt("currentColorIndex", 0);
        currentColorImage.color = playerColors[currentColorIndex].color;
        currentColorText.text = playerColors[currentColorIndex].name;
    }

    public void NextColor()
    {
        if(currentColorIndex < playerColors.Length - 1)
        {
            currentColorIndex++;
            PlayerPrefs.SetInt("currentColorIndex", currentColorIndex);
            currentColorImage.color = playerColors[currentColorIndex].color;
            currentColorText.text = playerColors[currentColorIndex].name;
        }
    }
    public void PreviousColor()
    {

        if (currentColorIndex > 0)
        {
            currentColorIndex--;
            PlayerPrefs.SetInt("currentColorIndex", currentColorIndex);
            currentColorImage.color = playerColors[currentColorIndex].color;
            currentColorText.text = playerColors[currentColorIndex].name;
        }
    }
}
