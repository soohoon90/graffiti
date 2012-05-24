#region License and Copyright Notice
// Copyright (c) 2010 Ananth Balasubramaniam
// All rights reserved.
// 
// The contents of this file are made available under the terms of the
// Eclipse Public License v1.0 (the "License") which accompanies this
// distribution, and is available at the following URL:
// http://www.opensource.org/licenses/eclipse-1.0.php
// 
// Software distributed under the License is distributed on an "AS IS" basis,
// WITHOUT WARRANTY OF ANY KIND, either expressed or implied. See the License for
// the specific language governing rights and limitations under the License.
// 
// By using this software in any fashion, you are agreeing to be bound by the
// terms of the License.
#endregion

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Graffiti.Core.Brushes
{
    public interface ILayer: IUpdateable
    {
        Texture2D Texture { get; set; }
        int TexCoordChannel { get; set; }
        Color Color { get; set; }
        
        TextureAddressMode AddressU { get; set; }
        TextureAddressMode AddressV { get; set; }

        BlendState BlendState { get; set; }

        ILayerTransforms LayerTransforms { get; }

        Matrix CurrentLayerTransform { get; }
    }
}