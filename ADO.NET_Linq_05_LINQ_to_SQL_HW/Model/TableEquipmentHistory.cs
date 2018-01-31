namespace ADO.NET_Linq_05_LINQ_to_SQL_HW.Model
{
    using System;
    using System.Collections.Generic;
    using System.Data.Linq.Mapping;
    using System.Data.Linq;
    using System.Data.Entity.Spatial;

    [Table(Name = "TableEquipmentHistory")]
    public partial class TableEquipmentHistory
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int intEquipmentHistoryId { get; set; }
        [Column(Name = "intEquipmentID")]
        public int? intEquipmentID { get; set; }
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
        [Column(Name = "intUserId")]
        public int? intUserId { get; set; }

        [Column(Name = "dCahengeDate")]
        public DateTime? dCahengeDate { get; set; }

        [Association(ThisKey = "intEquipmentID", OtherKey = "intEquipmentID")]
        public EntitySet<newEquipment> NewEquipments{ get; set; }
    }
}
