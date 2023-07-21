using UnityEngine;

namespace ExerciseOne
{
    public interface IInteractableObject
    {
        Transform GetTransform();
        void SetBaseColor();
        void SetHighlightColor();
    }
}