using System;
using System.Collections.Generic;
using System.Linq;

namespace _6th
{
	public class DiscretePolitics
	{
		private int users, files;

		private List<List<AccessRights>> _rightsProvider = new List<List<AccessRights>>()
		{
			new List<AccessRights> {AccessRights.Grant, AccessRights.Read, AccessRights.Write},
			new List<AccessRights> {AccessRights.Grant, AccessRights.Read},
			new List<AccessRights> {AccessRights.Read},
			new List<AccessRights> {AccessRights.Grant, AccessRights.Write},
			new List<AccessRights> {AccessRights.Write},
			new List<AccessRights> {AccessRights.Write, AccessRights.Read},
			new List<AccessRights> {AccessRights.Forbidden},
		};

		public List<AccessRights>[,] PoliticsMatrix { get; }


		public DiscretePolitics(int countOfUsers = 5, int countOfFiles = 3)
		{
			users = countOfUsers;
			files = countOfFiles;

			PoliticsMatrix = new List<AccessRights>[countOfUsers, countOfFiles];

			for (int i = 0; i < countOfUsers; i++)
			{
				for (int j = 0; j < countOfFiles; j++)
				{
					PoliticsMatrix[i, j] = TakeAccessRights();
				}
			}

			var random = new Random();
			var adminId = random.Next(0, countOfUsers);
			for (int i =0; i < files; i++)
				PoliticsMatrix[adminId, i] = SetAdminRights();
		}

		private List<AccessRights> SetAdminRights()
		{
			var arr = new AccessRights[_rightsProvider[0].Count];
			_rightsProvider[0].CopyTo(arr);
			return arr.ToList();
		}


		private List<AccessRights> TakeAccessRights()
		{
			var r = new Random();
			var randomRights = r.Next(0, 6);
			var arr = new AccessRights[_rightsProvider[randomRights].Count];
			_rightsProvider[randomRights].CopyTo(arr);
			return arr.ToList();
		}

		public bool HasRightForFiles(int id, int file, AccessRights right) => PoliticsMatrix[id, file].Contains(right);

		public string FormattedAccessRightsForUser(int id, int file)
		{
			var result = string.Empty;
			foreach (var rights in PoliticsMatrix[id, file])
			{
				result += rights + " ";
			}
			return result;
		}

		public void AddRightForUser(int id,int file, AccessRights right)
		{
			if (!PoliticsMatrix[id, file].Contains(right))
				PoliticsMatrix[id,file].Add(right);
		}
	}
}
