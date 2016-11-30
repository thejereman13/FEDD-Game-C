namespace LaserAmazer.render
{
    public class Wall : Model
    {

        public Wall(float[] vertices, float[] tCoords, int[] indices) : this(vertices, tCoords, indices, 4)
        {
        }

        public Wall(float[] vertices, float[] tCoords, int[] indices, int sideNum) : base(vertices, tCoords, indices, sideNum, "bgtile.png")
        {
        }

    }
}