namespace Infrastructure.Services.Interfaces
{
    /// <summary>
    /// 現在のユーザのClainやIDを取得するアクセサ
    /// </summary>
    public interface ICurrentUserService
    {
        string? UserId { get; }
    }
}