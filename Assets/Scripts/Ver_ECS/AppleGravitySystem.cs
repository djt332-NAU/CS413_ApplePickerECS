using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct AppleGravitySystem : ISystem
{   
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (transform, properties) in 
                SystemAPI.Query<RefRW<LocalTransform>,
                RefRW<AppleProperties>>())
        {
            transform.ValueRW.Position.y -= properties.ValueRW.AppleFallSpeed;
        }
    }
}
