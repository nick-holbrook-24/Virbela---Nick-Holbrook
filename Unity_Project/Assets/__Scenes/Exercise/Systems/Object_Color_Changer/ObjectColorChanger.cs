using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace ExerciseOne
{
    public class ObjectColorChanger : MonoBehaviour
    {
        private IInteractableObject previousNearest = null;
        private INearestObjectSystem nearestObjectSystem = null;
        private Transform playerTransform = null;
        private float secondsToWaitForNearestCheck = 1.0f;

        private CancellationTokenSource cancellationTokenSource = null;

        public void Initialize(INearestObjectSystem _nearestObjectSystem,
                Transform _playerTransform, float _secondsToWaitForNearestCheck)
        {
            nearestObjectSystem = _nearestObjectSystem;
            playerTransform = _playerTransform;
            secondsToWaitForNearestCheck = _secondsToWaitForNearestCheck;
        }

        private async void Start()
        {
            cancellationTokenSource = new CancellationTokenSource();
            await FindAndUpdateNearestObject(cancellationTokenSource.Token);
        }

        private async Task FindAndUpdateNearestObject(CancellationToken _cancellationToken)
        {
            while (!_cancellationToken.IsCancellationRequested)
            {
                IInteractableObject nearest =
                    nearestObjectSystem.FindNearestInteractable(playerTransform.position);

                if ((previousNearest != null) && (previousNearest != nearest))
                {
                    previousNearest.SetBaseColor();
                }

                if (nearest != null)
                {
                    nearest.SetHighlightColor();
                    previousNearest = nearest;
                }

                await Task.Delay((int)(secondsToWaitForNearestCheck * 1000), _cancellationToken);
            }
        }

        private void OnDestroy()
        {
            cancellationTokenSource.Cancel();
        }
    }
}