using UnityEngine;

namespace MIST.EQ.Instances
{
    public class PickableItemInstance : ItemInstance
    {
        #region physics
        protected override void OnTransformDetected(Transform detectedTransform)
        {
            // Get EquipmentController component
            EquipmentController eqController = detectedTransform.parent.GetComponentInParent<EquipmentController>();

            if (eqController == null)
            {
                // Set detected transform to null
                colliderDetector.detectedTransform = null;
                return;
            }
            else
            {
                // Pick up item and disable item instance object
                eqController.EquipOrPickUpItem(item);
                this.gameObject.SetActive(false);
            }
        }
        #endregion
    }
}