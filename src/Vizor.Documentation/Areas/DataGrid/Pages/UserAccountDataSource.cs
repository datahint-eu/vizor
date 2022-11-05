using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Bogus;
using Vizor.Data;

namespace Vizor.Documentation.Areas.DataGrid.Pages;

public struct UserAccount
{
	public string FirstName { get; set; }

	public string LastName { get; set; }

	public string Email { get; set; }

	public int Age { get; set; }

	public int Index { get; set; }
}

public class UserAccountDataSource : IDataSource<UserAccount>
{
	private const int TotalCount = 500;
	private List<UserAccount> users = new(TotalCount);

	public UserAccountDataSource()
	{
		for (int i = 0; i < TotalCount; ++i)
		{
			var faker = new Faker("en");
			var u = new UserAccount()
			{
				FirstName = faker.Person.FirstName,
				LastName = faker.Person.LastName,
				Email = faker.Person.Email,
				Age = faker.Random.Number(18, 99),
				Index = i + 1
			};
			users.Add(u);
		}
	}

	public Task<int> Count() => Task.FromResult(TotalCount);

	public async Task<ICollection<UserAccount>> Load(int offset, int count)
	{
		await Task.Delay(1000);

		var lst = new List<UserAccount>(count);
		for (int i = offset; i < offset + count; ++i)
		{
			lst.Add(users[i]);
		}
		return lst;
	}
}
