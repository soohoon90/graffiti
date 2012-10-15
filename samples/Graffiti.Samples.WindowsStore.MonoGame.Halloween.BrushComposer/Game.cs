﻿#region License and Copyright Notice
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

using Graffiti.Core.Animation;
using Graffiti.Core.Brushes;
using Graffiti.Core.Math;
using Graffiti.Core.Primitives;
using Graffiti.Core.Rendering;
using Graffiti.Math;
using Graffiti.Core.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Graffiti.Core.Animation.Constants;

namespace Graffiti.Samples.WindowsStore.MonoGame.Halloween.BrushComposer
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        private readonly GraphicsDeviceManager graphics;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
        }

        private IRenderer _renderer;
        private Quad _quad;

        protected override void Initialize()
        {
            GraphicsDevice.Viewport = graphics.AdaptViewport(256, 256);

            base.Initialize();

            var halfWidth = GraphicsDevice.Viewport.Width / 2f;
            var halfHeight = GraphicsDevice.Viewport.Height / 2f;

            _renderer = Renderer.Create(
                GraphicsDevice, Features.MultiPass | Features.PreTransformed | Features.SingleChannelTexCoords,
                projection: Matrix.CreateOrthographicOffCenter(-halfWidth, halfWidth, halfHeight, -halfHeight, 0, 1));

            var brush = new Brush
            {
                new Layer
                {
                    Texture = Content.Load<Texture2D>("Content/Bat body"),
                    AlphaTestEnable = true,
                    AlphaFunction = CompareFunction.Greater,
                    ReferenceAlpha = 128,
                    Transform = TranslateTransform.Procedural(t => new Vector3(0f, Functions.Sine(t, -0.1f, 0.2f, 0f, 1f), 0f))
                },
                new Layer
                {
                    Texture = Content.Load<Texture2D>("Content/Bat right wing"),
                    AddressU = TextureAddressMode.Clamp,
                    AddressV = TextureAddressMode.Clamp,
                    AlphaTestEnable = true,
                    AlphaFunction = CompareFunction.Greater,
                    ReferenceAlpha = 128,
                    Transform = new TransformGroup
                    {
                        TranslateTransform.Procedural(t => new Vector3(0f, Functions.Sine(t, -0.1f, 0.2f, 0f, 1f), 0f)),
                        (ConstantTransform)Matrix.CreateTranslation(new Vector3(-0.47f, -0.558f, 0f)),
                        RotateTransform.Procedural(t => Quaternion.CreateFromAxisAngle(Vector3.UnitZ, Functions.Sine(t, -0.3f, 0.6f, 0f, 0.5f))),
                        (ConstantTransform)Matrix.CreateTranslation(new Vector3(0.47f, 0.558f, 0f))
                    }
                },
                new Layer
                {
                    Texture = Content.Load<Texture2D>("Content/Bat left wing"),
                    AddressU = TextureAddressMode.Clamp,
                    AddressV = TextureAddressMode.Clamp,
                    AlphaTestEnable = true,
                    AlphaFunction = CompareFunction.Greater,
                    ReferenceAlpha = 128,
                    Transform = new TransformGroup
                    {
                        TranslateTransform.Procedural(t => new Vector3(0f, Functions.Sine(t, -0.1f, 0.2f, 0f, 1f), 0f)),
                        (ConstantTransform)Matrix.CreateTranslation(new Vector3(-0.492f, -0.558f, 0f)),
                        RotateTransform.Procedural(t => Quaternion.CreateFromAxisAngle(Vector3.UnitZ, -Functions.Sine(t, -0.3f, 0.6f, 0f, 0.5f))),
                        (ConstantTransform)Matrix.CreateTranslation(new Vector3(0.492f, 0.558f, 0f))
                    }
                }
            };

            _quad = new Quad
            {
                Transform = (ConstantMatrix)Matrix.CreateScale(256f),
                Brush = brush
            };
        }

        protected override void LoadContent()
        {
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        private float _time;
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            _time += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            _quad.Brush.Update(_time);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Purple);

            _quad.Render(_renderer, Matrix.Identity);
            _renderer.Flush();

            base.Draw(gameTime);
        }
    }
}
