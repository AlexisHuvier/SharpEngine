﻿using System;

namespace SharpEngine.Utils.Widget;

/// <summary>
/// Style for Label
/// </summary>
[Flags]
public enum LabelStyle
{
    /// <summary>
    /// None Style
    /// </summary>
    None = 0,
    
    /// <summary>
    /// Underline Style
    /// </summary>
    Underline = 1,
    
    /// <summary>
    /// Strike Style
    /// </summary>
    Strike = 2,
}