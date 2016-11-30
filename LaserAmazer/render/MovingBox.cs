namespace LaserAmazer.render
{

    public class MovingBox
    {

        private Model model;
        private int distance = 0, maxDistance;
        private float xVelocity = 1f, yVelocity = 0f;

        public MovingBox(float x, float y, int maxDistance, float xVelocity, float yVelocity)
        {
            this.model = CreateModel.CreateBox(x, y);
            this.maxDistance = maxDistance;
            this.xVelocity = xVelocity;
            this.yVelocity = yVelocity;
        }

        public MovingBox(float x, float y, int maxDistance, int distance, float xVelocity, float yVelocity)
        {
            this.model = CreateModel.CreateBox(x, y);
            this.maxDistance = maxDistance;
            this.distance = distance;
            this.xVelocity = xVelocity;
            this.yVelocity = yVelocity;
        }

        public void LogicLoop()
        {
            if (distance < maxDistance)
            {
                model.Move(0.05f * xVelocity, 0.05f * yVelocity, 0f); // Moves the box back and forth
                distance++;
            }
            else
            {
                distance = 0;
                xVelocity *= -1f;
                yVelocity *= -1f;
            }
        }

        public void Rotate(float angle)
        {
            model.Rotate(angle);
        }

    }
}