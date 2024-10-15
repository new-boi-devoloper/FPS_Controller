using Zenject;
using UnityEngine;
using System.Collections;
using _Source.MyFPSController;
using UnityEngine.Serialization;

public class PlayerBaseInstaller : MonoInstaller
{
    [SerializeField] private InputListener inputListener;
    [SerializeField] private Player player;
    [SerializeField] private Camera playerCamera;

    public override void InstallBindings()
    {
        Container.Bind<PlayerControls>().AsSingle().NonLazy();
        // как сделать так чтобы был playerControls.Disable()
        
        Container.Bind<PlayerInvoker>().AsSingle();
        Container.Bind<PlayerMovement>().AsSingle();
        Container.Bind<InputListener>().FromInstance(inputListener).AsSingle();
        Container.Bind<Player>().FromInstance(player).AsSingle();
        Container.Bind<Camera>().FromInstance(playerCamera).AsSingle();
        Debug.Log(
            $"All Binded: PlayerControls={Container.HasBinding<PlayerControls>()}, PlayerInvoker={Container.HasBinding<PlayerInvoker>()}, PlayerMovement={Container.HasBinding<PlayerMovement>()}, InputListener={Container.HasBinding<InputListener>()}, Player={Container.HasBinding<Player>()}, Camera={Container.HasBinding<Camera>()}");
    }
    
    
}