using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FindPairs
{
    public class MainCard : MonoBehaviour
    {

        #region Variables

        public GameObject cardBack;
        private LevelController controller;

        private int _id;

        #endregion

        #region Properties

        public int Id
        {
            get { return _id; }
        }

        #endregion

        #region BuiltIn Methods

        private void Awake()
        {
            controller = FindObjectOfType<LevelController>();
        }

        public void OnMouseDown()
        {
            if(cardBack.activeSelf && controller.CanReveal)
            {
                RevealCard();
                controller.CardRevealed(this);
            }
        }

        #endregion

        #region Custom Methods

        public void ChangeSprite(int id, Sprite image)
        {
            _id = id;
            GetComponent<SpriteRenderer>().sprite = image;
        }

        public void UnRevealCard()
        {
            cardBack.SetActive(true);
        }

        public void RevealCard()
        {
            cardBack.SetActive(false);
        }

        #endregion

    }
}
