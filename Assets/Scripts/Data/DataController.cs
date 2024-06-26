using Bayat.SaveSystem;
using UnityEngine;

namespace Data
{
    public abstract class DataController : MonoBehaviour
    {
        [SerializeField]
        protected SaveSystemSettingsPreset settings;

        protected virtual void Awake()
        {
            if (settings == null)
            {
                return;
            }
            settings.ApplyTo(SaveSystemSettings.DefaultSettings);
        }

        public abstract void Save();

        public abstract void Load();

        public abstract void Delete();
    }
}