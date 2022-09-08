using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates.UncontrolledStates
{
    public class UncontrolledWalkBackSubState : UncontrolledSubState
    {
        #region Initialization

        public UncontrolledWalkBackSubState(MainAnimalStateMachine context, UncontrolledSubStatesFactory factory) : base(context, factory){}

        private float _distanceBetweenPlayerAndAnimal;
        private int _layerBeforeEnter;

        #endregion

        #region SubStateMethods

        public override void OnStateEnter()
        {
            Context.AnimalAgent.enabled = true;
            Context.AnimalAgent.acceleration = 20;

            _layerBeforeEnter = Context.gameObject.layer;
            Context.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        }

        public override void OnStateUpdate()
        {
            var animalPosition = Context.AnimalTransform.position;
            _distanceBetweenPlayerAndAnimal = Vector3.Distance(animalPosition, Context.MainCharacterTransform.position);

            var mainCharacterRight = Context.MainCharacterTransform.right;

            var currentSpeed = Context.WalkBackSpeed.Evaluate(_distanceBetweenPlayerAndAnimal);
            Context.AnimalAgent.speed = currentSpeed;
            
            Vector3 nextDirection;
            
            var distanceToLeft = Vector3.Distance(animalPosition, -mainCharacterRight);
            var distanceToRight = Vector3.Distance(animalPosition, mainCharacterRight);
            
            if (distanceToRight < distanceToLeft) nextDirection = mainCharacterRight;
            else nextDirection = -mainCharacterRight;

            if (Context.AnimalAgent.isOnNavMesh) Context.AnimalAgent.SetDestination (Context.MainCharacterTransform.forward + nextDirection + animalPosition);
        }

        public override void OnStateExit()
        {
            Context.AnimalAgent.enabled = false;
            Context.gameObject.layer = _layerBeforeEnter;
        }

        public override bool TrySwitchState(out AnimalSubState newAnimalState)
        {
            newAnimalState = null;

            if (_distanceBetweenPlayerAndAnimal > Context.WalkBackDistance * 1.5f) newAnimalState = Factory.Idle();

            return newAnimalState != null;
        }

        #endregion
    }
}