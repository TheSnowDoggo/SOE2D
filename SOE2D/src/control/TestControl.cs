namespace Bad2D;

public class TestControl : Control
{
    public TriangleMesh Mesh { get; set; }
    
    public override void Draw()
    {
        Mesh.Draw();
    }
}