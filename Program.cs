namespace AdventureGame
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Player player = new Player();
			Events events = new Events();

			events.TrapEvent1 += TrapEvent;
			events.MonsterEvent1 += MonsterEvent;
			events.NothingEvent1 += NothingEvent;
			events.TreasureEvent1 += TreasureEvent;

			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.WriteLine("\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\n" +
				"                                                    \n" +
				"           Dungeon Crawler Adventure Game           \n" +
				"                                                    \n" +
				"////////////////////////////////////////////////////");
			

			Console.Write("Type your name: ");
			string name = Console.ReadLine();
			player.PlayerName = name;

			Console.WriteLine("\nHello " + name);

			while (player.PlayerHealth > 0)
			{
				int playerChoice;
				int[] pathOptions = GetPathOptions();
				DisplayOptions(pathOptions);

				if (int.TryParse(Console.ReadLine(), out playerChoice) && pathOptions.Contains(playerChoice))
				{
					if (playerChoice == (int)PathOptions.DeadEnd)
					{
						Console.WriteLine($"\nYour reached a deadend! {name}.");
						continue;
					}

					EventOptions eventOption = (EventOptions)TriggerEvent();
					events.PlayerNameEvent(eventOption, player);

					if (player.PlayerHealth <= 0)
					{
						Console.WriteLine("\nGame Over! Your health is 0.");
						break;
					}
				}
				else
				{
					Console.WriteLine("\nInvalid Choice. Please Choose again.");
				}
			}

			Console.WriteLine($"\n{player.PlayerName}'s Final Score = {player.PlayerScore}\n\n\n");
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
		}

		private static int[] GetPathOptions()
		{
			Random random = new Random();
			bool isDeadEnd = random.Next(0, 4) == 0;

			if (isDeadEnd)
			{
				return new int[] { (int)PathOptions.DeadEnd };
			}
            else
            {
				List<int> results = new List<int>();

				if (random.Next(0, 2) == 1) results.Add((int)PathOptions.Left);
				if (random.Next(0, 2) == 1) results.Add((int)PathOptions.Right);
				if (random.Next(0, 2) == 1) results.Add((int)PathOptions.Straight);

				if (results.Count == 0) {
					results.AddRange(new int[] { (int)PathOptions.Left, (int)PathOptions.Right, (int)PathOptions.Straight });
				}

				return results.ToArray();
			}
        }

		private static int TriggerEvent()
		{
			Random random = new Random();
			return random.Next(1, 5);
		}

		private static void DisplayOptions(int[] pathOptions)
		{
			Console.WriteLine("\nAvailable directions:");
			foreach(int option in pathOptions)
			{
				Console.WriteLine($"{option}: {(PathOptions)option}");
			}
			Console.Write(">>>> ");
		}

		private static void MonsterEvent(Player player)
		{
			Console.Write("\nA monster appeared....\n" +
				"Do you want to \n" +
				"1. Fight\n" +
				"2. Flee\n" +
				">>>");

			Random random = new Random();
			int choice = int.Parse(Console.ReadLine());

			if (choice == 1)
			{
				if (random.Next(0, 2) == 1)
				{
					Console.WriteLine("\nCongretulations! You defeated the monster (^_^).");
					player.PlayerScore += 200;
					Console.WriteLine($"\n{player.PlayerName} Health: {player.PlayerHealth}\n" +
						$"{player.PlayerName} Score: {player.PlayerScore}");
				}
				else
				{
					Console.WriteLine("\nOh nooo, You got defeated by the monster.");
					player.PlayerScore -= 200;
					player.PlayerHealth -= 250;
					Console.WriteLine($"\n{player.PlayerName} Health: {player.PlayerHealth}\n" +
						$"{player.PlayerName} Score: {player.PlayerScore}");
				}
			}
			else
			{
				Console.WriteLine("\nYou fleed>>>>>");
				Console.WriteLine($"\n{player.PlayerName} Health: {player.PlayerHealth}\n" +
						$"{player.PlayerName} Score: {player.PlayerScore}");
			}
		}

		private static void TrapEvent(Player player)
		{
			Random random = new Random();
			Console.WriteLine("\nYou got into a trap....");
			if (random.Next(0, 2) == 1)
			{
				Console.WriteLine("\nyou avoided the trap.");
				Console.WriteLine($"\n{player.PlayerName} Health: {player.PlayerHealth}\n" +
						$"{player.PlayerName} Score: {player.PlayerScore}");
			}
			else
			{
				Console.WriteLine("\nYou got hurt from the trap....");
				player.PlayerHealth -= 250;
				player.PlayerScore -= 200;
				Console.WriteLine($"\n{player.PlayerName} Health: {player.PlayerHealth}\n" +
						$"{player.PlayerName} Score: {player.PlayerScore}");
			}
		}

		private static void TreasureEvent(Player player)
		{
			Random random = new Random();
			int tresurePoints = random.Next(0, 201);
			Console.WriteLine($"\nCongretulations! You Found Tresure chest with {tresurePoints} points.");
			player.PlayerScore += tresurePoints;
			Console.WriteLine($"\n{player.PlayerName} Health: {player.PlayerHealth}\n" +
						$"{player.PlayerName} Score: {player.PlayerScore}");
		}

		private static void NothingEvent()
		{
			Console.WriteLine("\nNothing happend. Move forward.");
		}
	}
}
