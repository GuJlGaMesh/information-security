using System;
using System.Collections.Generic;
using System.Linq;

namespace _6th
{
	class Program
	{
		const int usersCount = 3;
		const int filesCount = 10;
		static void Main(string[] args)
		{
			
			var users = new List<User>();
			CreateUsers(users);
			var politics = new DiscretePolitics(usersCount, filesCount);

			var command = string.Empty;
			do
			{
				Console.WriteLine("Введите имя пользователя");
				var username = Console.ReadLine();
				var user = new User();
					if (users.Count(x => x.Name.ToLower() == username?.ToLower()) == 1)
					{
						user = users.Find(x => x.Name.ToLower() == username.ToLower());
						Console.WriteLine("Идентификация прошла успешно");
						Console.WriteLine($"Здраствуйте {username}");
					}
					else
					{
						Console.WriteLine("идентификация прошла не успешно");
						continue;
					}

					for (int i = 0; i < filesCount; i++)
					{
						Console.WriteLine($"File{i}: {politics.FormattedAccessRightsForUser(user.Id, i)}");
					}

					do
					{
						Console.Write("Следующая команда:");
						command = Console.ReadLine();
						if (command == "write" || command == "read")
						{
							Console.WriteLine("над каким файлом?");
							var fileNum = GetFileNumber();

							var right = command == "write" ? AccessRights.Write : AccessRights.Read;
							if (politics.HasRightForFiles(user.Id, fileNum, right))
								Console.WriteLine("Операция успешно прошла");
							else
								Console.WriteLine("У вас нет прав");
						}

						if (command == "grant")
						{
							Console.WriteLine("какому пользователю?");
							var recipientUsername = Console.ReadLine();
							while (users.Count( x => x.Name.ToLower() == recipientUsername) != 1)
							{
								Console.WriteLine("Неправильное имя пользователя, введите заново");
								recipientUsername = Console.ReadLine();
							}

							var recipientUser =
								users.FirstOrDefault(x => x.Name.ToLower() == recipientUsername.ToLower());

							Console.WriteLine("права для какого файла?");
							var fileNum = GetFileNumber();

							Console.WriteLine("какое право хотите передать?");

							command = Console.ReadLine();

							var grant = AccessRights.Grant;
							if (politics.HasRightForFiles(user.Id, fileNum, grant))
							{
								var right = command == "write" ? AccessRights.Write : command == "grant" ? AccessRights.Grant: AccessRights.Read;
								if (politics.HasRightForFiles(user.Id, fileNum, right))
								{
									politics.AddRightForUser(recipientUser.Id, fileNum, right);
								}
								Console.WriteLine("Операция успешно прошла");
							}
							else
								Console.WriteLine("Вы не можете передавать права для данного файла");
						}
					} while (command != "quit");

					Console.WriteLine($"Работа пользователя {user.Name} завершена!");
					Console.WriteLine($"Для выхода напишите stop, для продолжения что-то кроме stop");


			} while ((command = Console.ReadLine()) != "stop");
		}

		public static void CreateUsers(List<User> users)
		{
			for (int i = 0; i < 5; i++)
			{
				var user = new User { Id = i, Name = "User" + i };
				users.Add(user);
			}
		}


		public static int GetFileNumber()
		{
			var fileNum = Convert.ToInt32(Console.ReadLine());
			while (fileNum < 0 || fileNum > filesCount)
			{
				Console.WriteLine("Неправильный номер файла, введите заново");
				fileNum = Convert.ToInt32(Console.ReadLine());
			}

			return fileNum;
		}
	}
}
