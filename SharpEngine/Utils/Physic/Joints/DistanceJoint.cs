﻿using SharpEngine.Components;
using SharpEngine.Entities;
using SharpEngine.Utils.Math;
using tainicom.Aether.Physics2D.Dynamics;

namespace SharpEngine.Utils.Physic.Joints;

public class DistanceJoint: Joint
{
    public float Length { get; set; }
    public float Frequency { get; set; }
    public float DampingRatio { get; set; }

    public DistanceJoint(Entity target, Vec2 fromPosition = null, Vec2 targetPosition = null,
        float length = -1, float frequency = -1, float dampingRatio = -1) : base(JointType.Distance, target, 
        fromPosition == null ? new Vec2(0): fromPosition, 
        targetPosition == null ? new Vec2(0) : targetPosition)
    {
        Length = length;
        Frequency = frequency;
        DampingRatio = dampingRatio;
    }

    }
}