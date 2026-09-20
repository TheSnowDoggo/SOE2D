namespace Bad2D;

public class SpriteControl : Control
{
    public Mesh2D Mesh { get; set; }

    public override bool IsDrawable => Mesh != null;

    public override void Draw()
    {
        Mesh.Draw();
    }
}