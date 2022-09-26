using UnityEngine;

namespace PathCreation.Examples
{
    public class PathFollower : MonoBehaviour
    {
        public float pathProgress { get; private set; }
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        private float distanceTravelled;

        private void Start()
        {
            if (pathCreator != null)
                pathCreator.pathUpdated += OnPathChanged;
        }

        private void Update()
        {
            if (pathCreator != null && GameEvents.instance.gameStarted.Value 
                && !GameEvents.instance.gameWon.Value && !GameEvents.instance.gameLost.Value)
            {
                distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                pathProgress = pathCreator.path.GetPercentage(distanceTravelled);
            }
        }

        private void OnPathChanged()
        {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }
}