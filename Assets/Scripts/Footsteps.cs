using UnityEngine;

namespace blox
{
    [RequireComponent(typeof(CharacterController), typeof(AudioSource))]
    public class Footsteps : MonoBehaviour
    {
        [SerializeField]
        private float velocityMagnitudeTreshold = 1.0f;
        
        private CharacterController characterController;
        private AudioSource audioSrc;
        
        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            audioSrc = GetComponent<AudioSource>();
        }
        
        void Update()
        {
            if (characterController.velocity.magnitude > velocityMagnitudeTreshold)
            {
                if (!audioSrc.isPlaying)
                {
                    audioSrc.Play();
                }

            }
            else
            {
                audioSrc.Pause();
            }
            // Debug.Log(characterController.velocity.magnitude);
        }
    }
}