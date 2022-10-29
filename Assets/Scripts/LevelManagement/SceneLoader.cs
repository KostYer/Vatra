using UnityEngine.SceneManagement;

namespace LevelManagement
{
    public static class SceneLoader
    {
        public static void Load(GameScenes sceneName)
        {
            SceneManager.LoadScene(sceneName.ToString());
        }
    }
}