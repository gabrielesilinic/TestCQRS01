using System.Formats.Asn1;
using TestCQRS.Commands;
using TestCQRS.CqrsUtils;

namespace TestCQRS.Queries
{
    public class GetFavoritePlaceQueryResponse
    {
        public string Place { get; set; }
    }
    public class GetFavoritePlaceQueryHandler : QueryBase<GetFavoritePlaceQueryHandler,GetFavoritePlaceQueryResponse, Unit>
    {
        async public override Task<GetFavoritePlaceQueryResponse> Handle(Unit input)
        {
            await Task.CompletedTask;
            return new GetFavoritePlaceQueryResponse()
            {
                Place = FakeDatabase.FavoritePlace!
            };
        }
    }
}
