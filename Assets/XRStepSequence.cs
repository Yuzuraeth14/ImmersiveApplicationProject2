using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRStepSequence : MonoBehaviour
{
    [System.Serializable]
    public class Step
    {
        public string stepName;
        public XRSocketInteractor requiredSocket;   // Socket for this step
        public XRGrabInteractable requiredObject;   // Correct object for this step
        [HideInInspector] public bool isCompleted = false;
    }

    public Step[] steps;
    private int currentStepIndex = 0;

    [Header("Audio Feedback")]
    public AudioSource audioSource;
    public AudioClip correctSound;
    public AudioClip wrongSound;

    void Start()
    {
        // Disable ALL sockets at start
        foreach (var step in steps)
        {
            step.requiredSocket.socketActive = false;
            step.requiredSocket.selectEntered.AddListener(OnSocketPlaced);
        }

        // Only enable the first step
        if (steps.Length > 0)
            steps[0].requiredSocket.socketActive = true;
    }

    private void OnSocketPlaced(SelectEnterEventArgs args)
    {
        Step currentStep = steps[currentStepIndex];

        if (args.interactableObject.transform.gameObject == currentStep.requiredObject.gameObject)
        {
            // ? Correct object
            currentStep.isCompleted = true;
            Debug.Log("? Step Completed: " + currentStep.stepName);

            // Play correct sound
            if (audioSource && correctSound)
                audioSource.PlayOneShot(correctSound);

            // Lock current step and go to next
            currentStep.requiredSocket.socketActive = false;
            currentStepIndex++;

            if (currentStepIndex < steps.Length)
            {
                steps[currentStepIndex].requiredSocket.socketActive = true;
                Debug.Log("?? Next Step: " + steps[currentStepIndex].stepName);
            }
            else
            {
                Debug.Log("?? All steps completed!");
            }
        }
        else
        {
            // ? Wrong object
            Debug.Log("? Wrong object! That doesn’t belong here.");

            // Play wrong sound
            if (audioSource && wrongSound)
                audioSource.PlayOneShot(wrongSound);

            // Prevent it from snapping into the socket
            currentStep.requiredSocket.interactionManager.SelectExit(
                currentStep.requiredSocket, args.interactableObject
            );
        }
    }
}

