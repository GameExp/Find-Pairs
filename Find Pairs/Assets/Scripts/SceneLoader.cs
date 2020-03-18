using UnityEngine;
using UnityEngine.SceneManagement;

namespace FindPairs
{
    public class SceneLoader : MonoBehaviour
    {

        #region Varibales

        public static SceneLoader sceneLoader;

        public int unlockedLevels = 1;
        public int level01MaxScore = 0;
        public int level02MaxScore = 0;
        public int level03MaxScore = 0;
        public int level04MaxScore = 0;
        public int level05MaxScore = 0;
        public int level06MaxScore = 0;

        #endregion

        #region BuiltIn Methods

        private void Awake()
        {
            int noOfSceneLoaders = FindObjectsOfType<SceneLoader>().Length;
            if(noOfSceneLoaders > 1)
            {
                Destroy(this);
            }
            else
            {
                DontDestroyOnLoad(this);
                sceneLoader = this;
            }
        }

        private void Start()
        {
            // get unloacked Levels
            if (!(PlayerPrefs.HasKey("UNLOCKED_LEVELS")))
            {
                PlayerPrefs.SetInt("UNLOCKED_LEVELS", unlockedLevels);
                PlayerPrefs.SetInt("LEVEL_1_SCORE", level01MaxScore);
                PlayerPrefs.SetInt("LEVEL_2_SCORE", level02MaxScore);
                PlayerPrefs.SetInt("LEVEL_3_SCORE", level03MaxScore);
                PlayerPrefs.SetInt("LEVEL_4_SCORE", level04MaxScore);
                PlayerPrefs.SetInt("LEVEL_5_SCORE", level05MaxScore);
                PlayerPrefs.SetInt("LEVEL_6_SCORE", level06MaxScore);
            }
            else
            {
                unlockedLevels = PlayerPrefs.GetInt("UNLOCKED_LEVELS");
                level01MaxScore = PlayerPrefs.GetInt("LEVEL_1_SCORE");
                level02MaxScore = PlayerPrefs.GetInt("LEVEL_2_SCORE");
                level03MaxScore = PlayerPrefs.GetInt("LEVEL_3_SCORE");
                level04MaxScore = PlayerPrefs.GetInt("LEVEL_4_SCORE");
                level05MaxScore = PlayerPrefs.GetInt("LEVEL_5_SCORE");
                level06MaxScore = PlayerPrefs.GetInt("LEVEL_6_SCORE");
            }


        }

        #endregion

        #region Custom Methods

        public void StartGame()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            SceneManager.LoadScene(currentSceneIndex + 1);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void LoadLevel(int levelIndex)
        {
            SceneManager.LoadScene(levelIndex);
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void Retry()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }

        #endregion
    }
}