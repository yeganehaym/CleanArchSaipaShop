using System.Data;
using System.Data.Common;
using DNTPersianUtils.Core;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace SaipaShop.Persistent.Sql.Interceptors;

/// <summary>
/// a class for persian language - convert Ye Ke letters from arabic form to persian form by any keyboard that user is typing before saving to db
/// </summary>
public class PersianYeKeCommandInterceptor : DbCommandInterceptor
    {
        public override InterceptionResult<DbDataReader> ReaderExecuting(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result)
        {
            ApplyCorrectYeKe(command);
            return result;
        }

        public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result,
            CancellationToken cancellationToken = new CancellationToken())
        {
            ApplyCorrectYeKe(command);
            return ValueTask.FromResult(result);
        }

        public override InterceptionResult<int> NonQueryExecuting(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<int> result)
        {
            ApplyCorrectYeKe(command);
            return result;
        }

        public override ValueTask<InterceptionResult<int>> NonQueryExecutingAsync(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = new CancellationToken())
        {
            ApplyCorrectYeKe(command);
            return ValueTask.FromResult(result);
        }

        public override InterceptionResult<object> ScalarExecuting(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<object> result)
        {
            ApplyCorrectYeKe(command);
            return result;
        }

        public override ValueTask<InterceptionResult<object>> ScalarExecutingAsync(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<object> result,
            CancellationToken cancellationToken = new CancellationToken())
        {
            ApplyCorrectYeKe(command);
            return ValueTask.FromResult(result);
        }

        private static void ApplyCorrectYeKe(DbCommand command)
        {
            command.CommandText = command.CommandText.ApplyCorrectYeKe();

            foreach (DbParameter parameter in command.Parameters)
            {
                switch (parameter.DbType)
                {
                    case DbType.AnsiString:
                    case DbType.AnsiStringFixedLength:
                    case DbType.String:
                    case DbType.StringFixedLength:
                    case DbType.Xml:
                        parameter.Value =   parameter.Value is DBNull ? parameter.Value : parameter.Value.ToString().ApplyCorrectYeKe();
                        break;
                }
            }
        }
    }