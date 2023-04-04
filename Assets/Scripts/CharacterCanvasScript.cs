using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterCanvasScript : MonoBehaviour
{
    [SerializeField] private Image hpFill;
    [SerializeField] private TextMeshProUGUI characterName;

    public void SetHp(float hp)
    {
        hpFill.fillAmount = hp;
    }

    public void SetCharacterName(string Name)
    {
        characterName.text = Name;
    }
}