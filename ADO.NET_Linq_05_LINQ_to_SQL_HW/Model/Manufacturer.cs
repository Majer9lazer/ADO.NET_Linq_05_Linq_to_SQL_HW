namespace ADO.NET_Linq_05_LINQ_to_SQL_HW.Model
{
    using System;
    using System.Collections.Generic;
    using System.Data.Linq.Mapping;
    using System.Data.Linq;
    [Table(Name = "Manufacturer")]

    public class Manufacturer
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int intManufacturerID { get; set; }

        [Column(Name = "strName")]
        public string strName { get; set; }

        [Column(Name = "strManufacturerChecklistId")]
        public string strManufacturerChecklistId { get; set; }

        [Column(Name = "ManufacturerDescription")]
        public string ManufacturerDescription { get; set; }
        [Association(ThisKey = "intManufacturerID", OtherKey = "intManufacturerID")]
        public EntitySet<TablesModel> TablesModels { get; set; }

    }
}
