using Unity.FPS.Game;
using UnityEngine;

namespace Unity.FPS.AI
{
    public class FollowPlayer : MonoBehaviour
    {
        Transform m_PlayerTransform;
        Vector3 m_OriginalOffset;
        Vector3 lastPosition;

        [Header("Configuración de Movimiento")]
        public float speed = 3f;
        public float rotationSpeed = 5f;

        [Header("Configuración de Sonido")]
        [Tooltip("Arrastra aquí el clip de audio de las pisadas")]
        public AudioClip sonidoPisada;

        Animator animator;
        AudioSource audioSource;

        void Start()
        {
            ActorsManager actorsManager = FindAnyObjectByType<ActorsManager>();

            if (actorsManager != null && actorsManager.Player != null)
            {
                m_PlayerTransform = actorsManager.Player.transform;
                lastPosition = transform.position;
            }
            else
            {
                enabled = false;
                return;
            }

            m_OriginalOffset = transform.position - m_PlayerTransform.position;
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();

            // Si no hay AudioSource, lo crea automáticamente
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        void Update()
        {
            if (m_PlayerTransform == null) return;

            Vector3 targetPosition = m_PlayerTransform.position + m_OriginalOffset;

            // Movimiento suave hacia el objetivo
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                speed * Time.deltaTime
            );

            // Rotación hacia el jugador
            Vector3 direction = m_PlayerTransform.position - transform.position;
            direction.y = 0;

            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(direction),
                    rotationSpeed * Time.deltaTime
                );
            }

            // Detección de movimiento para la animación
            float distanceMoved = Vector3.Distance(transform.position, lastPosition);
            bool moving = distanceMoved > 0.02f; 

            if (animator != null)
            {
                animator.SetBool("IsWalking", moving);
            }

            lastPosition = transform.position;
        }

        // Esta función la llama el Animation Event 'PlayStep'
        public void PlayStep()
        {
            if (audioSource != null && sonidoPisada != null)
            {
                audioSource.PlayOneShot(sonidoPisada);
            }
        }
    }
}