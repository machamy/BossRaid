using UnityEngine.SceneManagement;

namespace Script.Game
{
    public class GameSceneInputManager : BaseInputManager
    {
        // Start is called before the first frame update
        private void Start()
        {
            //base.Start();
        }

        protected override void Update()
        {
            //base.Update();
        }

        protected override void OnHome()
        {
            
        }

        protected override void OnEscape()
        {
            SceneManager.LoadScene("Scenes/TitleScreen");
        }

        protected override void OnMenu()
        {
            
        }
    }
}
