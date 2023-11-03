using UnityEngine;
using UnityEngine.tvOS;

namespace Script
{
    public abstract class BaseInputManager : MonoBehaviour
    {
        // Start is called before the first frame update
        protected virtual void Start()
        {
        
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("ESCAPE");
                OnEscape();
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
