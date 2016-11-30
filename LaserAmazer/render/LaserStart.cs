namespace LaserAmazer.render
{
    public class LaserStart : Model
    {

        private LaserWrapper laser;

        public LaserStart(float[] vertices, float[] tCoords, int[] indices, float xOffset, float yOffset, LaserWrapper laser) : base(vertices, tCoords, indices, xOffset, yOffset, 4, GameTexture.START)
        {
            this.laser = laser;
        }

        public new void Render()
        {
            base.Render();
            laser.Render();
        }

        public void Reflect()
        {
            laser.RunReflections();
        }

        public void SetLaser(LaserWrapper l)
        {
            this.laser = l;
        }

        public new void Rotate(float angle)
        {
            base.Rotate(angle);

            if (laser != null)
                laser.RotateStart(angle, base.xOffset, base.yOffset);
        }

    }
}