using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FindPairs
{
    public class Timer : MonoBehaviour
    {

        #region Variables

        private bool isGameOver = false;
        public float timer = 60f;

        #endregion

        #region Properties

        public bool IsGameOver
        {
            set { isGameOver = value; }
            get { return isGameOver; }
        }

        #endregion

        #region BuiltIn Methods

        private void Update()
        {
            if (IsGameOver)
                return;

            timer -= Time.deltaTime;
            GetComponent<TextMesh>().text = Mathf.RoundToInt(timer).ToString();

            if(timer <= 0)
            {
                FindObjectOfType<LevelController>().TimeOver();
            }
        }

        #endregion

        #region Custom Methods
        #endregion

    }
}