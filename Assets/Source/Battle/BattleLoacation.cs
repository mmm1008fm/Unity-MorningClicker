public class BattleLoacation
{
	public string Name { get; private set; }
	public string Description { get; private set; }

	public BattleLoacation(string name, string description)
	{
		Name = name;
		Description = description;
	}
}