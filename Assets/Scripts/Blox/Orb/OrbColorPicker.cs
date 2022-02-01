using UnityEngine;

namespace blox.orb
{
    public class OrbColorPicker : MonoBehaviour
    {
        [SerializeField] private Orb orb;
        [SerializeField] private OrbColorPickerComponent[] colorPickerComponents;
        [SerializeField] private OrbColorPickerComponent initialColorPickerComponent;
        [SerializeField] private float radius;
        [SerializeField] private bool updateAtRuntime;

        private OrbColorPickerComponent currentColorPickerComponent;

        protected AudioSource[] colorAudios;


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

            colorAudios = GetComponents<AudioSource>();
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

            var handColorSelector = other.GetComponent<HandColorSelector>();
            if(!handColorSelector)
                return;
            
            // if (!other.CompareTag("colorSelector"))
            //     return;

            transform.localRotation = handColorSelector.OrbColorPickerRotationOtherHand;

            foreach (var colorPickerComponent in colorPickerComponents)
            {
                colorPickerComponent.gameObject.SetActive(true);
            }
            
            //Color Picker Opened Sound
            colorAudios[0].Play();

        }

        private void OnTriggerExit(Collider other)
        {
            var handColorSelector = other.GetComponent<HandColorSelector>();
            if(!handColorSelector)
                return;

            
            // if (!other.CompareTag("colorSelector"))
            //     return;

            foreach (var colorPickerComponent in colorPickerComponents)
            {
                colorPickerComponent.gameObject.SetActive(false);
            }
            
            //Color Picker Closed Sound
            colorAudios[0].Stop();
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
            
            //Color Selected Sound
            colorAudios = GetComponents<AudioSource>();
            colorAudios[1].Play();
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