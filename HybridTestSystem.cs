/* Based on Hybrid TwoStick example
 * 
 */

using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace FirstTrial01
{
    public class HybridTestSystem : ComponentSystem
    {
        public class Value : MonoBehaviour
        {
            public float f ;
        }

        struct Data
        {
            public Value value ;
            //public Position2D Position;
            //public Heading2D Heading;
            //public MoveSpeed MoveSpeed;
        }

        protected override void OnUpdate()
        {
            Debug.Log ("ini2") ;

            var dt = Time.deltaTime;

            foreach (var entity in GetEntities <Data>() ) // hybrid
            {
                var pos = entity.value.f + dt ;
            }
        }
    }
}