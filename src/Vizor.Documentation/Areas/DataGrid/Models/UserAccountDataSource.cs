using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Bogus;
using Vizor.Data;

namespace Vizor.Documentation.Areas.DataGrid.Models;

public struct UserAccount
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public int Age { get; set; }

    public int Index { get; set; }
}

public class UserAccountDataSource : ISortableGridDataSource<UserAccount>
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

    public async Task<ICollection<UserAccount>> LoadDataAsync(int offset, int count, string? propertyName, ViSortOrder sortOrder)
    {
        await Task.Delay(1000);

        switch (propertyName)
        {
            case nameof(UserAccount.FirstName):
				users.Sort((x, y) => x.FirstName.CompareTo(y.FirstName));
                break;
			case nameof(UserAccount.LastName):
				users.Sort((x, y) => x.LastName.CompareTo(y.LastName));
				break;
			case nameof(UserAccount.Email):
				users.Sort((x, y) => x.Email.CompareTo(y.Email));
				break;
			case nameof(UserAccount.Age):
				users.Sort((x, y) => x.Age.CompareTo(y.Age));
				break;
			case nameof(UserAccount.Index):
				users.Sort((x, y) => x.Index.CompareTo(y.Index));
				break;
		}

        var lst = new List<UserAccount>(count);
        for (int i = offset; i < offset + count; ++i)
        {
            lst.Add(users[i]);
        }
        return lst;
    }
}
