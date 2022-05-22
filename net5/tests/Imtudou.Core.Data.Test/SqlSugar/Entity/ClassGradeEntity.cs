using Imtudou.Core.Base;

using SqlSugar;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imtudou.Core.Data.Test.SqlSugar.Entity
{
    //实体与数据库结构一样
    [SugarTable("tbClassGrade")]
    public class ClassGradeEntity : BaseEntity, IKey<int>
    {
        //数据是自增需要加上IsIdentity 
        //数据库是主键需要加上IsPrimaryKey 
        //注意：要完全和数据库一致2个属性
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public Guid ClassGradeId { get; set; }
        public Guid SchoolId { get; set; }
        public string Name { get; set; }


    }
}
