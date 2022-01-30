using UnityEngine;

namespace blox
{
    public class OrbColorPicker : MonoBehaviour
    {
        [SerializeField] private Orb orb;
        [SerializeField] private OrbColorPickerComponent[] colorPickerComponents;
        [SerializeField] private OrbColorPickerComponent initialColorPickerComponent;
        [SerializeField] private float radius;
        [SerializeField] private bool updateAtRuntime;

        private OrbColorPickerComponent currentColorPickerComponent;


        private void OnEnable()
        {
            foreach (var colorPickerComponent in colorPickerComponents)
            {
                colorPickerComponent.OnColorSelect += OnColorSelect;
            }
        }

        private void OnDisable()
        {
            foreach (var colorPickerComponent in colorPickerComponents)
            {
                colorPickerComponent.OnColorSelect -= OnColorSelect;
            }
        }

        private void Start()
        {
            PositionColorPickerComponents();
            PickColor(initialColorPickerComponent);

            foreach (var colorPickerComponent in colorPickerComponents)
            {
                colorPickerComponent.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (updateAtRuntime)
                PositionColorPickerComponents();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(!orb.Selected)
                return;
            
            if (!other.CompareTag("colorSelector"))
                return;

            foreach (var colorPickerComponent in colorPickerComponents)
            {
                colorPickerComponent.gameObject.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("colorSelector"))
                return;

            foreach (var colorPickerComponent in colorPickerComponents)
            {
                colorPickerComponent.gameObject.SetActive(false);
            }
        }

        private void OnColorSelect(OrbColorPickerComponent colorPickerComponent)
        {
            if (currentColorPickerComponent)
                currentColorPickerComponent.Deselect();

            PickColor(colorPickerComponent);
        }

        private void PickColor(OrbColorPickerComponent colorPickerComponent)
        {
            currentColorPickerComponent = colorPickerComponent;
            currentColorPickerComponent.Select();
            orb.SetColor(currentColorPickerComponent.Material.color);
        }

        private void PositionColorPickerComponents()
        {
            for (var i = 0; i < colorPickerComponents.Length; i++)
            {
                var angle = i * Mathf.PI * 2 / colorPickerComponents.Length;
                var x = Mathf.Cos(angle) * radius;
                var z = Mathf.Sin(angle) * radius;
                var pos = transform.position + new Vector3(x, 0, z);
                var angleDegrees = -angle * Mathf.Rad2Deg;
                var rotation = Quaternion.Euler(0, angleDegrees, 0);

                colorPickerComponents[i].transform.localPosition = pos - transform.position;
                colorPickerComponents[i].transform.localRotation = rotation;

                // colorPickerComponents[i].transform.SetPositionAndRotation(pos, rotation);
            }
        }
    }
}