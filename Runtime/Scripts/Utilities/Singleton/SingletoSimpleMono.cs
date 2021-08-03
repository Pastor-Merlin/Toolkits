using UnityEngine;

namespace Toolkits
{
    public abstract class SingletoSimpleMono<T> : MonoBehaviour where T : SingletoSimpleMono<T>
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = (T)this;
                Init();
            }
            else
            {

            }
        }
        public virtual void Init()
        {

        }
    }
}