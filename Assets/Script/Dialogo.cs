using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class Dialogo : MonoBehaviour
{
    [SerializeField] private GameObject marcaDialogo;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)]private string[] lineasDialogo;

    private float typingTime=0.05f;
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int LineIndex;
    void Update()
    {
        if(isPlayerInRange && Input.GetButtonDown("Fire2"))
        {
            if(!didDialogueStart){
                IniciarDialogo();
            }
            else if (dialogueText.text == lineasDialogo[LineIndex]){
                siguienteLinea();
            }
            else{
                StopAllCoroutines();
                dialogueText.text= lineasDialogo[LineIndex];
            }


        }
    }

    private void IniciarDialogo()
    {
        didDialogueStart=true;
        dialoguePanel.SetActive(true);
        marcaDialogo.SetActive(false);
        LineIndex =0;
        Time.timeScale=0f;
        StartCoroutine(ShowLine());

    }

    private void siguienteLinea(){
        LineIndex++;
        if(LineIndex < lineasDialogo.Length)
        {
            StartCoroutine(ShowLine());
        }
        else{
            didDialogueStart=false;
            dialoguePanel.SetActive(false);
            marcaDialogo.SetActive(true);
            Time.timeScale=1;
        }
    }

    private IEnumerator ShowLine(){
        dialogueText.text=string.Empty;

        foreach(char ch in lineasDialogo[LineIndex]){
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){

        if(collision.gameObject.CompareTag("Player")){
            isPlayerInRange = true;
            marcaDialogo.SetActive(true);
        }
        

    }

    private void OnTriggerExit2D(Collider2D collision){

        if(collision.gameObject.CompareTag("Player")){
            marcaDialogo.SetActive(false);
            isPlayerInRange = false;
        }

    }
}
