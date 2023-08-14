using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class Seeking : Action
    {
        public float randomRange;
        NavMeshAgent navMeshAgent;
        Vector3 destination;
        public override void OnStart()
        {
            navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
            float randomX = Random.Range(-randomRange, randomRange);
            float randomZ = Random.Range(-randomRange, randomRange);
            destination = new Vector3(randomX, 0, randomZ) + gameObject.transform.position;
        }

        public override TaskStatus OnUpdate()
        {
            if (gameObject.transform.position != destination && !navMeshAgent.hasPath)
            {
                navMeshAgent.SetDestination(destination);
                return TaskStatus.Running;
            }
            
            if (navMeshAgent.hasPath)
            {
                return TaskStatus.Running;
            }

            return TaskStatus.Success;
        }
    }

    public class LookingAround : Action
    {
        private float randomRange;
        public override void OnStart()
        {
            randomRange = Random.Range(0, 360);
        }

        public override TaskStatus OnUpdate()
        {
            gameObject.transform.Rotate(0, randomRange, 0);
            return TaskStatus.Success;
        }
    }
}

