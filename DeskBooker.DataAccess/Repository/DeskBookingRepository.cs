using Dapper;
using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Domain;
using System.Data;

namespace DeskBooker.DataAccess.Repository
{
    public class DeskBookingRepository : IDeskBookingRepository
    {
        private readonly IDbConnection db;

        public DeskBookingRepository(IDbConnection db)
        {
            this.db = db;
        }

        public async Task InsertDeskBooking(DeskBooking deskBooking)
        {
            var procedureName = "dbo.DeskBooking_Insert";
            var parameters = new DynamicParameters();
            parameters.Add("FirstName", deskBooking.FirstName, DbType.String, ParameterDirection.Input);
            parameters.Add("LastName", deskBooking.LastName, DbType.String, ParameterDirection.Input);
            parameters.Add("Email", deskBooking.Email, DbType.String, ParameterDirection.Input);
            parameters.Add("Date", deskBooking.Date, DbType.DateTime, ParameterDirection.Input);
            parameters.Add("DeskId", deskBooking.DeskId, DbType.Int32, ParameterDirection.Input);

            await db.QueryAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<DeskBooking> GetDeskBooking(int id)
        {
            var procedureName = "dbo.DeskBooking_Get";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

            DeskBooking deskBooking = await db.QueryFirstOrDefaultAsync<DeskBooking>
                (procedureName, parameters, commandType: CommandType.StoredProcedure);

            return deskBooking;
        }

        public async Task DeleteDeskBooking(int id)
        {
            var procedureName = "dbo.DeskBooking_Delete";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

            await db.QueryAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
