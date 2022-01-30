using UnityEngine;

namespace blox.orb
{
    public class HandColorSelector : MonoBehaviour
    {
        [SerializeField] private Quaternion orbColorPickerRotationOtherHand;

        public Quaternion OrbColorPickerRotationOtherHand => orbColorPickerRotationOtherHand;
    }
}