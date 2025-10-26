using Silk.NET.Maths;
using Silk.NET.OpenGL;

namespace L3_dlim2227__3
{
    internal class CubeArrangementModel
    {
        public bool AnimationEnabeld { get; set; } = false;
        private bool roataionEnabeld = false;
        private double Time { get; set; } = 0;
        public double CenterCubeScale { get; private set; } = 1;

        private float actualAngle = 0;
        private static float targetAngle = (float)101;
        private const float targetAngleShufle = (float)70;
        private const float targetAngleBasic = (float)101;
        private const float rotationStepShufle = (float)Math.PI / 10;
        private static float rotationStepSimple = (float)Math.PI / 100;
        private static float rotationStep = (float)Math.PI / 100;
        private int rotationCounter = 0;

        
        private Direction rotationForZ1 = Direction.Nothing;
        private Direction rotationForZ2 = Direction.Nothing;
        private Direction rotationForZ3 = Direction.Nothing;
        private Direction rotationForY1 = Direction.Nothing;
        private Direction rotationForY2 = Direction.Nothing;
        private Direction rotationForY3 = Direction.Nothing;
        private Direction rotationForX1 = Direction.Nothing;
        private Direction rotationForX2 = Direction.Nothing;
        private Direction rotationForX3 = Direction.Nothing;
        
        public double DiamondCubeAngleOwnRevolution { get; private set; } = 0;

        public enum Axis
        {
            X,
            Y,
            Z
        }
        
        public enum Direction
        {
            Forward,
            Backward,
            Nothing
        }
        

        public double DiamondCubeAngleRevolutionOnGlobalY { get; private set; } = 0;

        public GlCube[] cubes = new GlCube[27];

        internal void AdvanceTime(double deltaTime)
        {
            if (rotationCounter != 0 && IsSolved() && !roataionEnabeld)
            {
                Time += deltaTime;

                CenterCubeScale = 1 + 0.2 * Math.Sin(1.5 * Time);

                DiamondCubeAngleOwnRevolution = Time * 10;
            
                DiamondCubeAngleRevolutionOnGlobalY = -Time;
            }
            else
            {
                if (!roataionEnabeld)
                {
                    return;
                }
                if (rotationForZ1 != Direction.Nothing)
                {
                    RotateLayer(0, rotationForZ1 == Direction.Forward, Axis.Z);
                    if (actualAngle >= targetAngle)
                    {
                        rotationForZ1 = Direction.Nothing;
                        actualAngle = 0;
                        roataionEnabeld = false;
                        rotationCounter++;
                    }
                    else
                    {
                        actualAngle += rotationStep * 180 / (float)Math.PI;
                    }
                }

                if (rotationForZ2 != Direction.Nothing)
                {
                    RotateLayer(1, rotationForZ2 == Direction.Forward, Axis.Z);
                    if (actualAngle >= targetAngle)
                    {
                        rotationForZ2 = Direction.Nothing;
                        actualAngle = 0;
                        roataionEnabeld = false;
                        rotationCounter++;
                    }
                    else
                    {
                        actualAngle += rotationStep * 180 / (float)Math.PI;  
                    }

                }

                if (rotationForZ3 != Direction.Nothing)
                {
                    RotateLayer(2, rotationForZ3 == Direction.Forward, Axis.Z);
                    if (actualAngle >= targetAngle)
                    {
                        rotationForZ3 = Direction.Nothing;
                        actualAngle = 0;
                        roataionEnabeld = false;
                        rotationCounter++;
                    }
                    else
                    {
                        actualAngle += rotationStep * 180 / (float)Math.PI;
                    }
                }

                if (rotationForY1 != Direction.Nothing)
                {
                    RotateLayer(0, rotationForY1 == Direction.Forward, Axis.Y);
                    if (actualAngle >= targetAngle)
                    {
                        rotationForY1 = Direction.Nothing;
                        actualAngle = 0;
                        roataionEnabeld = false;
                        rotationCounter++;
                    }
                    else
                    {
                        actualAngle += rotationStep * 180 / (float)Math.PI;
                    }
                }

                if (rotationForY2 != Direction.Nothing)
                {
                    RotateLayer(1, rotationForY2 == Direction.Forward, Axis.Y);
                    if (actualAngle >= targetAngle)
                    {
                        rotationForY2 = Direction.Nothing;
                        actualAngle = 0;
                        roataionEnabeld = false;
                        rotationCounter++;
                    }
                    else
                    {
                        actualAngle += rotationStep * 180 / (float)Math.PI;    
                    }
                }

                if (rotationForY3 != Direction.Nothing)
                {
                    RotateLayer(2, rotationForY3 == Direction.Forward, Axis.Y);
                    if (actualAngle >= targetAngle)
                    {
                        rotationForY3 = Direction.Nothing;
                        actualAngle = 0;
                        roataionEnabeld = false;
                        rotationCounter++;
                    }
                    else
                    {
                        actualAngle += rotationStep * 180 / (float)Math.PI; 
                    }
                }

                if (rotationForX1 != Direction.Nothing)
                {
                    RotateLayer(0, rotationForX1 == Direction.Forward, Axis.X);
                    if (actualAngle >= targetAngle)
                    {
                        rotationForX1 = Direction.Nothing;
                        actualAngle = 0;
                        roataionEnabeld = false;
                        rotationCounter++;
                    }
                    else
                    {
                        actualAngle += rotationStep * 180 / (float)Math.PI;
                    }
                }

                if (rotationForX2 != Direction.Nothing)
                {
                    RotateLayer(1, rotationForX2 == Direction.Forward, Axis.X);
                    if (actualAngle >= targetAngle)
                    {
                        rotationForX2 = Direction.Nothing;
                        actualAngle = 0;
                        roataionEnabeld = false;
                        rotationCounter++;
                    }
                    else
                    {
                        actualAngle += rotationStep * 180 / (float)Math.PI;
                    }
                }

                if (rotationForX3 != Direction.Nothing)
                {
                    RotateLayer(2, rotationForX3 == Direction.Forward, Axis.X);
                    if (actualAngle >= targetAngle)
                    {
                        rotationForX3 = Direction.Nothing;
                        actualAngle = 0;
                        roataionEnabeld = false;
                        rotationCounter++;
                    }
                    else
                    {
                        actualAngle += rotationStep * 180 / (float)Math.PI;
                    }
                }
            }
        }

