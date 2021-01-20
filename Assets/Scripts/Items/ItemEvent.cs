using UnityEngine;
using UnityEngine.Events;

namespace ARPG.Items
{
    [CreateAssetMenu(menuName = "Events/Item Event")]
    public class ItemEvent : ScriptableObject
    {
        public UnityAction<Item> OnEventRaised;
        public void RaiseEvent(Item item)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(item);
        }
    }
}
