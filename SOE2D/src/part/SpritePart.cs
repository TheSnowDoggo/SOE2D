namespace SOE2D;

public class SpritePart : Part
{
    public Mesh2D Mesh { get; set; }

    public override bool Drawable => Mesh != null;

    public override void Draw()
    {
        Mesh.Draw();
    }
}