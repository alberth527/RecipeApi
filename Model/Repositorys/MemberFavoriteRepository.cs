using Comm.Model;
using CommonApi.Model.Entity;

namespace CommonApi.Model.Repositorys
{
    public class MemberFavoriteRepository : BaseRepository<MemberFavorite>
    {
        public MemberFavoriteRepository()
        {
            TableName = "public.member_favorites";
            DBName = "NGsql";
        }

        public int AddFavorite(MemberFavorite item)
        {
            return AddItem(item);
        }

        public int UpdateFavorite(MemberFavorite item)
        {
            return Update(item);
        }

        public int DeleteFavorite(MemberFavorite item)
        {
            return Delete(item);
        }
    }
}
