using Silk.NET.Maths;

namespace L3_dlim2227__3
{
    internal class CameraDescriptor
    {
        private double DistanceToOrigin  = 1;

        private const double DistanceScaleFactor = 1.1;

        private const double AngleChangeStepSize = Math.PI / 180 * 5;
        
        private const float MoveSpeed = 0.1f;

        private float xTarget = 0.0f;
        private float yTarget = 0.0f;
        private float zTarget = 0.0f;
        

        public Vector3D<float> Position
        {
            get;
            private set;
        } = new Vector3D<float>(4.0f, 0.0f, 0.0f);



        /// <summary>
        /// Gets the up vector of the camera.
        /// </summary>
        public Vector3D<float> UpVector
        {
            get
            {
                return new Vector3D<float>(0.0f, 1.0f, 0.0f);
            }
        }

        /// <summary>
        /// Gets the target point of the camera view.
        /// </summary>
        public Vector3D<float> Target { get; private set; } = new Vector3D<float>(0.0f, 0.0f, 0.0f);


        /// <summary>
        /// Gets the forward vector of the camera.
        /// </summary>
        public Vector3D<float> ForwardVector
        {
            get
            {
                return Target - Position;
            }
        }
       
        
        public void MoveForward()
        {
            Position += ForwardVector * MoveSpeed;
            Target += ForwardVector * MoveSpeed;
        }

        public void MoveBackward()
        {
            Position -= ForwardVector * MoveSpeed;
            Target -= ForwardVector * MoveSpeed;
        }

        public void MoveRight()
        {
            Position -= Vector3D.Cross(UpVector, ForwardVector) * MoveSpeed;
            Target -= Vector3D.Cross(UpVector, ForwardVector) * MoveSpeed;
        }

        public void MoveLeft()
        {
            Position += Vector3D.Cross(UpVector, ForwardVector) * MoveSpeed;
            Target += Vector3D.Cross(UpVector, ForwardVector) * MoveSpeed;
        }

        public void RotateLeft()
        {
            var rotate = Matrix4X4.CreateFromAxisAngle(UpVector, (float)AngleChangeStepSize);
            var forward = Vector3D.Normalize(Target - Position);
            forward = Vector3D.Transform(forward, rotate);
            Target = forward + Position;
        }
        
        public void RotateRight()
        {
            var rotate = Matrix4X4.CreateFromAxisAngle(UpVector, -(float)AngleChangeStepSize);
            var forward = Vector3D.Normalize(Target - Position);
            forward = Vector3D.Transform(forward, rotate);
            Target = forward + Position;
        }

        public void RotateUp()
        {
            var right = Vector3D.Cross(UpVector, Target - Position);
            var normRight = Vector3D.Normalize(right);
            var rotate = Matrix4X4.CreateFromAxisAngle(normRight, -(float)AngleChangeStepSize);

            var forward = Vector3D.Normalize(Target - Position);
            forward = Vector3D.Transform(forward, rotate);

            Target = forward + Position;
        }

        public void RotateDown()
        {
            var right = Vector3D.Cross(UpVector, Target - Position);
            var normRight = Vector3D.Normalize(right);

            var rotate = Matrix4X4.CreateFromAxisAngle(normRight, (float)AngleChangeStepSize);

            var forward = Vector3D.Normalize(Target - Position);
            forward = Vector3D.Transform(forward, rotate);

            Target = forward + Position;
        }

        private static Vector3D<float> GetPointFromAngles(double distanceToOrigin, double angleToMinZYPlane, double angleToMinZXPlane)
        {
            var x = distanceToOrigin * Math.Sin(angleToMinZYPlane) * Math.Cos(angleToMinZXPlane);
            var z = distanceToOrigin * Math.Cos(angleToMinZYPlane) * Math.Cos(angleToMinZXPlane);
            var y = distanceToOrigin * Math.Sin(angleToMinZXPlane);

            return new Vector3D<float>((float)x, (float)y, (float)z);
        }
    }
}