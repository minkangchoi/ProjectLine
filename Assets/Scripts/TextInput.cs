using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Yarn.Unity.Example {

    public class TextInput : MonoBehaviour
    {

        public GameObject inputObject;
        public GameObject displayObject;
        public GameObject inputContainer;
        public InputField inputField;
        public DialogueRunner dialogueRunner;
        public InMemoryVariableStorage inMemVarStorageRef;

        private Text inputText;
        private Text displyText;
        private string inputString;
        
        void Awake() {
            dialogueRunner.AddCommandHandler(
                "promptInput",
                PromptInput
            );
        }

        void Start() {
            inputText = inputObject.GetComponent<Text>();
            displyText = displayObject.GetComponent<Text>();
            Hide();
        }

        void PromptInput(string[] parameters, System.Action onComplete) {
            Display();
            inputField.ActivateInputField();
            StartCoroutine(StoreInput(onComplete));
        }

        IEnumerator StoreInput(System.Action onComplete) {
            while (!Input.GetKeyDown(KeyCode.Return) ) {
                yield return null;
            }

            inputString = inputText.text;
            displyText.text = inputString;
            inMemVarStorageRef.SetValue("$userInput", inputString);

            ClearInput();

            Hide();

            // call onComplete to continue yarn script
            onComplete();
        }

        public void Display() {
            inputContainer.SetActive(true);
        }

        public void Hide() {
            inputContainer.SetActive(false);
        }

        public void ClearInput() {
            inputField.text = "";
        }
    }
}
