using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;




public class GameManager : MonoBehaviour
{
    public EducationalInfo educationalInfo; // Referência ao script EducationalInfo

    public Sprite[] cardFace;
    public Sprite cardBack;
    public GameObject[] cards;
    public Text matchText;

    private bool _init = false;
    private int _matches = 0;

    //SOM - 12/05
    public AudioClip successSound;  // Som para quando o par for encontrado
    public AudioClip errorSound;    // Som para quando o par não for encontrado
    private AudioSource audioSource; // Fonte de áudio para tocar os sons
    public AudioClip backgroundMusic;
    private AudioSource musicSource;


    void Start()
    {

        Card.DO_NOT = false;

        audioSource = GetComponent<AudioSource>();
        // Inicializa o AudioSource da música
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;           // Faz a música tocar em loop
        musicSource.volume = 0.2f;         // Volume moderado
        musicSource.Play();

        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Level1")
        {
            _matches = 0;
            matchText.text = "Acertos: 0";
        }
        else if (currentScene == "Level2")
        {
            _matches = 0;
            matchText.text = "Acertos: 0";
        }

    }



    // Update is called once per frame
    void Update()
    {
        if (!_init)
            initializeCards();

        if (Input.GetMouseButtonUp(0))
            checkCards();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }

    }

    void initializeCards()
    {
        Debug.Log("initializingCards() has been called");
        Debug.Log("cards size " + cards.Length);
        for (int id = 0; id < 2; id++)
        {


            for (int i = 1; i <= (cards.Length/2); i++)
            {
                
                bool test = false;
                int choice = 0;
                while (!test)
                {
                    choice = Random.Range(0, cards.Length);
                    test = !(cards[choice].GetComponent<Card>().initialized);
                }
                cards[choice].GetComponent<Card>().cardValue = i;
                cards[choice].GetComponent<Card>().initialized = true;
            }
        }
        foreach (GameObject c in cards)
            c.GetComponent<Card>().setupGraphics();

        if (!_init)
            _init = true;
    }

    public Sprite getCardBack()
    {
        return cardBack;
    }
    public Sprite getCardFace(int i)
    {
        return cardFace [i-1];
    }

    void checkCards()
    {
        List<int> c = new List<int>();
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i].GetComponent<Card>().state == 1)
                c.Add(i);
        }
        if (c.Count == 2)
            cardComparison(c);
    }

   void cardComparison(List<int> c)
    {
        Card.DO_NOT = true;
        int x = 0;
        if (cards[c[0]].GetComponent<Card>().cardValue == cards[c[1]].GetComponent<Card>().cardValue)
        {
            _matches++;
            matchText.text = "Acertos: " + _matches;

            // Toca som de sucesso
            audioSource.PlayOneShot(successSound);

            //educationalInfo.ShowSuccessMessage(cards[c[0]].GetComponent<Card>().cardValue);
            StartCoroutine(HideMatchedCardsAfterDelay(c));

            educationalInfo.ShowSuccessMessage();  // ✅ Aqui usamos o nome certo

            // Aguarda 2 segundos antes de ocultar as cartas
            StartCoroutine(HideMatchedCardsAfterDelay(c));
        }
        else
        {
            // Toca som de erro
            audioSource.PlayOneShot(errorSound);

            educationalInfo.ShowErrorMessage();  // ✅ Aqui também

            // Esconde a mensagem educativa após 1 segundo
            StartCoroutine(HideInfoAfterDelay());
        }

        for (int i = 0; i < c.Count; i++)
        {
            cards[c[i]].GetComponent<Card>().state = x;
            cards[c[i]].GetComponent<Card>().falseCheck();
        }

        // Verifica se todas as combinações foram feitas
        if (_matches >= cards.Length / 2)
        {
            string currentScene = SceneManager.GetActiveScene().name;

            if (currentScene == "Level1")
            {
                SceneManager.LoadScene("Level2");
            }
            else if (currentScene == "Level2")
            {
                SceneManager.LoadScene("Level3");
            }
            else if (currentScene == "Level3")
            {
                SceneManager.LoadScene("FinalScene"); // ✅ Leva à nova cena final
            }
            else
            {
                Debug.Log("Fim do jogo. Nenhuma próxima fase configurada.");
            }
        }



    }

    // Função para esconder a mensagem educativa após 1 segundo
    IEnumerator HideInfoAfterDelay()
    {
        yield return new WaitForSeconds(1);
        educationalInfo.HideInfo();
    }

    IEnumerator HideMatchedCardsAfterDelay(List<int> c)
    {
        yield return new WaitForSeconds(1f);

        cards[c[0]].transform.localScale = new Vector3(0, 0, 0);
        cards[c[1]].transform.localScale = new Vector3(0, 0, 0);
    }

}