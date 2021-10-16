using System;
using System.Diagnostics;
using System.Text.Json;
using Hgm.Ecs;
using Hgm.Ecs.Text;
using Hgm.Engine.IO;
using Hgm.Register;
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
			manager = new GraphicsDeviceManager(this);
		}

		protected override void Initialize()
		{
			var applePieSchemaName = new NamespacedString("hedgemen:sentient_apple_pie");
			Console.WriteLine($"FullName: {applePieSchemaName.FullName}, Namespace: {applePieSchemaName.Namespace}, Name: {applePieSchemaName.Name}");

			var applePieSchema = JsonSerializer.Deserialize<EntitySchema>(new File("sentient_apple_pie_schema.json").ReadString());
			Console.WriteLine(applePieSchema.RegistryName);
			Console.WriteLine(applePieSchema.Parts.Count);
			Console.WriteLine(applePieSchema.Parts[0].Fields["healing_amount"]);

			/*var foodSchema = JsonSerializer.Deserialize<PartSchema>(new File("food.json").ReadString());
			Console.WriteLine(foodSchema.Fields.Count);
			Console.WriteLine(foodSchema.RegistryName);
			Console.WriteLine(foodSchema.Fields["healing_amount"]);*/

			spriteBatch = new SpriteBatch(manager.GraphicsDevice);
			var test = new GameObject();
			test.AddPart(new MovementPart { Speed = 1025 });
			test.HandleEvent(new GameEventTest());

			Console.WriteLine($"Speed: {test.GetPart<IMovement>().Speed}");

			if(test.WillRespondToEvent<ConsoleWriteMessageEvent>())
				test.HandleEvent(new ConsoleWriteMessageEvent("Hello Parts! Write me!"));
		}

		protected override void Update(GameTime gameTime)
		{
			
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