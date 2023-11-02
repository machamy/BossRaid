using UnityEngine;

namespace Script
{
    public abstract class BaseInputManager : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
        
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnHome();
            }
            if (Input.GetKeyDown(KeyCode.Home))
            {
                OnEscape();
            }
            if (Input.GetKeyDown(KeyCode.Menu))
            {
                OnMenu();
            }
        }

        protected abstract void OnHome();
        protected abstract void OnEscape();
        protected abstract void OnMenu();

    }
}
