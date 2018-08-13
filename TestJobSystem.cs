/* Based on TwoStick example
 * 
 */

using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

namespace FirstTrial01
{
    public class RemoveDeadBarrier : BarrierSystem
    {
    }

    public struct Health : IComponentData
    {
        public float f ;
    }

    public class TestJobSystem : JobComponentSystem
    {
        struct Data
        {
            [ReadOnly] public EntityArray a_entities;
            //[ReadOnly] public ComponentDataArray<Health> Health;
            [ReadOnly] public ComponentDataArray <Health> a_health ;
        }

        [Inject] private Data data ;               
        
        [Inject] private RemoveDeadBarrier m_RemoveDeadBarrier ;

        /// <summary>
        /// Execute Jobs
        /// </summary>
        // [BurstCompile]
        struct RemoveReadJob : IJob
        // struct CollisionJob : IJobParallelFor // example of job parallel for
        {
            public bool isBool;
            [ReadOnly] public EntityArray a_entities;
            [ReadOnly] public ComponentDataArray <Health> a_health;

            public EntityCommandBuffer commandsBuffer ;

            public void Execute()
            {
                for (int i = 0; i < a_entities.Length; ++i )
                {
                    if ( a_health [i].f <= 0.0f || isBool )
                    {
                        commandsBuffer.DestroyEntity ( a_entities [i] ) ;
                    }
                }
                               
            }
            
            
        }

        protected override JobHandle OnUpdate ( JobHandle inputDeps )
        {
            return new RemoveReadJob
            {
                isBool = true,
                a_entities = data.a_entities,
                a_health = data.a_health,
                commandsBuffer = m_RemoveDeadBarrier.CreateCommandBuffer (),
            }.Schedule(inputDeps) ;

        }
    }

}