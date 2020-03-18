using UnityEngine;
using UnityEngine.UI;

namespace FindPairs
{
    public class LevelLoader : MonoBehaviour
    {

        #region Variables

        public Button[] levelButtons;
        public Text[] levelScoreTexts;

        #endregion

        #region BuiltIn Methods

        private void Start()
        {
            for(int i = 0; i < levelButtons.Length; i++)
            {
                Color originalColor = levelButtons[i].image.color;
                originalColor.a = 0.4f;
                levelButtons[i].image.color = originalColor;
                levelButtons[i].interactable = false;
            }


            for(int i = 0; i < SceneLoader.sceneLoader.unlockedLevels; i++)
            {
                Color originalColor = levelButtons[i].image.color;
                originalColor.a = 1f;
                levelButtons[i].image.color = originalColor;
                levelButtons[i].interactable = true;
            }

            levelScoreTexts[0].text = SceneLoader.sceneLoader.level01MaxScore.ToString();
            levelScoreTexts[1].text = SceneLoader.sceneLoader.level02MaxScore.ToString();
            levelScoreTexts[2].text = SceneLoader.sceneLoader.level03MaxScore.ToString();
            levelScoreTexts[3].text = SceneLoader.sceneLoader.level04MaxScore.ToString();
            levelScoreTexts[4].text = SceneLoader.sceneLoader.level05MaxScore.ToString();
            levelScoreTexts[5].text = SceneLoader.sceneLoader.level06MaxScore.ToString();
        }

        #endregion

        #region Custom Methods
        #endregion

    }
}
