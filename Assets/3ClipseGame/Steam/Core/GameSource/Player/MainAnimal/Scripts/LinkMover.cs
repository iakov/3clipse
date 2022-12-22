using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace _3ClipseGame.Steam.Core.GameSource.Player.MainAnimal.Scripts
{

    [RequireComponent(typeof(NavMeshAgent))]
    public class LinkMover : MonoBehaviour
    {
        #region PrivateFields

        [SerializeField] private OffMeshLinkMoveMethod _method = OffMeshLinkMoveMethod.Parabola;
        [SerializeField] private AnimationCurve _curve = new();

        #endregion

        #region MonoBehaviourMethods

        private IEnumerator Start()
        {
            var agent = GetComponent<NavMeshAgent>();
            agent.autoTraverseOffMeshLink = false;
            
            while (gameObject.activeSelf)
            {
                if (!agent.isOnOffMeshLink) yield return null;
                switch (_method)
                {
                    case OffMeshLinkMoveMethod.NormalSpeed:
                        yield return StartCoroutine(StaticSpeedMove(agent));
                        break;
                    case OffMeshLinkMoveMethod.Parabola:
                        yield return StartCoroutine(ParabolaMove(agent, 2.0f, 0.5f));
                        break;
                    case OffMeshLinkMoveMethod.Curve:
                        yield return StartCoroutine(CustomCurveMove(agent, 0.5f));
                        break;
                    case OffMeshLinkMoveMethod.Teleport:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                agent.CompleteOffMeshLink();
            }
        }

        #endregion

        #region MoveMethods

        private IEnumerator StaticSpeedMove(NavMeshAgent agent)
        {
            var data = agent.currentOffMeshLinkData;
            var endPos = data.endPos + Vector3.up * agent.baseOffset;
            
            while (agent.transform.position != endPos)
            {
                agent.transform.position = Vector3.MoveTowards(agent.transform.position, endPos, agent.speed * Time.deltaTime);
                yield return null;
            }
        }

        private IEnumerator ParabolaMove(NavMeshAgent agent, float height, float duration)
        {
            var data = agent.currentOffMeshLinkData;
            var startPos = agent.transform.position;
            var endPos = data.endPos + Vector3.up * agent.baseOffset;
            var normalizedTime = 0.0f;
            
            while (normalizedTime < 1.0f)
            {
                var yOffset = height * 4.0f * (normalizedTime - normalizedTime * normalizedTime);
                agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
                normalizedTime += Time.deltaTime / duration;
                yield return null;
            }
        }

        private IEnumerator CustomCurveMove(NavMeshAgent agent, float duration)
        {
            var data = agent.currentOffMeshLinkData;
            var startPos = agent.transform.position;
            var endPos = data.endPos + Vector3.up * agent.baseOffset;
            var normalizedTime = 0.0f;
            
            while (normalizedTime < 1.0f)
            {
                var yOffset = _curve.Evaluate(normalizedTime);
                agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
                normalizedTime += Time.deltaTime / duration;
                transform.LookAt(endPos);
                yield return null;
            }
        }

        #endregion

        #region PrivateStructs

        private enum OffMeshLinkMoveMethod
        {
            Teleport,
            NormalSpeed,
            Parabola,
            Curve
        }

        #endregion
    }
}