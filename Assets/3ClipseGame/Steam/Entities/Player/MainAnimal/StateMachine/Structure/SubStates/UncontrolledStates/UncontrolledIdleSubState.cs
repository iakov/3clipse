using _3ClipseGame.Steam.Entities.Scripts.CharacterMover;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.MainAnimal.StateMachine.Structure.SubStates.UncontrolledStates
{
    public class UncontrolledIdleSubState : UncontrolledSubState
    {
        public UncontrolledIdleSubState(MainAnimalStateMachine context, UncontrolledSubStatesFactory factory) : base(context, factory) {}

        private float _distanceBetweenPlayerAndAnimal;

        public override void OnStateEnter()
        {
            Context.AnimalMover.ChangeMove(MoveType.StateMove, Vector3.zero, RotationType.RotateOnBeginning);
            
            Context.AnimalAgent.enabled = false;
            Context.AnimalAgent.enabled = true;
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            
            _distanceBetweenPlayerAndAnimal = Vector3.Distance(Context.AnimalTransform.position, Context.MainCharacterTransform.position);
            Context.AnimalTransform.LookAt(Context.MainCharacterTransform);
        }

        public override void OnStateExit()
        {
            var randomIndex = Random.Range(0, Context.PossibleFollowTargets.Length);
            var randomTarget = Context.PossibleFollowTargets[randomIndex];
            Context.CurrentTarget = randomTarget;
        }

        public override bool TrySwitchState(out AnimalSubState<UncontrolledSubStatesFactory> newAnimalState)
        {
            newAnimalState = null;

            if (_distanceBetweenPlayerAndAnimal > Context.FollowWalkDistance)
                newAnimalState = Factory.Walk();
            else if (_distanceBetweenPlayerAndAnimal < Context.WalkBackDistance) newAnimalState = Factory.WalkBack();
            else if (StateTimer > Context.WaitTime) newAnimalState = Factory.Entertain();

            return newAnimalState != null;
        }
    }
}