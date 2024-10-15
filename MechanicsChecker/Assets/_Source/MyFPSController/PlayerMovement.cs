using UnityEngine;

namespace _Source.MyFPSController
{
    public class PlayerMovement
    {
        public void Move(Rigidbody playerRb, float playerSpeed, Vector2 moveInput, Camera playerCamera)
        {
            // Преобразуем ввод в вектор движения
            Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);

            // Преобразуем локальное направление движения в мировое направление, учитывая направление камеры
            moveDirection = playerCamera.transform.TransformDirection(moveDirection);
            moveDirection.y = 0; // Убедитесь, что движение происходит только по горизонтали

            // Применяем скорость к Rigidbody
            playerRb.velocity = moveDirection.normalized * playerSpeed;
        }

        public void MoveCamera(Rigidbody playerRb, Camera playerCamera, float vertical, float horizontal, float BottomLimit, float TopLimit)
        {
            // Поворот игрока вокруг вертикальной оси
            playerRb.transform.Rotate(Vector3.up * horizontal);

            // Поворот камеры вокруг горизонтальной оси
            float _xRotation = playerCamera.transform.localEulerAngles.x;
            _xRotation -= vertical;
            _xRotation = ClampAngle(_xRotation, BottomLimit, TopLimit);

            playerCamera.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        }

        private static float ClampAngle(float angle, float min, float max)
        {
            if (angle > 180f) angle -= 360f;
            if (angle < -180f) angle += 360f;
            return Mathf.Clamp(angle, min, max);
        }
    }
}