using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial struct AppleInteractSystem : ISystem
{
    const float basketXScale = 4;
    const float basketYScale = 1;
    private bool appleMissed; 
    private int numLives;
    
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        numLives = 3;
    }
    
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecbSingleton = 
            SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
        
        appleMissed = false;
            
        foreach (var (appTransform, appProperties, appEntity) in 
                 SystemAPI.Query<RefRW<LocalTransform>,
                 RefRW<AppleProperties>>().WithEntityAccess())
        {   
            if (appTransform.ValueRO.Position.y <= appProperties.ValueRW.AppleBoundY)
            {
                appleMissed = true;
                
                // Destroy one basket
                foreach (var (properties, entity) in 
                         SystemAPI.Query<RefRW<BasketProperties>>().WithEntityAccess())
                {
                    ecb.DestroyEntity(entity);
                    break;
                }
                
                numLives--;
                
                if (numLives == 0)
                {
                    SceneManager.LoadScene("_Scene_Start");
                }
                
                break;
            }
            
            foreach (var (basTransform, basProperties) in 
                     SystemAPI.Query<RefRW<LocalTransform>,
                     RefRW<BasketProperties>>())
            {
                if (appleBasketCross(appTransform.ValueRO.Position, 
                                     basTransform.ValueRO.Position))
                {
                    ecb.DestroyEntity(appEntity);
                    // TODO: Update Score and High Score
                }
            }
        }
        
        if (appleMissed)
        {
            foreach (var (appTransform, appProperties, appEntity) in 
                     SystemAPI.Query<RefRW<LocalTransform>,
                     RefRW<AppleProperties>>().WithEntityAccess())
            {
                ecb.DestroyEntity(appEntity);
            }
        }
    }
    
    public bool appleBasketCross(Vector3 applePos, Vector3 basketPos)
    {
        return ((applePos.x <= basketPos.x + basketXScale && 
                 applePos.x >= basketPos.x - basketXScale) && 
                (applePos.y <= basketPos.y + basketYScale && 
                 applePos.y >= basketPos.y - basketYScale));
    }
}
