using UnityEngine;

namespace ExerciseOne
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private ExerciseManagerData exerciseManagerData = null;

        private CharacterController characterController = null;
        private Vector2 playerRotation = Vector2.zero;
        private float playerMovementSpeed = 0.0f;
        private Vector3 movementDirection = Vector3.zero;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        private void FixedUpdate()
        {
            movementDirection.Normalize();

            if(movementDirection == Vector3.zero)
            {
                return;
            }

            characterController.Move(movementDirection * playerMovementSpeed * Time.deltaTime);
        }

        private void Update()
        {
            float playerRotationSpeed;

            playerRotationSpeed = exerciseManagerData.playerRotationSpeed;

            if (playerRotationSpeed > 0)
            {
                playerRotation.x += Input.GetAxis("Mouse X") * playerRotationSpeed;
                playerRotation.y += Input.GetAxis("Mouse Y") * playerRotationSpeed;
                playerRotation.y = Mathf.Clamp(playerRotation.y, -1f * 
                    exerciseManagerData.playerYRotationLimit,
                    exerciseManagerData.playerYRotationLimit);

                var xQuat = Quaternion.AngleAxis(playerRotation.x, Vector3.up);
                var yQuat = Quaternion.AngleAxis(playerRotation.y, Vector3.left);

                transform.localRotation = xQuat * yQuat;
            }

            // If the movement speed is <= 0, then there is no point in checking movement input.
            playerMovementSpeed = exerciseManagerData.playerMovementSpeed;
            if (playerMovementSpeed <= 0.0f)
            {
                return;
            }

            if ((Input.GetKey(KeyCode.LeftShift) == true) && (exerciseManagerData.playerCurrentRunStamina > 0))
            {
                playerMovementSpeed = exerciseManagerData.playerRunSpeed;
                exerciseManagerData.playerCurrentRunStamina -= (Time.deltaTime * exerciseManagerData.playerRateOfStaminaLoss);
            }
            else if ((Input.GetKey(KeyCode.LeftShift) == false)
                && (exerciseManagerData.playerCurrentRunStamina < exerciseManagerData.playerRunMaxStamina))
            {
                exerciseManagerData.playerCurrentRunStamina += (Time.deltaTime * exerciseManagerData.playerRateOfStaminaGain);
            }

            float xAxisMovement = Input.GetAxis("Horizontal");
            float zAxisMovement = Input.GetAxis("Vertical");

            movementDirection = (transform.right * xAxisMovement) + (transform.forward * zAxisMovement);
        }
    }
}