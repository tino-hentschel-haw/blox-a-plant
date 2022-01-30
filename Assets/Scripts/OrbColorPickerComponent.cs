using UnityEngine;

namespace blox
{
    public class OrbColorPickerComponent : MonoBehaviour
    {
        [SerializeField] private float scaleByFactor;
        private Vector3 defaultScale;
        private Vector3 selectedScale;

        public Material Material { get; private set; }

        public delegate void ColorSelectDelegate(OrbColorPickerComponent orbColorPickerComponent);

        public event ColorSelectDelegate OnColorSelect;


        private void Awake()
        {
            Material = GetComponent<MeshRenderer>().material;
            defaultScale = transform.localScale;
            selectedScale = new Vector3(defaultScale.x * scaleByFactor, defaultScale.y, defaultScale.z);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("colorSelector"))
                return;

            OnColorSelect?.Invoke(this);
        }

        public void Select()
        {
            transform.localScale = selectedScale;
        }

        public void Deselect()
        {
            transform.localScale = defaultScale;
        }
    }
}