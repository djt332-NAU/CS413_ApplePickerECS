using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct AppleSpawnerSystem : ISystem
{
    private float sinceLastDrop;
    
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        sinceLastDrop = 0;
    }
    
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        sinceLastDrop += Time.deltaTime;
        
        foreach (var (transform, properties) in 
                SystemAPI.Query<RefRW<LocalTransform>,
                RefRW<AppleTreeProperties>>())
        {
            if (sinceLastDrop > properties.ValueRW.AppleDropDelay)
            {
                Entity curApple = state.EntityManager.Instantiate(
                                     properties.ValueRO.ApplePrefab);
                state.EntityManager.SetComponentData(curApple, new LocalTransform
                {
                    Position = transform.ValueRW.Position,
                    Rotation = quaternion.identity,
                    Scale = 1f
                });
                
                sinceLastDrop = 0;
            }
        }
    }
}