        public void SetCubes(GlCube[] cubes)
        {
            this.cubes = cubes;
        }

        public async Task RotateZ1Forward(Direction direction)
        {
            if (!roataionEnabeld)
            {
                roataionEnabeld = true;
                if (rotationForZ1 == Direction.Nothing)
                {
                    setNewPosition(0, direction, Axis.Z);
                    rotationForZ1 = direction;
                }
                await WaitForRotationCompletion();
            }
        }

        public async Task RotateZ2Forward(Direction direction)
        {
            if (!roataionEnabeld)
            {
                roataionEnabeld = true;
                if (rotationForZ2 == Direction.Nothing)
                {
                    setNewPosition(1, direction, Axis.Z);
                    rotationForZ2 = direction;
                }
                await WaitForRotationCompletion();
            }
        }

        public async Task RotateZ3Forward(Direction direction)
        {
            if (!roataionEnabeld)
            {
                roataionEnabeld = true;
                if (rotationForZ3 == Direction.Nothing)
                {
                    setNewPosition(2, direction, Axis.Z);
                    rotationForZ3 = direction;
                }
                await WaitForRotationCompletion();
            }
        }

        public async Task RotateY1Forward(Direction direction)
        {
            if (!roataionEnabeld)
            {
                roataionEnabeld = true;
                if (rotationForY1 == Direction.Nothing)
                {
                    setNewPosition(0, direction, Axis.Y);
                    rotationForY1 = direction;
                }
                await WaitForRotationCompletion();
            }
        }

        public async Task RotateY2Forward(Direction direction)
        {
            if (!roataionEnabeld)
            {
                roataionEnabeld = true;
                if (rotationForY2 == Direction.Nothing)
                {
                    setNewPosition(1, direction, Axis.Y);
                    rotationForY2 = direction;
                }
                await WaitForRotationCompletion();
            }
        }

