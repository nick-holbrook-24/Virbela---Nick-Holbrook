using UnityEngine;
using UnityEngine.Assertions;

namespace ExerciseOne
{
    public class PlayerVisuals : MonoBehaviour
    {
        [SerializeField] private ExerciseManagerData exerciseManagerData = null;

        private Renderer playerRenderer = null;

        private const int avatarChildIndex = 0;

        private void Awake()
        {
            playerRenderer = transform.GetChild(avatarChildIndex).GetComponent<Renderer>();
        }

        private void OnEnable()
        {
            UpdatePlayerVisuals();
        }

        private void UpdatePlayerVisuals()
        {
            Assert.IsNotNull(exerciseManagerData);

            if (exerciseManagerData.playerDefaultColor != null)
            {
                playerRenderer.material = exerciseManagerData.playerDefaultMaterial;
            }

            if (exerciseManagerData.playerDefaultColor != Color.clear)
            {
                playerRenderer.material.color = exerciseManagerData.playerDefaultColor;
            }
        }
    }
}