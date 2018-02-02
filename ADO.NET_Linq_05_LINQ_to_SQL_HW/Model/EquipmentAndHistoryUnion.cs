using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using System.Data.Linq;
namespace ADO.NET_Linq_05_LINQ_to_SQL_HW.Model
{
    [Table(Name = "EquipmentAndHistoryUnion")]
    public class EquipmentAndHistoryUnion
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int EquipmentAndHistoryUnionID { get; set; }
        [Column(Name = "intGarageRoom")]
        public string intGarageRoom { get; set; }

        [Column(Name = "strSerialNo")]
        public string strSerialNo { get; set; }

        [Column(Name = "intTypeHistory")]
        public int? intTypeHistory { get; set; }

        [Column(Name = "dStartDate")]
        public DateTime? dStartDate { get; set; }

        [Column(Name = "dEndDate")]
        public DateTime? dEndDate { get; set; }

        [Column(Name = "intDaysCount")]
        public int? intDaysCount { get; set; }

        [Column(Name = "intStatys")]
        public int? intStatys { get; set; }
    }
}
