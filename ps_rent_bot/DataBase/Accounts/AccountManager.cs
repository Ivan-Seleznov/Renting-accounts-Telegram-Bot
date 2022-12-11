using ps_rent_bot.DataBase.Accounts;
using System;
namespace ps_rent_bot.DataBase
{
	public class AccountManager
	{
	

		public static Account FindAccount(string Game)
        {
			try
			{
				string game_toLower = Game.ToLower();
				foreach (var account in Program.Db.Accounts)
				{
					if (account.Games.Contains(Game) || account.Games.Contains(game_toLower))
					{
                        if (account.IsRented == false)
                        {
							return account;
						}
						
					}
				}
				return null;
			}
			catch (Exception)
			{
				throw new Exception("Ошибка поиска аккаунта");
			}
        }
	}
}
