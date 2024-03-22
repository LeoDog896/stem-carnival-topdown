using UnityEngine;
using UnityEngine.Events; // Required for UnityEvents
using UnityEngine.UI;
using System.Collections;

public class Tutorial : MonoBehaviour
{
    public CharacterController characterController;
    public Text tutorialText;
    public GameObject Panel;
    public UnityEvent onEnemySpawn; // Event to be invoked when the enemy should spawn
    public EnemySpawns spawners; // Reference to the Spawner script


    void Start()
    {
        Panel.SetActive(true);
        tutorialText.enabled = true;
        // Disable player movement when the tutorial starts
        if (characterController != null)
        {
            characterController.EnableMovement(false);
        }
        else
        {
            Debug.LogError("CharacterController reference not set in Tutorial script!");
        }

        // Set initial text
        if (tutorialText != null)
        {
            // Start the tutorial with the first message after 1 second
            StartCoroutine(ShowTutorialMessage("Hey there. Welcome to KINGPIN!", 3f));
        }
        else
        {
            Debug.LogError("Text component reference not set in Tutorial script!");
        }
    }

    IEnumerator ShowTutorialMessage(string message, float duration)
    {
        tutorialText.text = message;

        yield return new WaitForSeconds(duration);

        if (message == "Hey there. Welcome to KINGPIN!")
        {
            // Enable player movement after 1 second
            if (characterController != null)
            {
                characterController.EnableMovement(true);
                StartCoroutine(ShowTutorialMessage("Move Around With The Joystick!!", 2f));
            }
        }
        if (message == "Move Around With The Joystick!!")
        {
            StartCoroutine(ShowTutorialMessage("Press E to Attack, and Press F to Kick!", 2f));
            yield return new WaitForSeconds(duration);
            StartCoroutine(ShowTutorialMessage("Enemies will spawn outside of the Camera, so look out!", 2f));
            yield return new WaitForSeconds(duration);
            StartCoroutine(ShowTutorialMessage("Lastly, you get score from kills, and you have a health bar! The rest will be obvious. Have Fun!", 3f));
            yield return new WaitForSeconds(duration);
            Panel.SetActive(false);
            tutorialText.enabled = false;
            spawners.SpawnPrefabs(false);

        }
    }

}