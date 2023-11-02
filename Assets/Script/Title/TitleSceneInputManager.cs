using UnityEngine.Device;
using UnityEngine.SceneManagement;

namespace Script.Game
{
    public class TitleSceneInputManager : BaseInputManager
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
            Application.Quit();
        }

        protected override void OnMenu()
        {
            
        }
    }
}
