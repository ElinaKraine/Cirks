using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    public GameObject[] steps;
    int characterIndex;
    public GameObject spawnPoint;
    int[] otherPlayers;
    int index;
    private GameObject mainCharacter;
    private int currentStep = -1;
    private bool isMoving = false;

    private const string textFileName = "playerNames";

    void Start()
    {
        characterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        mainCharacter = Instantiate(playerPrefabs[characterIndex],
            spawnPoint.transform.position, Quaternion.identity);
        mainCharacter.GetComponent<NameScript>().SetPlayerName(
            PlayerPrefs.GetString("PlayerName"));

        otherPlayers = new int[PlayerPrefs.GetInt("PlayerCount")];
        string[] nameArray = ReadLinesFromFile(textFileName);
        Debug.Log(otherPlayers.Length + " " + nameArray.Length);

        for (int i = 0; i < otherPlayers.Length; i++)
        {
            spawnPoint.transform.position += new Vector3(0.2f, 0, 0.08f);
            index = Random.Range(0, playerPrefabs.Length);
            GameObject character = Instantiate(playerPrefabs[index],
                spawnPoint.transform.position, Quaternion.identity);
            character.GetComponent<NameScript>().SetPlayerName(
                nameArray[Random.Range(0, nameArray.Length)]);
        }
    }

    public void MovePlayer(int stepsToMove)
    {
        if (!isMoving)
        {
            StopAllCoroutines();
            StartCoroutine(MoveToNextPosition(stepsToMove));
        }
    }

    IEnumerator MoveToNextPosition(int stepsToMove)
    {
        isMoving = true;

        int targetStep = Mathf.Min(currentStep + stepsToMove, steps.Length - 1);
        int stepsBack = stepsToMove - (steps.Length - (currentStep + 1));

        while (currentStep < targetStep)
        {
            currentStep++;
            Vector3 targetPosition = steps[currentStep].transform.position;

            // Добавляем смещение по оси Y, чтобы персонаж был над платформой
            targetPosition.y += 0.2f; // Используйте значение, которое вам нужно для высоты

            float elapsedTime = 0;
            float moveTime = 0.5f;
            Vector3 startPosition = mainCharacter.transform.position;

            while (elapsedTime < moveTime)
            {
                mainCharacter.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            mainCharacter.transform.position = targetPosition;
            yield return new WaitForSeconds(0.1f);
        }

        if (stepsBack > 0)
        {
            while (stepsBack > 0)
            {
                currentStep--;
                stepsBack--;

                Vector3 targetPosition = steps[currentStep].transform.position;

                // Добавляем смещение по оси Y, чтобы персонаж был над платформой
                targetPosition.y += 0.2f; // Используйте значение, которое вам нужно для высоты

                float elapsedTime = 0;
                float moveTime = 0.5f;
                Vector3 startPosition = mainCharacter.transform.position;

                while (elapsedTime < moveTime)
                {
                    mainCharacter.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveTime);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                mainCharacter.transform.position = targetPosition;
                yield return new WaitForSeconds(0.1f);
            }
        }

        isMoving = false;
    }


    string[] ReadLinesFromFile(string fName)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(fName);
        if (textAsset != null)
        {
            return textAsset.text.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        }
        else
        {
            Debug.LogError("File not found " + fName);
            return new string[0];
        }
    }
}
