using System.Collections.Generic;
using UnityEngine;

namespace _3ClipseGame.Steam.Entities.Player.Scripts
{
    public class PlayerMover : MonoBehaviour
    {
        #region Initialization

        private CharacterController _playerController;
        private readonly List<Move> _movesList = new List<Move>();

        #endregion

        #region PublicMethods

        public void StartWork()
        {
            _playerController = GetComponent<CharacterController>();
        }

        public void ChangeMove(MoveType type, Vector3 newMove)
        {
            foreach (var move in _movesList)
            {
                if (move.MoveType != type) continue;
                move.MoveVector = newMove;
                return;
            }
            
            _movesList.Add(new Move(type, newMove));
        }

        public void UpdateWork()
        {
            var resultMove = Vector3.zero;
            foreach (var move in _movesList) resultMove += move.MoveVector;
            _playerController.Move(resultMove);
        }

        #endregion

        #region PrivateClasses

        private class Move
        {
            public Move(MoveType type, Vector3 moveVector)
            {
                MoveType = type;
                MoveVector = moveVector;
            }
            
            public readonly MoveType MoveType;
            public Vector3 MoveVector;
        }

        #endregion
    }
    
    public enum MoveType
    {
        GravityMove, StateMove
    }
}
