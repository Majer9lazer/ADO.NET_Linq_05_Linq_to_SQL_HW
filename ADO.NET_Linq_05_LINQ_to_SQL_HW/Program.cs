using ADO.NET_Linq_05_LINQ_to_SQL_HW.Model;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_Linq_05_LINQ_to_SQL_HW
{
    class Program
    {
        private static LocaDb db = new LocaDb();
        static void Main(string[] args)
        {
            try
            {

                #region Для проверки на связку таблиц

                //var query1 = from e in NewEquipments
                //             select
                //             from t in e.TableEquipmentHistories
                //             where t.intEquipmentID == e.intEquipmentID
                //             select new
                //             {
                //                 e.intGarageRoom,
                //                 e.strSerialNo,
                //                 t.intTypeHistory,
                //                 t.dStartDate,
                //                 t.dEndDate,
                //                 t.intDaysCount,
                //                 t.intStatys
                //             };
                #endregion
                //Всё окей

                Task_05();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
        static void Task_02()
        {

            Table<TableEquipmentHistory> TableEquipmentHistoryies =
                            db.GetTable<TableEquipmentHistory>();
            Table<newEquipment> newEq = db.GetTable<newEquipment>();


            //Ничерта не выходит
            var NewEquipments = newEq.Select(s => s).ToList();
            var Histories = TableEquipmentHistoryies.Select(s => s).ToList();

            var query = from equipment in NewEquipments
                        join history in Histories
                        on equipment.intEquipmentID
                        equals history.intEquipmentID
                        orderby equipment.intGarageRoom
                        select new
                        {
                            equipment.intGarageRoom,
                            equipment.strSerialNo,
                            history.intTypeHistory,
                            history.dStartDate,
                            history.dEndDate,
                            history.intDaysCount,
                            history.intStatys

                        };
            var subQ = query.ToList();
            foreach (var item in subQ)
            {
                Console.WriteLine("intGarageRoom : {0}\n" +
                    "strSerialNo : {1}\n" +
                    "dStartDate : {2}\n" +
                    "dEndDate : {3}\n" +
                    "intDaysCount : {4}\n" +
                    "intStatys : {5}\n"

                    , item.intGarageRoom,
                    item.strSerialNo,
                    item.dStartDate,
                    item.dEndDate,
                    item.intDaysCount,
                    item.intStatys);
            }

        }
        static void Task_05()
        {
            Table<Manufacturer> manufactures = db.GetTable<Manufacturer>();
            var a = manufactures.Select(s=>s).ToList();
            var b = db.GetTable<TablesModel>().Select(s => s).ToList();
            var query
                = from m in a
                  select
                  from mo in m.TablesModels
                  select new
                  {

                      m.strName,
                      m.ManufacturerDescription,
                      mo.intManufacturerID,
                      mo.strImage

                  };
            foreach (var item in query)
            {
                foreach (var item2 in item)
                {
                    Console.WriteLine("{0\n{1}\n{2}\n{3}\n{4}\n", 
                        item2.intManufacturerID,
                        item2.ManufacturerDescription,
                        item2.strImage,
                        item2.strName);
                }
            }
        }
    }
}
