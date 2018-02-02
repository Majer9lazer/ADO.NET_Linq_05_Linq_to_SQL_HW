namespace ADO.NET_Linq_05_LINQ_to_SQL_HW.Model
{
    using System;
    using System.Collections.Generic;
    using System.Data.Linq.Mapping;
    using System.Data.Linq;
    using System.Data.Entity.Spatial;

    [Table(Name ="TablesModel")]
    public  class TablesModel
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int intModelID { get; set; }

        [Column(Name = "strName")]
        public string strName { get; set; }
        [Column(Name = "intManufacturerID")]
        public int? intManufacturerID { get; set; }
        [Column(Name = "intSMCSFamilyID")]
        public int? intSMCSFamilyID { get; set; }

        [Column(Name = "strImage")]
        public string strImage { get; set; }
        [Association(ThisKey = "intManufacturerID", OtherKey = "intManufacturerID")]
        public EntitySet<Manufacturer> Manufactures { get; set; }
        [Association(ThisKey = "intModelID", OtherKey = "intModelID")]
        public EntitySet<newEquipment> newEquipments { get; set; }
    }
}
