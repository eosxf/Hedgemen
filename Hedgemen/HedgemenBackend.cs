using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Hgm.Base;
using Hgm.Ecs;
using Hgm.Ecs.Text;
using Hgm.IO;
using Hgm.IO.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Hgm;

public sealed class HedgemenBackend : Game, IHedgemenBackend
{
	private readonly GraphicsDeviceManager _manager;
	private SpriteBatch _spriteBatch;

	public HedgemenBackend()
	{
		_manager = new GraphicsDeviceManager(this);
	}

	protected override void Initialize()
	{
		TestKaze();
		EcsNew.EcsNewSandbox.Sandbox();
		//TestEcs();
		//TestSchema();
		//TestNewSerialization();
		_spriteBatch = new SpriteBatch(_manager.GraphicsDevice);
	}

	private void TestNewSerialization()
	{
		var entity = new Entity();
		entity.AddComponent(new CharacterSheet
		{
			Class = new CharacterClassWarrior()
		});

		var sheet = entity.GetComponent<CharacterSheet>();
		sheet.Strength = 1025;
		sheet.Intelligence = 10;
		sheet.Charisma = 15;

		var options = new JsonSerializerOptions
		{
			WriteIndented = true
		};

		IFile file = new File("new_serialization.json");
		string json = JsonSerializer.Serialize(entity.GetObjectState(), options);
		file.WriteString(json);

		var state = JsonSerializer.Deserialize<SerializationState>(file.ReadString(), options)!;
		var newEntity = state.Instantiate<Entity>();
		Console.WriteLine($"New entity character sheet strength: {newEntity.GetComponent<CharacterSheet>().Strength}");
	}

	private void TestKaze()
	{
		var hedgemen = new HedgemenMod();
		hedgemen.Initialize();
	}

	private void TestEcs()
	{
		var entity = new Entity();
		entity.AddComponent(new CharacterSheet
		{
			Class = new CharacterClassWarrior()
		});

		var sheet = entity.GetComponent<CharacterSheet>();
		sheet.Strength = 1025;
		sheet.Intelligence = 10;
		sheet.Charisma = 15;

		Console.WriteLine(entity.GetComponent<CharacterSheet>().QueryComponentInfo());

		var eChangeClass = entity.Propagate(new ChangeClassEvent("archer"));
		Console.WriteLine($"Handled event '{typeof(ChangeClassEvent)}'? Answer: {eChangeClass.Handled}");

		SerializeJson(entity);
	}

	private void TestSchema()
	{
		var schema = new EntitySchema(new File("sentient_apple_pie_schema.json"));
		var entity = new Entity(schema);
		Console.WriteLine(entity.GetComponent<CharacterSheet>().Class);
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

		file.WriteString(JsonSerializer.Serialize(entity.GetObjectState(), options));

		var entity2Info = JsonSerializer.Deserialize<SerializationState>(file.Open(), options)!;

		var entity2 = entity2Info.Instantiate<Entity>();
		Console.WriteLine(entity.GetComponent<CharacterSheet>());

		Console.WriteLine(entity2.GetComponent<CharacterSheet>().Intelligence);

		Console.WriteLine($"obj2 class_name: {entity2.GetComponent<CharacterSheet>().Class.ClassName}");
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