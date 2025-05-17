using System.Collections.Generic;
using UnityEngine;
using TMPro; // Importante: necessário para usar TextMeshProUGUI

public class EducationalInfo : MonoBehaviour
{
    public TextMeshProUGUI successText;
    public TextMeshProUGUI errorText;

    private List<string> successMessages = new List<string>()
    {
        "Reciclar é transformar o mundo!",
        "Você protegeu a natureza!",
        "Muito bem! Lixo reciclado!",
        "Excelente! Um passo a mais pela Terra!",
        "Boa! O meio ambiente agradece!"
    };

    private List<string> errorMessages = new List<string>()
    {
        "A cidade é minha, eu vou vencer!",
        "Você precisa aprender mais sobre reciclagem!",
        "Errou! Mas ainda dá tempo de salvar o planeta!",
        "Hmm… vamos tentar de novo com mais atenção.",
        "O planeta precisa da sua ajuda!"
    };

    public void ShowSuccessMessage()
    {
        string msg = successMessages[Random.Range(0, successMessages.Count)];
        successText.text = msg;
        errorText.text = "";
    }

    public void ShowErrorMessage()
    {
        string msg = errorMessages[Random.Range(0, errorMessages.Count)];
        errorText.text = msg;
        successText.text = "";
    }

    public void HideInfo()
    {
        successText.text = "";
        errorText.text = "";
    }
}
