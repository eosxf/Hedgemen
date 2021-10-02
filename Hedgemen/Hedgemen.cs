using System;
using System.Diagnostics;
using Hgm.Ecs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hgm
{
    public sealed class Hedgemen : Game, IHedgemen
    {
        private GraphicsDeviceManager manager;
        private SpriteBatch spriteBatch;
        private Globals globals;

        public Globals Globals => globals;

        public Hedgemen()
        {
            new GameObject().Do();
            Console.WriteLine(new StackTrace());
            manager = new GraphicsDeviceManager(this);
        }

        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(manager.GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            System.GC.Collect();
        }

        protected override void Draw(GameTime gameTime)
        {
            manager.GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}