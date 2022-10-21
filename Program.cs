using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;

namespace lesson_11
{
	internal class Program
	{
		static void PrintTableUser(Dictionary<string,User> Data)
        {
			string str = $"Name\t|Casino\t|Balance\t|\n";
			foreach(var item in Data.Values)
            {
				str += $"{item.Name}\t|{item.Casino}\t|{item.Balance}\t|\n";
			}
			Console.WriteLine(str);
        }

		static void PrintTableCasino(Dictionary<string, Casino> Data)
		{
			string str = $"Name\t|login\t|Current Slot\t|\n";
			foreach (var item in Data.Values)
			{
				str += $"{item.Name}\t|{item.login}\t|{item.Password}\t|{item.Current_slot}\t|\n";
			}
			Console.WriteLine(str);
		}

		static void PrintSlotsList(Dictionary<string, Slots> Data)
		{
			string str = "";
			foreach (var item in Data.Values)
			{
				str += $"{item.Name}";
			}
			Console.WriteLine(str);
		}

		static void Main(string[] args)
		{
			IFirebaseConfig ifc = new FirebaseConfig()
			{
				AuthSecret = "3psFZEGbg5rdIzaEiYlyZalJa0A0O6NgR5hRKZI4",
				BasePath = "https://dz-11-48855-default-rtdb.europe-west1.firebasedatabase.app/"
			};
			IFirebaseClient client;
			while (true)
            {
				Console.WriteLine("1.Вивід даних \n2.Ввід даних\n->");
				int task = int.Parse(Console.ReadLine());
				int subtask;
				switch (task)
                {
					case 1:
						Console.WriteLine("1. Користувачі 2. Казино 3. Слоти");
						subtask = int.Parse(Console.ReadLine());
						switch (subtask)
                        {
							case 1:
								client = new FireSharp.FirebaseClient(ifc);
								FirebaseResponse res = client.Get(@"User/");
								Dictionary<string, User> DataUser = JsonConvert.DeserializeObject<Dictionary<string, User>>(res.Body.ToString());
								PrintTableUser(DataUser);
								break;
							case 2:
								client = new FireSharp.FirebaseClient(ifc);
								FirebaseResponse resp = client.Get(@"Casino/");
								Dictionary<string, Casino> DataCasino = JsonConvert.DeserializeObject<Dictionary<string, Casino>>(resp.Body.ToString());
								PrintTableCasino(DataCasino);
								break;
							case 3:
								client = new FireSharp.FirebaseClient(ifc);
								FirebaseResponse respz = client.Get(@"Slots/");
								Dictionary<string, Slots> DataSlots = JsonConvert.DeserializeObject<Dictionary<string, Slots>>(respz.Body.ToString());
								PrintSlotsList(DataSlots);
								break;
						}
						break;
					case 2:
						Console.WriteLine("1. Користувачі 2. Казино 3. Слоти");
						subtask = int.Parse(Console.ReadLine());
						switch (subtask)
                        {
							case 1:
								Console.WriteLine("Введіть через ентер ім'я, казино, баланс ");
								var user = new User
								{
									Name = Console.ReadLine(),
									Casino = Console.ReadLine(),
									Balance = int.Parse(Console.ReadLine())
								};
								client = new FireSharp.FirebaseClient(ifc);
								client.Set(@"User/" + user.Name, user);
								break;
							case 2:
								Console.WriteLine("Введіть через ентер казино, логіни, паролі, граючий слот ");
								var casino = new Casino
								{
									Name = Console.ReadLine(),
									login = Console.ReadLine(),
									Password = Console.ReadLine(),
									Current_slot = Console.ReadLine(),
								};
								client = new FireSharp.FirebaseClient(ifc);
								client.Set(@"Casino/" + casino.Name, casino);
								break;
							case 3:
								Console.WriteLine("Введіть через ентер назву слота ");
								var slots = new Slots
								{
									Name = Console.ReadLine()
								};
								client = new FireSharp.FirebaseClient(ifc);
								client.Set(@"Slots/" + slots.Name, slots);
								break;
						}
						break;
                }
            }
            
		}
	}
}
