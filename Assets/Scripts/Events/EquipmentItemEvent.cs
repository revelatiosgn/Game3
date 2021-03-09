using UnityEngine;
using UnityEngine.Events;

using ARPG.Items;

namespace ARPG.Events
{
    [CreateAssetMenu(menuName = "Events/EquipmentItem Event")]
    public class EquipmentItemEvent : ScriptableObject
    {
        public UnityAction<EquipmentItem, GameObject> onEventRaised;
        public void RaiseEvent(EquipmentItem value, GameObject target)
        {
            if (onEventRaised != null)
                onEventRaised.Invoke(value, target);
        }
    }
}
