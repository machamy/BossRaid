using UnityEngine.SceneManagement;

namespace Script.Game
{
    public class GameSceneInputManager : BaseInputManager
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
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
