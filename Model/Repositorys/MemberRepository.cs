using Comm.Model;
using CommonApi.Model.Entity;

namespace CommonApi.Model.Repositorys
{
    public class MemberRepository : BaseRepository<Member>
    {
        public MemberRepository()
        {
            TableName = "public.members";
            DBName = "NGsql";
        }

        public int AddMember(Member item)
        {
            return AddItem(item);
        }

        public int UpdateMember(Member item)
        {
            return Update(item);
        }

        public int DeleteMember(Member item)
        {
            return Delete(item);
        }

        public IEnumerable<Member> SearchMembers(string query)
        {
            var sql = @"SELECT * FROM public.members WHERE name ILIKE @Query OR email ILIKE @Query;";
            return GetItem(sql, new { Query = "%" + query + "%" });
        }
    }
}
