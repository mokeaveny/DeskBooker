using Dapper;
using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Domain;
using System.Data;

namespace DeskBooker.DataAccess.Repository
{
    public class DeskRepository : IDeskRepository
    {
        private readonly IDbConnection db;

        public DeskRepository(IDbConnection db)
        {
            this.db = db;
        }

        public async Task<List<Desk>> GetAllDesks()
        {
            var procedureName = "dbo.Desk_Get_All";
            var parameters = new DynamicParameters();

            List<Desk> desks = (await db.QueryAsync<Desk>
                (procedureName, commandType: CommandType.StoredProcedure)).ToList();

            return desks;
        }

        public List<Desk> GetAvailableDesks(DateTime date)
        {
            var procedureName = "dbo.Desk_Get_ForAvailableDeskBookingDate";
            var parameters = new DynamicParameters();
            parameters.Add("Date", date, DbType.DateTime, ParameterDirection.Input);

            List<Desk> desks = (db.Query<Desk>
                (procedureName, parameters, commandType: CommandType.StoredProcedure)).ToList();

            return desks;
        }
    }
}
