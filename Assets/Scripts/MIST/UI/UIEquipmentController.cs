using UnityEngine;

using MIST.EQ;

namespace MIST.UI
{
    public class UIEquipmentController : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private UIEquipmentSlot[] equipmentSlots;

        [Header("Player")]
        [SerializeField] private EquipmentController playerEquipmentController;

        private void Awake() => playerEquipmentController.OnEquipmentChanged += UpdateEquipmentSlots;

        private void Start() => UpdateEquipmentSlots();

        private void OnDestroy() => playerEquipmentController.OnEquipmentChanged -= UpdateEquipmentSlots;

        private void UpdateEquipmentSlots()
        {
            foreach (var slot in equipmentSlots)
                slot.UpdateSlotPresence(playerEquipmentController.equipment);
        }
    }
}