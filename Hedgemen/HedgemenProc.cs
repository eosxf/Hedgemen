using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Hgm.Ecs;
using Hgm.Ecs.Text;
using Hgm.IO;
using Hgm.IO.Serialization;
using Hgm.Register;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CharacterSheet = Hgm.Ecs.CharacterSheet;
using Entity = Hgm.Ecs.Entity;

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
		TestKaze();
		TestEcs();
		TestSchema();
		_spriteBatch = new SpriteBatch(_manager.GraphicsDevice);
	}

	private void TestKaze()
	{
		Hedgemen.Kaze.Registry.Components.Register("hedgemen:character_sheet", () => new CharacterSheet());
		var sheet = Hedgemen.Kaze.Registry.Components["hedgemen:character_sheet"]() as CharacterSheet;
		Console.WriteLine($"Sheet: {sheet.Strength}");
	}

	private void TestEcs()
	{
		var entity = new Entity();
		entity.AddPart(new CharacterSheet
		{
			Class = new CharacterClassWarrior()
		});

		var sheet = entity.GetPart<CharacterSheet>();
		sheet.Strength = 1025;
		sheet.Intelligence = 10;
		sheet.Charisma = 15;

		var eChangeClass = entity.Propagate(new ChangeClassEvent("archer"));
		Console.WriteLine($"Handled event '{typeof(ChangeClassEvent)}'? Answer: {eChangeClass.Handled}");

		SerializeJson(entity);
	}

	private void TestSchema()
	{
		var schema = new EntitySchema(new File("sentient_apple_pie_schema.json"));
		Console.WriteLine(schema);

		var foodSchema = schema.Components[0];
		Console.WriteLine("Food healing amount: {0}", foodSchema.Fields.Get<int>("healing_amount"));
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

		var entity2Info = JsonSerializer.Deserialize<SerializedInfo>(file.Open(), options)!;

		var entity2 = entity2Info.Instantiate<Entity>();
		Console.WriteLine(entity.GetPart<CharacterSheet>());
		Console.WriteLine(entity2.GetPart<CharacterSheet>().Intelligence);

		Console.WriteLine($"obj2 class_name: {entity2.GetPart<CharacterSheet>().Class.ClassName}");
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