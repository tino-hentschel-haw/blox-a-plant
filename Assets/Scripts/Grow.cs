using UnityEngine;

namespace blox
{
    public class Grow : MonoBehaviour
    {
        [SerializeField] private float maxScale = 1.0f;
        [SerializeField] private float scaleRate = 0.2f;

        private float currentScale = 0.0f;

        void Update()
        {
            if (!(currentScale < maxScale))
                return;

            transform.localScale = Vector3.one * currentScale;
            currentScale += scaleRate * Time.deltaTime;
        }
    }
}