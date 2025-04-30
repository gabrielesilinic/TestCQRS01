using TestCQRS.CqrsUtils;

namespace TestCQRS.Commands
{
    public static class FakeDatabase
    {
        public static string? FavoritePlace { get; set; }
    }

    public class SetFavoritePlaceCommand
    {
        public string Place { get; set; }
    }

    public class SetFavoritePlaceCommandHandler : CommandBase<SetFavoritePlaceCommandHandler,Unit, SetFavoritePlaceCommand>
    {

        public override Task<Unit> Handle(SetFavoritePlaceCommand command)
        {
            FakeDatabase.FavoritePlace = command.Place;
            return Task.FromResult(Unit.Value);
        }
    }
}
