using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct BasketSpawnerSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<AppleTreeProperties>();
    }
    
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false;
        
        foreach (var (transform, properties) in 
                SystemAPI.Query<RefRW<LocalTransform>,
                RefRW<AppleTreeProperties>>())
        {
            for (int i = 0; i < properties.ValueRW.NumBaskets; i++)
            {
                Entity curBasket = state.EntityManager.Instantiate(
                                       properties.ValueRW.BasketPrefab);
                                       
                Vector3 pos = Vector3.zero;
                pos.y = properties.ValueRW.BasketBottomY + 
                        (properties.ValueRW.BasketSpacingY * i);
                
                state.EntityManager.SetComponentData(curBasket, new LocalTransform
                {
                    Position = pos,
                    Rotation = quaternion.identity,
                    Scale = 1f
                });
            }
        }
    }
}
