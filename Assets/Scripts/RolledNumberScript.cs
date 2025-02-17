using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RolledNumberScript : MonoBehaviour
{
    DiceRollScript diceRollScript;
    [SerializeField] TMP_Text rolledNumberText;
    private bool hasMoved = false;

    void Awake()
    {
        diceRollScript = FindObjectOfType<DiceRollScript>();
    }

    void Update()
    {
        if (diceRollScript == null)
        {
            Debug.LogError("DiceRollScript not found in the scene");
            return;
        }

        if (diceRollScript.isLanded && !hasMoved)
        {
            rolledNumberText.text = diceRollScript.diceFaceNum;

            int rolledNumber = int.Parse(diceRollScript.diceFaceNum);
            FindObjectOfType<PlayerScript>().MovePlayer(rolledNumber);

            hasMoved = true;
        }
        else if (!diceRollScript.isLanded)
        {
            rolledNumberText.text = "?";
            hasMoved = false;
        }
    }
}
