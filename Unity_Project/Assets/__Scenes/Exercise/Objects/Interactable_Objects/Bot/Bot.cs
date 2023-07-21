using UnityEngine;

namespace ExerciseOne
{
    public class Bot : MonoBehaviour, IInteractableObject, IObjectDataHandler
    {
        [SerializeField] private ExerciseManagerData exerciseManagerData = null;

        private Renderer botRenderer = null;

        private void Awake()
        {
            botRenderer = transform.GetComponent<Renderer>();
        }

        private void OnEnable()
        {
            botRenderer.material.color = exerciseManagerData.botBaseColor;
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public void SetBaseColor()
        {
            botRenderer.material.color = exerciseManagerData.botBaseColor;
        }

        public void SetHighlightColor()
        {
            botRenderer.material.color = exerciseManagerData.botHighlightColor;
        }

        public ObjectData ExtractObjectData()
        {
            ObjectData objectData = new ObjectData
            {
                objectType = "Bot",
                objectName = transform.name,
                position = new SerializableVector3(transform.position),
                baseColor = new SerializableColor(exerciseManagerData.botBaseColor),
                highlightColor = new SerializableColor(exerciseManagerData.botHighlightColor)
            };

            return objectData;
        }

        public void ApplyObjectData(ObjectData _objectData)
        {
            transform.name = _objectData.objectName;
            transform.position = _objectData.position.ToVector3();

            exerciseManagerData.botBaseColor = _objectData.baseColor.ToColor();
            exerciseManagerData.botHighlightColor = _objectData.highlightColor.ToColor();
        }
    }
}