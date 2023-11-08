using Imtudou.Core.CommonEnum;
using Imtudou.Core.Data.SqlSugar;
using Imtudou.Core.Data.Test.SqlSugar.Entity;
using Imtudou.Core.Helpers;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xunit;
using Xunit.Abstractions;

namespace Imtudou.Core.Data.Test.SqlSugar
{
    public class GetStarted
    {
        private readonly ITestOutputHelper testOutputHelper;
        private readonly string mysql;
        private readonly string sqlserver;
        private readonly string sqlite;
        private readonly ISqlSugarRepository<SchoolEntity, int> schoolRepository;
        private readonly ISqlSugarRepository<ClassGradeEntity, int> classGradeRepository;
        private readonly ISqlSugarRepository<StudentEntity, int> studentRepository;

        public GetStarted(ITestOutputHelper test)
        {
            this.testOutputHelper = test;
            string testst = AppSettingsHelper.Configuration["test"];
            mysql = AppSettingsHelper.Configuration.GetConnectionString("mysql");
            var mysql2 = AppSettingsHelper.Configuration["ConnectionStrings:mysql"];
            sqlserver = AppSettingsHelper.Configuration.GetConnectionString("sqlserver");
            sqlite = AppSettingsHelper.Configuration.GetConnectionString("sqlite");
            this.schoolRepository = this.GetServiceProvider().GetService<ISqlSugarRepository<SchoolEntity, int>>().Instance(new SqlOptions(mysql, DataBaseTypeEnum.MySql)) as ISqlSugarRepository<SchoolEntity, int>;
            this.studentRepository =  new SqlSugarRepository<StudentEntity, int>(new SqlOptions(sqlserver, DataBaseTypeEnum.SqlServer));
            this.classGradeRepository = this.GetServiceProvider().GetService<ISqlSugarRepository<ClassGradeEntity, int>>().Instance(new SqlOptions(sqlite, DataBaseTypeEnum.Sqlite)) as ISqlSugarRepository<ClassGradeEntity, int>;
        }


        [Fact]
        public void InitTables()
        {
            this.schoolRepository.DbContext.CodeFirst.SetStringDefaultLength(200).InitTables(typeof(SchoolEntity));
            this.studentRepository.DbContext.CodeFirst.SetStringDefaultLength(200).InitTables(typeof(StudentEntity));
            this.classGradeRepository.DbContext.CodeFirst.SetStringDefaultLength(200).InitTables(typeof(ClassGradeEntity));
        }


        [Fact]
        public async void Add()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
  
            var schoolInput = new SchoolEntity
            {
                Name = "振华一中",
                SchoolId = Guid.NewGuid(),
                CreateTime = DateTime.Now,
                CreateId = "admin",
                CreateName = "admin",
                UpdateTime = DateTime.Now,
                UpdateId = "admin",
                UpdateName = "admin",
            };
            var addSchoolResult = this.schoolRepository.Insert(schoolInput);
            Assert.True(addSchoolResult > 0);

            var classGradeInput = new ClassGradeEntity
            {
                Name = "一班",
                SchoolId = schoolInput.SchoolId,
                ClassGradeId = Guid.NewGuid(),
                CreateTime = DateTime.Now,
                CreateId = "admin",
                CreateName = "admin",
                UpdateTime = DateTime.Now,
                UpdateId = "admin",
                UpdateName = "admin",
            };
            var addclassGradeResult = await this.classGradeRepository.InsertAsync(classGradeInput);
            Assert.True(addclassGradeResult > 0);

            var studentInput = new List<StudentEntity>();
            for (int i = 0; i < 20000; i++)
            {
                Thread.Sleep(1);
                studentInput.Add(new StudentEntity
                {
                    Name = "张三_" + i,
                    ClassGradeId = classGradeInput.ClassGradeId,
                    StudentId = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    CreateId = "admin",
                    CreateName = "admin",
                    UpdateTime = DateTime.Now,
                    UpdateId = "admin",
                    UpdateName = "admin",
                });
            }
            var addStudentResult = this.studentRepository.InsertBulk(studentInput);

            Assert.True(addStudentResult > 0);
            stopwatch.Stop();
            this.testOutputHelper.WriteLine($"获取运行时间[毫秒]  :{stopwatch.ElapsedMilliseconds.ToString()}");
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
