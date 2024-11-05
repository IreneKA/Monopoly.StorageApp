var serviceCollection = new ServiceCollection();

serviceCollection.AddStorageServices(options => options.UseSqlite("Data Source=StorageApp.db"));

using var servicesProvider = serviceCollection.BuildServiceProvider();

var palletService = servicesProvider.GetRequiredService<IPalletService>()!;

var groupedPallets = await palletService.GetGroupedByExpirationDateAsync();

Console.WriteLine("Сгруппировать все паллеты по сроку годности, отсортировать по возрастанию срока годности, в каждой группе отсортировать паллеты по весу:\n");

ShowGroupedPallets(groupedPallets);

Console.WriteLine("\n3 паллеты, которые содержат коробки с наибольшим сроком годности, отсортированные по возрастанию объема:\n");

var pallets = await palletService.GetTopByExpirationDateAsync();

ShowPallets(pallets);

Console.ReadKey();

void ShowGroupedPallets(IEnumerable<(DateOnly? ExpirationDate, IEnumerable<IPallet> Pallets)> palletGroups)
{
    if (!palletGroups.Any())
    {
        Console.WriteLine("Список пуст.");
        return;
    }

    foreach (var palletGroup in palletGroups)
    {
        var expDateString = palletGroup.ExpirationDate == null ? "Срок годности не указан" : palletGroup.ExpirationDate.ToString(); 
        Console.WriteLine($"{expDateString}:");
        ShowPallets(palletGroup.Pallets);
    }
}

void ShowPallets(IEnumerable<IPallet> pallets)
{
    if (!pallets.Any())
    {
        Console.WriteLine("Список пуст.");
        return;
    }

    Console.WriteLine(JsonSerializer.Serialize(pallets, new JsonSerializerOptions() { WriteIndented = true }));
}