        public async Task RotateY3Forward(Direction direction)
        {
            if (!roataionEnabeld)
            {
                roataionEnabeld = true;
                if (rotationForY3 == Direction.Nothing)
                {
                    setNewPosition(2, direction, Axis.Y);
                    rotationForY3 = direction;
                }
                await WaitForRotationCompletion();
            }
        }

        public async Task RotateX1Forward(Direction direction)
        {
            if (!roataionEnabeld)
            {
                roataionEnabeld = true;
                if (rotationForX1 == Direction.Nothing)
                {
                    setNewPosition(0, direction, Axis.X);
                    rotationForX1 = direction;
                }
                await WaitForRotationCompletion();
            }
        }

        public async Task RotateX2Forward(Direction direction)
        {
            if (!roataionEnabeld)
            {
                roataionEnabeld = true;
                if (rotationForX2 == Direction.Nothing)
                {
                    setNewPosition(1, direction, Axis.X);
                    rotationForX2 = direction;
                }
                await WaitForRotationCompletion();
            }
        }

        public async Task RotateX3Forward(Direction direction)
        {
            if (!roataionEnabeld)
            {
                roataionEnabeld = true;
                if (rotationForX3 == Direction.Nothing)
                {
                    setNewPosition(2, direction, Axis.X);
                    rotationForX3 = direction;
                }
                await WaitForRotationCompletion();
            }
        }

        private void RotateLayer(int layer, bool clockwise, Axis axis)
        {
            foreach (var cube in cubes)
            {
                bool inLayer = false;
                switch (axis)
                {
                    case Axis.X:
                        // lapos 6, 7, 8
                        inLayer = cube.x == layer - 1;
                        break;
                    case Axis.Y:
                        // vízszintes 3, 4, 5
                        inLayer = cube.y == layer - 1;
                        break;
                    case Axis.Z:
                        // függőleges 0, 1, 2
                        inLayer = cube.z == layer - 1;
                        break;
                }

                if (inLayer)
                {
                    switch (axis)
                    {
                        case Axis.X:
                            cube.ApplyRotationX(clockwise ? rotationStep : -rotationStep);
                            actualAngle += rotationStep;
                            break;
                        case Axis.Y:
                            cube.ApplyRotationY(clockwise ? rotationStep : -rotationStep);
                            actualAngle += rotationStep;
                            break;
                        case Axis.Z:
                            cube.ApplyRotationZ(clockwise ? rotationStep : -rotationStep);
                            actualAngle += rotationStep;
                            break;
                    }
                }
            }
        }
        
        private void setNewPosition(int layer, Direction direction, Axis axis)
        {
            foreach (var cube in cubes)
            {
                bool inLayer = false;
                switch (axis)
                {
                    case Axis.X:
                        // lapos 6, 7, 8
                        inLayer = cube.x == layer - 1;
                        break;
                    case Axis.Y:
                        // vízszintes 3, 4, 5
                        inLayer = cube.y == layer - 1;
                        break;
                    case Axis.Z:
                        // függőleges 0, 1, 2
                        inLayer = cube.z == layer - 1;
                        break;
                }

                if (inLayer)
                {
                    if (direction == Direction.Forward)
                    {
                        switch (axis)
                        {
                            case Axis.X:
                                int xx = cube.x;
                                int yy = cube.y;
                                int zz = cube.z;
                                cube.y = yy * 0 - zz * 1;
                                cube.z = yy * 1 + zz * 0;
                                break;
                            case Axis.Y:
                                xx = cube.x;
                                zz = cube.z;
                                cube.x = xx * 0 + zz * 1;
                                cube.z = zz * 0 - xx * 1;
                                break;
                            case Axis.Z:
                                xx = cube.x;
                                yy = cube.y;
                                cube.x = xx * 0 - yy * 1;
                                cube.y = xx * 1 + yy * 0;
                                break;
                        }
                    }
                    else if (direction == Direction.Backward)
                    {
                        switch (axis)
                        {
                            case Axis.X:
                                int xx = cube.x;
                                int yy = cube.y;
                                int zz = cube.z;
                                cube.y = yy * 0 - zz * 1;
                                cube.z = yy * 1 + zz * 0; 
                                xx = cube.x;
                                yy = cube.y;
                                zz = cube.z;
                                cube.y = yy * 0 - zz * 1;
                                cube.z = yy * 1 + zz * 0;
                                xx = cube.x;
                                yy = cube.y;
                                zz = cube.z;
                                cube.y = yy * 0 - zz * 1;
                                cube.z = yy * 1 + zz * 0;
                                break;
                            case Axis.Y:
                                xx = cube.x;
                                zz = cube.z;
                                cube.x = xx * 0 + zz * 1;
                                cube.z = zz * 0 - xx * 1;
                                xx = cube.x;
                                zz = cube.z;
                                cube.x = xx * 0 + zz * 1;
                                cube.z = zz * 0 - xx * 1;
                                xx = cube.x;
                                zz = cube.z;
                                cube.x = xx * 0 + zz * 1;
                                cube.z = zz * 0 - xx * 1;
                                break;
                            case Axis.Z:
                                xx = cube.x;
                                yy = cube.y;
                                cube.x = xx * 0 - yy * 1;
                                cube.y = xx * 1 + yy * 0;
                                xx = cube.x;
                                yy = cube.y;
                                cube.x = xx * 0 - yy * 1;
                                cube.y = xx * 1 + yy * 0;
                                xx = cube.x;
                                yy = cube.y;
                                cube.x = xx * 0 - yy * 1;
                                cube.y = xx * 1 + yy * 0;
                                break;
                        }
                    }
                    
                }
            }
        }

