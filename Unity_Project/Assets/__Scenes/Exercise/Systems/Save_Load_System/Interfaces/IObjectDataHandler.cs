namespace ExerciseOne
{
    public interface IObjectDataHandler : IObjectDataExtractor
    {
        public void ApplyObjectData(ObjectData _objectData);
        public string GetObjectType();
        public string GetObjectName();
    }
}
