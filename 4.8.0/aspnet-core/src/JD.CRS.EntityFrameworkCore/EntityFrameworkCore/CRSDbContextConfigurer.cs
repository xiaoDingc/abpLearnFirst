using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace JD.CRS.EntityFrameworkCore
{
    public static class CRSDbContextConfigurer
    {
        /// <summary>
        /// 创建 LoggerFactory 的单一实例/全局实例 控制台记录
        /// 面ConsoleLoggerProvider的构造函数将在未来的3.0中过时，现在可以不管
        /// </summary>
        public static readonly  LoggerFactory MyLoggerFactory=
            new LoggerFactory(new []
            {
                new ConsoleLoggerProvider((categoty,level)=>
                categoty==DbLoggerCategory.Database.Command.Name
                && level==LogLevel.Information,true
                    ),
            });
        public static void Configure(DbContextOptionsBuilder<CRSDbContext> builder, string connectionString)
        {
            builder.UseLoggerFactory(MyLoggerFactory).UseSqlServer(connectionString);
           // builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<CRSDbContext> builder, DbConnection connection)
        {
            builder.UseLoggerFactory(MyLoggerFactory).UseSqlServer(connection);
            //builder.UseSqlServer(connection);
        }
    }
}
