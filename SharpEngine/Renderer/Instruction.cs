using System.Collections.Generic;

namespace SharpEngine.Renderer;

public struct Instruction
{
    public InstructionType Type;
    public InstructionDestination Destination;
    public int ZLayer;
    public List<object> Parameters;
}