using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    //declaring card back image
    private Sprite cardBack;
    //created list of buttons that will represent the cards
    public List<Button> playCards = new List<Button>();
   //array of images provided
    public Sprite[] cardImages;
    //list of sprites inside card puzzle
    public List<Sprite> CardPuzzle = new List<Sprite>();
   //boolean and ints to handle the game logic
    private bool Firstpick;
    private bool Secondpick;
    private int guessCount;
    private int correctguess;
    private int totalGuess;
    private string FirstPickName;
    private string SecondPickName;
    private int FirstPickIndex;
    private int SecondPickIndex;
    void Start()
    {
        GetCards();
        AddOnClickListener();
        addCardPuzzle();
        RandomizeGrid(CardPuzzle);
        totalGuess = playCards.Count/2;
    }
    //method to intiliaze cards and set image as card back
    void GetCards()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Card");
        for (int x = 0; x < objects.Length; x++)
        {
            playCards.Add(objects[x].GetComponent<Button>());
            playCards[x].image.sprite = cardBack;
        }
    }
    //method that adds the sprites to the cardpuzzle list
    void addCardPuzzle()
    {
        int cnt = playCards.Count;
        int index =0;
        for (int x = 0; x < cnt; x++)
        {
            if (index == cnt / 2)
            {
                index = 0;
            }
            CardPuzzle.Add(cardImages[index]);
            index++;
        }
    }
    //method that adds listeners to buttons as they are intilalized
    void AddOnClickListener()
    {
        foreach (Button btn in playCards)
        {
            btn.onClick.AddListener(() => cardPicked());
        } 
    }
    //method to handle the selecting of cards and matches created
    public void cardPicked()
        {
            //determine what was clicked
            Button clickedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            string clickedObjectName = clickedButton.gameObject.name;
            Debug.Log("Card picked: " + clickedObjectName);
            //display sprite for picked card
            if(!Firstpick)
            {
                Firstpick = true;
                FirstPickIndex = playCards.IndexOf(clickedButton);
                FirstPickName = CardPuzzle[FirstPickIndex].name;
                playCards[FirstPickIndex].image.sprite = CardPuzzle[FirstPickIndex];
            }
            else if (!Secondpick)
            {
                Secondpick = true;  
                SecondPickIndex = playCards.IndexOf(clickedButton);
                SecondPickName = CardPuzzle[SecondPickIndex].name;
                playCards[SecondPickIndex].image.sprite = CardPuzzle[SecondPickIndex];
                guessCount++;
                if (FirstPickName == SecondPickName){
                Debug.Log("Match Created"); 
                }
                else{
                Debug.Log("no Match Created"); 
                }
                StartCoroutine(CheckMatchCreated());

            }
            //check if match has been created
            IEnumerator CheckMatchCreated(){
                yield return new WaitForSeconds (1f);
                if (FirstPickName == SecondPickName)
                {
                    //disable selection after 2 choices are made
                    yield return new WaitForSeconds (1f);
                    playCards[FirstPickIndex].interactable = false;
                    playCards[SecondPickIndex].interactable = false;

                    playCards[FirstPickIndex].image.color = new Color(0,0,0,0);
                    playCards[SecondPickIndex].image.color = new Color(0,0,0,0);
                    CheckIfAlltilesSelected();
                }
                else{
                playCards[FirstPickIndex].image.sprite = cardBack;
                playCards[SecondPickIndex].image.sprite = cardBack; 
                }
               yield return new WaitForSeconds (1f); 
                Firstpick = Secondpick = false;

            }
            //check if game is finished
            void CheckIfAlltilesSelected(){
                correctguess++;
                if(correctguess == totalGuess){
                    Debug.Log("Finished");
                }
            }
        }
        //randomizes the placement of the sprites
        void RandomizeGrid(List<Sprite> list)
        {
            for(int x=0; x< list.Count;x++){
                Sprite temp = list[x];
                int indexswap = Random.Range(0,list.Count);
                list[x] = list[indexswap];
                list[indexswap] = temp;
            }
        }
}
