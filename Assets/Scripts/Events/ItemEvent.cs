using UnityEngine;
using UnityEngine.Events;

using ARPG.Items;

namespace ARPG.Events
{
    [CreateAssetMenu(menuName = "Events/Item Event")]
    public class ItemEvent : ScriptableObject
    {
        public UnityAction<Item> OnEventRaised;
        public void RaiseEvent(Item value)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(value);
        }
    }
}
