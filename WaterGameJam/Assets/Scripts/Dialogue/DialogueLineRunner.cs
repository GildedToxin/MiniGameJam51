using TMPro;
using UnityEngine;

public class DialogueLineRunner : MonoBehaviour
{

    public TextMeshProUGUI text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DialogueManager.Instance.dialogueLineRunner = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
