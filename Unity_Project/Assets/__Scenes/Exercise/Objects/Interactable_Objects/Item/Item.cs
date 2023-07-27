using UnityEngine;

namespace ExerciseOne
{
    public class Item : MonoBehaviour, IInteractableObject, IObjectDataHandler
    {
        [SerializeField] private ExerciseManagerData exerciseManagerData = null;

        private Renderer itemRenderer = null;

        private const string itemType = "Item";

        private void Awake()
        {
            itemRenderer = transform.GetComponent<Renderer>();
        }

        private void OnEnable()
        {
            itemRenderer.material.color = exerciseManagerData.itemBaseColor;
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public void SetBaseColor()
        {
            itemRenderer.material.color = exerciseManagerData.itemBaseColor;
        }

        public void SetHighlightColor()
        {
            itemRenderer.material.color = exerciseManagerData.itemHighlightColor;
        }

        public ObjectData ExtractObjectData()
        {
            ObjectData objectData = new ObjectData
            {
                objectType = itemType,
                objectName = transform.name,
                position = new SerializableVector3(transform.position),
                baseColor = new SerializableColor(exerciseManagerData.itemBaseColor),
                highlightColor = new SerializableColor(exerciseManagerData.itemHighlightColor)
            };

            return objectData;
        }

        public void ApplyObjectData(ObjectData _objectData)
        {
            transform.name = _objectData.objectName;
            transform.position = _objectData.position.ToVector3();

            exerciseManagerData.itemBaseColor = _objectData.baseColor.ToColor();
            exerciseManagerData.itemHighlightColor = _objectData.highlightColor.ToColor();
        }

        public string GetObjectType()
        {
            return itemType;
        }

        public string GetObjectName()
        {
            return transform.name;
        }
    }
}