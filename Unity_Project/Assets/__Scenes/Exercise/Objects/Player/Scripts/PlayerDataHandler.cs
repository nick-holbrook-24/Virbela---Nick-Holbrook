using UnityEngine;

namespace ExerciseOne
{
    public class PlayerDataHandler : MonoBehaviour, IObjectDataHandler
    {
        [SerializeField] private ExerciseManagerData exerciseManagerData = null;

        private const string playerType = "Player";

        public ObjectData ExtractObjectData()
        {
            ObjectData objectData = new ObjectData
            {
                objectType = playerType,
                objectName = transform.name,
                position = new SerializableVector3(transform.position),
                baseColor = new SerializableColor(exerciseManagerData.playerDefaultColor),
                highlightColor = new SerializableColor(Color.clear)
            };

            return objectData;
        }

        public void ApplyObjectData(ObjectData _objectData)
        {
            transform.name = _objectData.objectName;
            transform.position = _objectData.position.ToVector3();

            exerciseManagerData.playerDefaultColor = _objectData.baseColor.ToColor();
        }

        public string GetObjectType()
        {
            return playerType;
        }

        public string GetObjectName()
        {
            return transform.name;
        }
    }
}