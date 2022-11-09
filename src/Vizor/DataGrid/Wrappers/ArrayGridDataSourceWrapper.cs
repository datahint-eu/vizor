using Vizor.Data;

namespace Vizor.DataGrid.Wrappers;

internal class ArrayGridDataSourceWrapper<TItem> : IGridDataSourceWrapper<TItem>
{
	private readonly TItem[] arr;

	public ArrayGridDataSourceWrapper(TItem[] arr)
	{
		this.arr = arr;
	}

	public bool SupportsSorting => true;

	public Task<int> Count() => Task.FromResult(arr.Length);

	public Task<ICollection<TItem>> LoadDataAsync(int offset, int count, Expression<Func<TItem, object?>>? sortExpr, ViSortOrder sortOrder)
	{
		if (offset >= arr.Length)
			return Task.FromResult((ICollection<TItem>)new List<TItem>(0));

		var result = new List<TItem>(count);
		var index = offset;
		while (index < arr.Length && result.Count < count)
		{
			result.Add(arr[index]);
			++index;
		}

		return Task.FromResult((ICollection<TItem>)result);
	}
}
