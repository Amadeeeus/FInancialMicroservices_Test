namespace UserService.Domain.Entities;

/// <summary>
/// Сущность пользователя
/// </summary>
public sealed class UserEntity
{
    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Name { get; private set; } = null!;

    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; private set; } = null!;
    
    /// <summary>
    /// Интересные пользователю курсы
    /// </summary>
    public string? Favourites { get; private set; }

    private UserEntity()
    {
    }

    public UserEntity(Guid id, string name, string password, string favourites)
    {
        Id = id;
        Name = name;
        Password = password;
        Favourites = favourites;
    }
    
    public void Update(string name, string password, string favourites)
    {
        Name = name;
        Password = password;
        Favourites = favourites;
    }
}