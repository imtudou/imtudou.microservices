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
    public class InsertTest
    {
        private readonly string mysql;
        private readonly string sqlserver;
        private readonly string sqlite;
        private readonly ISqlSugarRepository<SchoolEntity, int> schoolRepository;
        private readonly ISqlSugarRepository<ClassGradeEntity, int> classGradeRepository;
        private readonly ISqlSugarRepository<StudentEntity, int> studentRepository;

        public InsertTest()
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
            var schoolInput = new List<SchoolEntity>();
            for (int i = 1; i <= 10; i++)
            {
                schoolInput.Add(new SchoolEntity
                {
                    Name = $"振华{i}中",
                    SchoolId = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    CreateId = "admin",
                    CreateName = "admin",
                    UpdateTime = DateTime.Now,
                    UpdateId = "admin",
                    UpdateName = "admin",
                });                  
            }
            var addSchoolResult = this.schoolRepository.Insert(schoolInput);
            Assert.True(addSchoolResult == 10);
        }

        [Fact]
        public async void TestAsync()
        {
            var classGradeInput = new List<ClassGradeEntity>();
            for (int i = 1; i <= 10; i++)
            {
                classGradeInput.Add(new ClassGradeEntity
                {
                    Name = $"{i}班",
                    SchoolId = Guid.Parse("a07b04b0-e433-4952-a082-547b5a93438e"),
                    ClassGradeId = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    CreateId = "admin",
                    CreateName = "admin",
                    UpdateTime = DateTime.Now,
                    UpdateId = "admin",
                    UpdateName = "admin",
                });
            }
           
            var addclassGradeResult = await this.classGradeRepository.InsertAsync(classGradeInput);
            Assert.True(addclassGradeResult == 10);
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
