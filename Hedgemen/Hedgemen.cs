using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Hgm.Ecs;
using Hgm.Ecs.Serialization;
using Hgm.Engine.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using File = Hgm.Engine.IO.File;

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
			manager = new GraphicsDeviceManager(this);
		}

		protected override void Initialize()
		{
			globals = new Globals();
			
			globals.RegisterAssembly(typeof(Hedgemen).Assembly);
			var obj = new GameObject();
			obj.AddPart(new CharacterSheet());

			var sheet = obj.GetPart<CharacterSheet>();
			sheet.Strength = 1025;
			sheet.Intelligence = 10;
			sheet.Charisma = 15;

			IFile file = new File("serialized_obj.bin");

			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(file.Open(FileMode.OpenOrCreate), obj.GetSerializedInfo());

			var serializedInfo = formatter.Deserialize(file.Open()) as SerializedInfo;
			if (serializedInfo is null) throw new Exception("Something went wrong with deserialization!");
			var obj2 = serializedInfo.ConstructObject<GameObject>();

			var obj2Sheet = obj2.GetPart<CharacterSheet>();
			
			Console.WriteLine($"obj2Sheet: Strength({obj2Sheet.Strength}), Intelligence({obj2Sheet.Intelligence}), Charisma({obj2Sheet.Charisma})");

			//Console.WriteLine(obj.GetPart<CharacterSheet>().QueryComponentInfo().AccessType);

			spriteBatch = new SpriteBatch(manager.GraphicsDevice);
		}

		protected override void Update(GameTime gameTime)
		{
			if(Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
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