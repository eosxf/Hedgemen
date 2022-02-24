using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Hgm.Ecs;
using Hgm.IO.Serialization;
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
			TestEcs();
			spriteBatch = new SpriteBatch(manager.GraphicsDevice);
		}
		private void TestEcs()
		{
			var entity = new Entity();
			entity.AddPart(new CharacterSheet
			{
				Class = new CharacterClassWarrior
				{
					
				}
			});

			var sheet = entity.GetPart<CharacterSheet>();
			sheet.Strength = 1025;
			sheet.Intelligence = 10;
			sheet.Charisma = 15;

			var eChangeClass = entity.Propagate(new GameChangeClassEvent("archer"));
			Console.WriteLine($"Handled event '{typeof(GameChangeClassEvent)}'? Answer: {eChangeClass.Handled}");
			
			SerializeJson(entity);
		}

		private void SerializeJson(Entity entity)
		{
			IFile file = new File("serialized_obj.json");

			var options = new JsonSerializerOptions
			{
				IgnoreReadOnlyFields = false,
				IgnoreReadOnlyProperties = false,
				IncludeFields = false,
				UnknownTypeHandling = JsonUnknownTypeHandling.JsonNode,
				WriteIndented = true
			};
			
			file.WriteString(JsonSerializer.Serialize(entity.GetSerializedInfo(), options));

			var entity2Info = JsonSerializer.Deserialize<SerializedInfo>(file.Open(), options);

			var entity2 = entity2Info.Instantiate<Entity>();
			Console.WriteLine(entity.GetPart<CharacterSheet>());
			Console.WriteLine(entity2.GetPart<CharacterSheet>().Intelligence);
			
			Console.WriteLine($"obj2 class_name: {entity2.GetPart<CharacterSheet>().Class.ClassName}");
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