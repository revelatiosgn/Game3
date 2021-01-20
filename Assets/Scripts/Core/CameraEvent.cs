using UnityEngine;
using UnityEngine.Events;

namespace ARPG.Core
{
    [CreateAssetMenu(menuName = "Events/Camera Event")]
    public class CameraEvent : ScriptableObject
    {
        public UnityAction OnEventRaised;
        public void RaiseEvent()
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke();
        }
    }
}
