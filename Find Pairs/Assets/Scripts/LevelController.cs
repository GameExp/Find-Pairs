using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FindPairs
{
    public class LevelController : MonoBehaviour
    {

        #region Variables

        public const int gridRows = 2;
        public const int gridCols = 4;
        public const float offsetX = 8f;
        public const float offsetY = 10f;

        public MainCard originalCard;
        public Sprite[] images;

        private MainCard _firstRevealedCard;
        private MainCard _secondRevealedCard;

        private int _score = 0;
        public TextMesh scoreLabel;
        public GameObject matchKnownLabel;

        public Timer timer;

        private int totalPairsFound = 0;

        private Dictionary<int, int> openedCards;

        private bool perfectRecall = true;
        public bool allTilesRevealedAtStart;

        public GameObject winLabel;
        public GameObject loseLabel;

        #endregion

        #region Properties

        public bool CanReveal
        {
            get { return _secondRevealedCard == null; }
        }

        #endregion

        #region BuiltIn Methods

        private void Awake()
        {
            winLabel.SetActive(false);
            loseLabel.SetActive(false);
        }

        private void Start()
        {
            timer.IsGameOver = true;
            openedCards = new Dictionary<int, int>();
            matchKnownLabel.SetActive(false);

            Vector3 startPos = originalCard.transform.position;

            int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 };
            numbers = ShuffledArray(numbers);

            for(int i = 0; i < gridCols; i++)
            {
                for(int j = 0; j < gridRows; j++)
                {
                    MainCard card;
                    if(i == 0 && j == 0)
                    {
                        card = originalCard;
                    }
                    else
                    {
                        card = Instantiate(originalCard) as MainCard;
                    }

                    int index = j * gridCols + i;
                    int id = numbers[index];
                    card.ChangeSprite(id, images[id]);

                    float posX = (offsetX * i) + startPos.x;
                    float posY = (offsetY * j) + startPos.y;
                    card.transform.position = new Vector3(posX, posY, startPos.z);
                }
            }

            if(allTilesRevealedAtStart)
            {
                StartCoroutine(DisplayAllCards());
            }
            else
            {
                timer.IsGameOver = false;
            }
        }

        #endregion

        #region Custom Methods

        private int[] ShuffledArray(int[] numbers)
        {
            int[] newArray = numbers.Clone() as int[];
            for(int i = 0; i < newArray.Length; i++)
            {
                int temp = newArray[i];
                int randomIndex = Random.Range(i, newArray.Length);
                newArray[i] = newArray[randomIndex];
                newArray[randomIndex] = temp;
            }
            return newArray;
        }

        public void CardRevealed(MainCard _card)
        {
            if(_firstRevealedCard == null)
            {
                _firstRevealedCard = _card;
                if(openedCards.ContainsKey(_firstRevealedCard.Id))
                {
                    openedCards[_firstRevealedCard.Id]++;
                    matchKnownLabel.SetActive(true);
                }
                else
                {
                    openedCards.Add(_firstRevealedCard.Id, 0);
                }
            }
            else
            {
                _secondRevealedCard = _card;
                StartCoroutine(CheckMatch());
            }
        }

        private IEnumerator DisplayAllCards()
        {
            // reveal all cards
            MainCard[] cards = FindObjectsOfType<MainCard>();
            if(cards != null)
            {
                foreach(MainCard card in cards)
                {
                    card.RevealCard();
                }
            }

            // wait for seconds
            yield return new WaitForSeconds(5f);

            // unreveal all tiles
            if(cards != null)
            {
                foreach(MainCard card in cards)
                {
                    card.UnRevealCard();
                    timer.IsGameOver = false;
                }
            }
        }

        private IEnumerator CheckMatch()
        {
            if(_firstRevealedCard.Id == _secondRevealedCard.Id)
            {
                _score += 20;
                scoreLabel.text = "Score: " + _score;
                openedCards.Remove(_firstRevealedCard.Id);

                totalPairsFound++;
                if(totalPairsFound >= images.Length)
                {
                    HandleWinSituation();
                }
            }
            else
            {
                yield return new WaitForSeconds(0.5f);

                
                if(openedCards[_firstRevealedCard.Id] > 0)
                {
                    _score += (-5 * openedCards[_firstRevealedCard.Id]);
                    scoreLabel.text = "Score: " + _score;
                    perfectRecall = false;
                }

                _firstRevealedCard.UnRevealCard();
                _secondRevealedCard.UnRevealCard();
            }

            matchKnownLabel.SetActive(false);

            _firstRevealedCard = null;
            _secondRevealedCard = null;
        }

        public void TimeOver()
        {
            if(totalPairsFound >= images.Length)
            {
                HandleWinSituation();
            }
            else
            {
                HandleLoseSituation();
            }
        }

        private void HandleWinSituation()
        {
            winLabel.SetActive(true);
            timer.IsGameOver = true;
            // check if any time is remaining and add bonus
            if(timer.timer > 0)
            {
                _score += Mathf.RoundToInt(timer.timer);
                scoreLabel.text = "Score: " + _score;
            }

            // check if it is a perfect recall and add bonus
            if(perfectRecall)
            {
                _score += 5 * images.Length;
                scoreLabel.text = "Score: " + _score;
            }

            int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
            switch(currentBuildIndex)
            {
                case 2:
                    PlayerPrefs.SetInt("LEVEL_1_SCORE", _score);
                    SceneLoader.sceneLoader.level01MaxScore = _score;
                    break;
                case 3:
                    PlayerPrefs.SetInt("LEVEL_2_SCORE", _score);
                    SceneLoader.sceneLoader.level02MaxScore = _score;
                    break;
                case 4:
                    PlayerPrefs.SetInt("LEVEL_3_SCORE", _score);
                    SceneLoader.sceneLoader.level03MaxScore = _score;
                    break;
                case 5:
                    PlayerPrefs.SetInt("LEVEL_4_SCORE", _score);
                    SceneLoader.sceneLoader.level04MaxScore = _score;
                    break;
                case 6:
                    PlayerPrefs.SetInt("LEVEL_5_SCORE", _score);
                    SceneLoader.sceneLoader.level05MaxScore = _score;
                    break;
                case 7:
                    PlayerPrefs.SetInt("LEVEL_6_SCORE", _score);
                    SceneLoader.sceneLoader.level06MaxScore = _score;
                    break;
                default:
                    Debug.Log("Not correct level index");
                    break;
            }

            SceneLoader.sceneLoader.unlockedLevels++;
            PlayerPrefs.SetInt("UNLOCKED_LEVELS", SceneLoader.sceneLoader.unlockedLevels);
            
        }

        private void HandleLoseSituation()
        {
            timer.IsGameOver = true;
            // enable required UI
            loseLabel.SetActive(true);
        }

        #endregion

    }
}