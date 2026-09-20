namespace Bad2D;

[Flags]
public enum Anchor
{
	None    = 0,
	Left    = 1 << 0,
	Right   = 1 << 1,
	CenterH = Left | Right,
	Top     = 1 << 2,
	Bottom  = 1 << 3,
	CenterV = Top | Bottom,
	Center  = CenterH | CenterV,
}