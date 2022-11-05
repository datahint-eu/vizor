﻿using System.Collections.Generic;
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

	public Task<int> Count() => Task.FromResult(TotalCount);

	public async Task<ICollection<UserAccount>> Load(int offset, int count)
	{
		if (count <= 0)
			count = TotalCount;
		if (offset + count > TotalCount) // TotalCount = 110, offset 100, count 50 --> count = 10
			count = TotalCount - offset;

		await Task.Delay(1000);

		var lst = new List<UserAccount>(count);
		for (int i = 0; i < count; ++i)
		{
			var faker = new Faker("en");
			var u = new UserAccount()
			{
				FirstName = faker.Person.FirstName,
				LastName = faker.Person.LastName,
				Email = faker.Person.Email,
				Age = faker.Random.Number(18, 99),
				Index = offset + i
			};
			lst.Add(u);
		}
		return lst;
	}
}