using Imtudou.Core.Base;
using Imtudou.Core.CommonEnum;
using Imtudou.Core.Data.SqlSugar;
using Imtudou.Core.Data.Test.SqlSugar.Entity;
using Imtudou.Core.Extensions;
using Imtudou.Core.Helpers;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SqlSugar;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace Imtudou.Core.Data.Test.SqlSugar
{
    public class QueryTest
    {
        private readonly string mysql;
        private readonly string sqlserver;
        private readonly string sqlite;
        private readonly ISqlSugarRepository<SchoolEntity, int> schoolRepository;
        private readonly ISqlSugarRepository<ClassGradeEntity, int> classGradeRepository;
        private readonly ISqlSugarRepository<StudentEntity, int> studentRepository;

        public QueryTest()
        {
            mysql = AppSettingsHelper.Configuration.GetConnectionString("mysql");
            sqlserver = AppSettingsHelper.Configuration.GetConnectionString("sqlserver");
            sqlite = AppSettingsHelper.Configuration.GetConnectionString("sqlite");
            this.schoolRepository = this.GetServiceProvider().GetService<ISqlSugarRepository<SchoolEntity, int>>().Instance(new SqlOptions(mysql, DataBaseTypeEnum.MySql)) as ISqlSugarRepository<SchoolEntity, int>;
            this.studentRepository = new SqlSugarRepository<StudentEntity, int>(new SqlOptions(sqlserver, DataBaseTypeEnum.SqlServer));
            this.classGradeRepository = this.GetServiceProvider().GetService<ISqlSugarRepository<ClassGradeEntity, int>>().Instance(new SqlOptions(sqlite, DataBaseTypeEnum.Sqlite)) as ISqlSugarRepository<ClassGradeEntity, int>;
        }

        [Fact]
        public void Test()
        {
            var getResult1 = this.studentRepository.Exists(2001, 21994);

            var existsids = new List<int> { 2001, 21994 };
            var getResult2 = this.studentRepository.Exists(s => existsids.Contains(s.Id));


            var getResult3 = this.studentRepository.GetSingle(s => existsids.Contains(s.Id));

            var getResult4 = this.studentRepository.GetById(2001);

            var getResult5 = this.studentRepository.GetByIds(2001, 21994);

            var getResult6 = this.studentRepository.GetByIds("2001, 21994");

            string createName = "sys";
            Expression<Func<StudentEntity, bool>> expression1 = s => s.ClassGradeId == Guid.Parse("2E07A850-862C-4B1E-B357-EABEA5D9E8B2");
            expression1 = expression1.WhereIf(!string.IsNullOrEmpty(createName), s => s.CreateName.Contains(createName));
            var getResult7 = this.studentRepository.GetList(expression1);

            int total = 0;
            Expression<Func<StudentEntity, bool>> expression2 = Expressionable.Create<StudentEntity>()
                .AndIF(!string.IsNullOrEmpty(createName), s => s.CreateName.Contains(createName))
                .ToExpression();
            var getResult8 = this.studentRepository.GetPageList(expression2, new PageBaseModel { PageIndex = 1, PageSize = 10,OrderByType = OrderByTypeEnum.Desc, OrderFileds = "CreateTime" },ref total);
        }

        [Fact]
        public async void TestAsync()
        {
            var getResult1 = await this.studentRepository.ExistsAsync(1, 2);

            string createName = "sys";
            RefAsync<int> total = 0;
            Expression<Func<StudentEntity, bool>> expression2 = Expressionable.Create<StudentEntity>()
                .AndIF(!string.IsNullOrEmpty(createName), s => s.CreateName.Contains(createName))
                .ToExpression();
            var getResult2 = await this.studentRepository.GetPageListAsync(expression2, new PageBaseModel { PageIndex = 1, PageSize = 10 }, total);
          


        }

        private IServiceProvider GetServiceProvider()
        {
            IServiceCollection service = new ServiceCollection();
            service.AddSingleton<ISqlSugarRepository<SchoolEntity, int>, SqlSugarRepository<SchoolEntity, int>>();
            service.AddSingleton<ISqlSugarRepository<StudentEntity, int>, SqlSugarRepository<StudentEntity, int>>();
            service.AddSingleton<ISqlSugarRepository<ClassGradeEntity, int>, SqlSugarRepository<ClassGradeEntity, int>>();
            return service.BuildServiceProvider();
        }
    }
}
