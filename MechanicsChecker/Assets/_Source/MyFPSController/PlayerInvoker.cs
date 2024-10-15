using System;
using UnityEngine;
using Zenject;

namespace _Source.MyFPSController
{
    public class PlayerInvoker
    {
        //Dependencies
        private InputListener _inputListener;
        private PlayerMovement _playerMovement;
        private Player _player;
        private Camera _camera;

        [Inject]
        public PlayerInvoker(InputListener inputListener, PlayerMovement playerMovement, Player player, Camera camera)
        {
            _inputListener = inputListener;
            _playerMovement = playerMovement;
            _player = player;
            _camera = camera;

            SubscribeToEvents();
            Debug.Log(
                $"PlayerInvoker Initialised: InputListener={inputListener != null}, PlayerMovement={playerMovement != null}, Player={player != null}, Camera={camera != null}");
        }

        private void SubscribeToEvents()
        {
            _inputListener.OnMoveInput += InvokeMove;
            _inputListener.OnCameraLookInput += InvokeCameraMove;
        }

        public void Dispose()
        {
            _inputListener.OnMoveInput -= InvokeMove;
            _inputListener.OnCameraLookInput -= InvokeCameraMove;
        }
        
        //
        // public void HandleInput(Vector3 playerMovement)
        // {
        //     InvokeMove(playerMovement);
        // }

        private void InvokeMove(Vector2 positionToMove)
        {
            _playerMovement.Move(_player.PlayerRb, _player.PlayerSpeed, positionToMove, _camera);
            Debug.Log($"Move Values Applied: {positionToMove}");
        }


        private void InvokeCameraMove(float vertical, float horizontal)
        {
            _playerMovement.MoveCamera(_player.PlayerRb, _camera, vertical, horizontal,
                _inputListener.BottomCameraLimit, _inputListener.TopCameraLimit);
            Debug.Log($"Camera Move Values Applied: {horizontal}");
        }
    }
}