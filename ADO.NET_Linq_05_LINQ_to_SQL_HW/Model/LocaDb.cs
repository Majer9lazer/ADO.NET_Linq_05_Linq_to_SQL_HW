namespace ADO.NET_Linq_05_LINQ_to_SQL_HW.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Linq;

    public partial class LocaDb : DataContext
    {
        public LocaDb()
            : base(@"data source=403_4_66\SQLEXPRESS;initial catalog=MCS;integrated security=True")
        {
        }

        public Table<Manufacturer> Manufacturers { get; set; }
        public Table<newEquipment> newEquipments { get; set; }
        public Table<TableEquipmentHistory> TableEquipmentHistories { get; set; }
        public Table<TablesModel> TablesModels { get; set; }

       public Table<EquipmentAndHistoryUnion> EquipmentAndHistoryUnions { get; set; }
    }
}
