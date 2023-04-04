using Photon.Pun;
using TMPro;
using UnityEngine;

public class PopUpDisplayer : MonoBehaviour
{
    [SerializeField] private GameObject popUpCanvas;

    [SerializeField] private TextMeshProUGUI winOrLoseText;
    [SerializeField] private TextMeshProUGUI nickNameText;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Awake()
    {
        Character.CharacterDiedEvent.AddListener(CheckDiedCharacter);
    }

    private void OpenPopUp(bool won, Character character)
    {
        popUpCanvas.SetActive(true);
        winOrLoseText.text = won ? "You won" : "You lose";
        nickNameText.text = PhotonNetwork.LocalPlayer.NickName;
        scoreText.text = character.points.ToString();
    }

    private void CheckDiedCharacter(Character character)
    {
        Debug.Log("Someone Died");
        if (character.isMine()) { OpenPopUp(false, character); return; }
        var characters = FindObjectsOfType<Character>();
        if (characters.Length - 1 == 1)
        {
            foreach(var chara in characters) if (chara.isMine()) OpenPopUp(true, chara);
        }
    }
}