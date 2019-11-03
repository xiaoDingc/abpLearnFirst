using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace JD.CRS.EntityFrameworkCore
{
    public static class CRSDbContextConfigurer
    {
        /// <summary>
        /// ���� LoggerFactory �ĵ�һʵ��/ȫ��ʵ�� ����̨��¼
        /// ��ConsoleLoggerProvider�Ĺ��캯������δ����3.0�й�ʱ�����ڿ��Բ���
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
