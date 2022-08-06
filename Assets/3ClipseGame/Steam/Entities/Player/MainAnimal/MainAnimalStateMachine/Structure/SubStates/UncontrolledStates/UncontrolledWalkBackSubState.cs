using _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.States;
using _3ClipseGame.Steam.Entities.Player.Scripts.PlayerMoverScripts;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.MainAnimalStateMachine.Structure.SubStates.UncontrolledStates
{
    public class UncontrolledWalkBackSubState : AnimalSubState
    {
        public UncontrolledWalkBackSubState(MainAnimalStateMachine context, AnimalStateFactory factory) : base(context, factory) =>
            _factory = (UncontrolledSubStatesFactory)factory;

        private float _distanceBetweenPlayerAndAnimal;
        private UncontrolledSubStatesFactory _factory;

        public override void OnStateEnter()
        {
            Context.AnimalAgent.enabled = true;
            Context.AnimalAgent.acceleration = 20;
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

            if (distanceToRight < distanceToLeft)
            {
                Debug.Log("Right");
                nextDirection = mainCharacterRight;
            }
            else
            {
                Debug.Log("Left");
                nextDirection = -mainCharacterRight;
            }
            
            Context.AnimalAgent.SetDestination (Context.MainCharacterTransform.forward + nextDirection + animalPosition);
        }

        public override void OnStateExit()
        {
            Context.AnimalAgent.enabled = false;
        }

        public override bool TrySwitchState(out AnimalState newAnimalState)
        {
            newAnimalState = null;

            if (_distanceBetweenPlayerAndAnimal > Context.WalkBackDistance * 1.5f) newAnimalState = _factory.Idle();

            return newAnimalState != null;
        }
    }
}