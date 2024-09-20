namespace AdventureGame
{
	public class Events
	{
		public delegate void MonsterEventHandler(Player player);
		public delegate void TrapEventHandler(Player player);
		public delegate void TreasureEventHandler(Player player);
		public delegate void NothingEventHandler();

		public event MonsterEventHandler MonsterEvent1;
		public event TrapEventHandler TrapEvent1;
		public event TreasureEventHandler TreasureEvent1;
		public event NothingEventHandler NothingEvent1;

		public void PlayerNameEvent(EventOptions option, Player player)
		{
			PlayerChoiceEvent(option, player);
		}

		protected virtual void PlayerChoiceEvent(EventOptions option, Player player)
		{
			switch (option)
			{
				case EventOptions.Monster:
					if (MonsterEvent1 != null)
					{
						MonsterEvent1(player);
					}
					break;
				case EventOptions.Trap:
					if (TrapEvent1 != null)
					{
						TrapEvent1(player);
					}
					break;
				case EventOptions.Treasure:
					if(TreasureEvent1 != null)
					{
						TreasureEvent1(player);
					}
					break;
				case EventOptions.Nothing:
					if (NothingEvent1 != null)
					{
						NothingEvent1();
					}
					break;
				default:
					Console.WriteLine("Invalid Choice.");
					break;
			}
		}
	}
}
