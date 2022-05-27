using Imtudou.Core.CommonEnum;
using Imtudou.Core.Data.SqlSugar;
using Imtudou.Core.Data.Test.SqlSugar.Entity;
using Imtudou.Core.Helpers;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace Imtudou.Core.Data.Test.SqlSugar
{
    public class UpdateTest
    {
        private readonly string mysql;
        private readonly string sqlserver;
        private readonly string sqlite;
        private readonly ISqlSugarRepository<SchoolEntity, int> schoolRepository;
        private readonly ISqlSugarRepository<ClassGradeEntity, int> classGradeRepository;
        private readonly ISqlSugarRepository<StudentEntity, int> studentRepository;

        public UpdateTest()
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
            var getResult1 = this.studentRepository.GetById(21999);
            getResult1.UpdateId = "admin修改";
            getResult1.UpdateName = "admin修改";
            getResult1.UpdateTime = DateTime.Now;
            var updateResult = this.studentRepository.Update(getResult1);
            Assert.True(updateResult);
        }

        [Fact]
        public async void TestAsync()
        {
            var getResult1 = await this.studentRepository.GetListAsync(s => s.ClassGradeId == Guid.Parse("2E07A850-862C-4B1E-B357-EABEA5D9E8B2"));

            for (int i = 0; i < getResult1.Count; i++)
            {
                getResult1[i].UpdateId = $"admin修改_{i}";
                getResult1[i].UpdateName = $"admin修改_{i}";
                getResult1[i].UpdateTime = DateTime.Now;
            }
            var updateResult = await this.studentRepository.UpdateBulkAsync(getResult1);
            Assert.True(updateResult);
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
