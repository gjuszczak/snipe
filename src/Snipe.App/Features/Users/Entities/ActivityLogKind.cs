namespace Snipe.App.Features.Users.Entities
{
    public enum ActivityLogKind
    {
        SignInSuccess,
        SignInFailure,
        SignInWithRefreshTokenSuccess,
        SignInWithRefreshTokenFailure,
        UserLockedOut,
        RefreshTokenRevoked
    }
}
