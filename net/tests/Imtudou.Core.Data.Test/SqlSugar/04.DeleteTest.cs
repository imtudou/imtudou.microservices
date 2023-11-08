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
    public class DeleteTest
    {
        private readonly string mysql;
        private readonly string sqlserver;
        private readonly string sqlite;
        private readonly ISqlSugarRepository<SchoolEntity, int> schoolRepository;
        private readonly ISqlSugarRepository<ClassGradeEntity, int> classGradeRepository;
        private readonly ISqlSugarRepository<StudentEntity, int> studentRepository;

        public DeleteTest()
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
            var getResult1 = this.studentRepository.GetSingle(s => s.Id == 22000);
            var getResult2 = this.studentRepository.GetById(21999);
            var getResult3 = this.studentRepository.GetByIds(21996, 21997, 21998);
            var getResult4 = this.classGradeRepository.GetList(s => s.SchoolId == Guid.Parse("c127e5ef-10ea-4cb7-93f3-24e300e98c8c"));
            var deleteResult1 = this.studentRepository.Delete(getResult1);
            Assert.True(deleteResult1);
            var deleteResult2 = this.studentRepository.DeleteById(21999);
            var deleteResult3 = this.studentRepository.DeleteByIds(21998,21997);

        }

        [Fact]
        public async void TestAsync()   
        {
            var getResult4 = await this.classGradeRepository.GetListAsync(s => s.SchoolId == Guid.Parse("c127e5ef-10ea-4cb7-93f3-24e300e98c8c"));
            var deleteResult = this.classGradeRepository.Delete(getResult4);
            Assert.True(deleteResult);

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
