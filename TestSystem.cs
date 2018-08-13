using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace FirstTrial01
{
    struct SomeValue : IComponentData
    {
        public float f ;
    }

    public class TestSystem : ComponentSystem
    {
        
        struct Data
        {
            public readonly int Length;

            [ReadOnly] public ComponentDataArray <SomeValue> a_f ;
        }

        // [Inject] private Data m_Data;
        private Data m_Data ;

        protected override void OnUpdate ()
        {
            Debug.Log ("ini2") ;

            var dt = Time.deltaTime;

            for (int index = 0; index < m_Data.Length; ++index)
            // foreach (var entity in GetEntities<Data> () ) // hybrid
            {                
                var someValue = m_Data.a_f [index].f ;
            }
        }
    }
}