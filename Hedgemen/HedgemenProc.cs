using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Hgm.Base;
using Hgm.Ecs;
using Hgm.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using File = Hgm.IO.File;

namespace Hgm;

public sealed class HedgemenProc : Game, IHedgemen
{
	private readonly GraphicsDeviceManager _manager;
	private SpriteBatch _spriteBatch;

	public HedgemenProc()
	{
		_manager = new GraphicsDeviceManager(this);
	}

	protected override void Initialize()
	{
		Hedgemen.RegisterAssemblies(typeof(HedgemenProc), typeof(object));
		_spriteBatch = new SpriteBatch(_manager.GraphicsDevice);
	}

	protected override void Update(GameTime gameTime)
	{
		if (Keyboard.GetState().IsKeyDown(Keys.Escape))
			Exit();
	}

	protected override void Draw(GameTime gameTime)
	{
		_manager.GraphicsDevice.Clear(Color.CornflowerBlue);
		_spriteBatch.Begin();
		_spriteBatch.End();
		base.Draw(gameTime);
	}
}