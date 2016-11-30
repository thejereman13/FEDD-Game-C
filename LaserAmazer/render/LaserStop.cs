namespace LaserAmazer.render
{
    public class LaserStop : Model
    {

        public LaserStop(float[] vertices, float[] tCoords, int[] indices, float xOffset, float yOffset) : base(vertices, tCoords, indices, xOffset, yOffset, 4, GameTexture.FINISH)
        {
        }

        /**
         * Called by ReflectionCalculation once a laser has its destination set to this object
         */
        public void laserIntersection()
        {
            GameInstance.setState(State.LEVEL_COMPLETE);
        }

    }
}