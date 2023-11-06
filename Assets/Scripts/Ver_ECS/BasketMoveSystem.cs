using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct BasketMoveSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<BasketProperties>();
    }
    
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (transform, properties) in 
                SystemAPI.Query<RefRW<LocalTransform>,
                RefRW<BasketProperties>>())
        {
            Vector3 mousePos2D = Input.mousePosition;
            mousePos2D.z = -Camera.main.transform.position.z;
            Vector3 mousePos3D = Camera.main.ScreenToWorldPoint( mousePos2D );
            Vector3 pos = transform.ValueRW.Position;
            pos.x = mousePos3D.x;
            
            transform.ValueRW.Position = pos;
        }
    }
}