        public async Task ShuffleAsync()
        { 
            setRotationSpeed(rotationStepShufle);
            setRotationTarget(targetAngleShufle);
            Random r = new Random();
            for (int i = 0; i < 30; i++)
            {
                int rotation = r.Next(1, 18);
                switch (rotation)
                {
                    case 1:
                        await RotateX1Forward(Direction.Forward);
                        break;
                    case 2:
                        await RotateX1Forward(Direction.Backward);
                        break;
                    case 3:
                        await RotateX2Forward(Direction.Forward);
                        break;
                    case 4:
                        await RotateX2Forward(Direction.Backward);
                        break;
                    case 5:
                        await RotateX3Forward(Direction.Forward);
                        break;
                    case 6:
                        await RotateX3Forward(Direction.Backward);
                        break;
                    case 7:
                        await RotateY1Forward(Direction.Forward);
                        break;
                    case 8:
                        await RotateY1Forward(Direction.Backward);
                        break;
                    case 9:
                        await RotateY2Forward(Direction.Forward);
                        break;
                    case 10:
                        await RotateY2Forward(Direction.Backward);
                        break;
                    case 11:
                        await RotateY3Forward(Direction.Forward);
                        break;
                    case 12:
                        await RotateY3Forward(Direction.Backward);
                        break;
                    case 13:
                        await RotateZ1Forward(Direction.Forward);
                        break;
                    case 14:
                        await RotateZ1Forward(Direction.Backward);
                        break;
                    case 15:
                        await RotateZ2Forward(Direction.Forward);
                        break;
                    case 16:
                        await RotateZ2Forward(Direction.Backward);
                        break;
                    case 17:
                        await RotateZ3Forward(Direction.Forward);
                        break;
                    case 18:
                        await RotateZ3Forward(Direction.Backward);
                        break;
                }
            }
            setRotationSpeed(rotationStepSimple);
            setRotationTarget(targetAngleBasic);
        }
        private async Task WaitForRotationCompletion()
        {
            while (roataionEnabeld)
            {
                await Task.Yield();
            }
        }

        private bool IsSolved()
        {
            int i = 0;
            for (int j = -1; j <= 1; j++)
            {
                for (int k = -1; k <= 1; k++)
                {
                    for (int l = -1; l <= 1; l++)
                    {
                        if (cubes[i].x == l && cubes[i].y == k && cubes[i].z == j)
                        {
                            i++;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void setRotationSpeed(float speed)
        {
            rotationStep = speed;
        }
        
        private void setRotationTarget(float target)
        {
            targetAngle = target;
        }
    }
}
