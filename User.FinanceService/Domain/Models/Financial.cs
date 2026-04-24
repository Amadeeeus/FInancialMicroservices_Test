namespace User.FinanceService.Domain.Models;

/// <summary>
/// Сущность курсов валют
/// </summary>
public sealed class Financial
{
    /// <summary>
    /// id валюты
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Название валюты
    /// </summary>
    public string Name { get; private set; } = null!;

    /// <summary>ы
    /// Курс к рублю
    /// </summary>
    public string Rate { get; private set; } = null!;
    
    private  Financial()
    {
    }

    public Financial(Guid id, string name, string rate)
    {
        Id = id;
        Name = name;
        Rate = rate;   
    }
}