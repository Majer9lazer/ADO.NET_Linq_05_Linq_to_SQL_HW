using ADO.NET_Linq_05_LINQ_to_SQL_HW.Model;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ADO.NET_Linq_05_LINQ_to_SQL_HW
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowMenu();
        }
        private static void ShowMenu()
        {
            try
            {
                start:
                for (int i = 2; i <= 7; i++)
                {
                    Console.WriteLine("{0} - Task {0}", i);
                }
                int option = 8;
                Console.WriteLine("1 - Exit");
                int.TryParse(Console.ReadLine(), out option);
                switch (option)
                {

                    case 1: { Console.Clear(); break; }
                    case 2:
                        {
                            Console.Clear();
                            Console.WriteLine("Выполняем задание - {0}", option);
                            EgorStructure.Task_02();
                            goto start;
                        }
                    case 3:
                        {
                            Console.Clear();
                            Console.WriteLine("Выполняем задание - {0}", option);
                            Console.WriteLine("Придется запустить задание под номером 2 так как они оба связаны");
                            EgorStructure.Task_03(EgorStructure.Task_02());
                            goto start;
                        }
                    case 4:
                        {
                            Console.Clear();
                            Console.WriteLine("Выполняем задание - {0}", option);
                            EgorStructure.Task_04(); goto start;

                        }
                    case 5:
                        {
                            Console.Clear();
                            Console.WriteLine("Выполняем задание - {0}", option);
                            Console.WriteLine("1 - Использовать оптимальную загрузку данных\n2 -Использовать отложенную загрузку данных");
                            int.TryParse(Console.ReadLine(), out option);
                            EgorStructure.Task_05(option);
                            goto start;
                        }
                    case 6:
                        {
                            Console.Clear();
                            Console.WriteLine("Выполняем задание - {0}", option);
                            EgorStructure.Task_06();
                            goto start;
                        }
                    case 7:
                        {
                            Console.Clear();
                            Console.WriteLine("Выполняем задание - {0}", option);
                            EgorStructure.Task_07();
                            goto start;
                        }
                    default:
                        Console.WriteLine("Вы выбрали пункт которого нет в списке!");
                        break;
                }
              
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

    }
    struct EgorStructure
    {
        private static LocaDb db = new LocaDb();
        private static string _connstring = "";
        public static void Task_04()
        {
            _connstring = ConfigurationManager.ConnectionStrings["LocaDb"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(_connstring))
            {
                string date = string.Format("{0:yyyy/dd/mm}", DateTime.Now.AddDays(1));
                date = date.Replace('.', '-');
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "insert into newEquipment" +
                    "(intManufacturerID,intModelID,intSNPrefixID,intEquipmentTypeID,intSMCSFamilyID,intSizeID," +
                    "intMetered,LastDate,intLastMetered,intTotalMetered,bitActive,bitPreservation,bitMeter," +
                    "bitKTG,isDelete,intLocationId,bitMethodAmortization)" +
                    "values(1,1,119,29,1,3,9999,'" + date + "',1212,454,1,0,0,0,0,4,0)" +
                    "insert into Manufacturer(strName) values('Audi')" +
                      "insert into Manufacturer(strName) values('BMW')" +
                        "insert into Manufacturer(strName) values('KIA')" +
                          "insert into Manufacturer(strName) values('JEEP')";

                cmd.Connection = conn;
                cmd.ExecuteNonQuery();

                Console.WriteLine("Добавление прошло успешно!!");
            }
        }
        public static List<EquipmentAndHistoryUnion> Task_02()
        {
            Table<TableEquipmentHistory> TableEquipmentHistoryies =
                            db.GetTable<TableEquipmentHistory>();
            Table<newEquipment> newEq = db.GetTable<newEquipment>();
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
            List<EquipmentAndHistoryUnion> unlist = new List<EquipmentAndHistoryUnion>();
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

                EquipmentAndHistoryUnion e = new EquipmentAndHistoryUnion();
                e.intGarageRoom = item.intGarageRoom;
                e.strSerialNo = item.strSerialNo;
                e.dStartDate = item.dStartDate;
                e.dEndDate = item.dEndDate;
                e.intDaysCount = item.intDaysCount;
                e.intStatys = item.intStatys;
                unlist.Add(e);
            }
            return unlist;
        }
        public static void Task_05(int option)
        {
            Table<Manufacturer> manufactures = db.GetTable<Manufacturer>();
            if (option == 1)
            {

                var a = manufactures.Select(s => s).ToList();
                var b = db.GetTable<TablesModel>().Select(s => s).ToList();
                var query
                    = from m in a
                      select
                      from mo in m.TablesModels
                      orderby mo.intModelID
                      select new
                      {

                          m.strName,
                          m.ManufacturerDescription,
                          mo.intManufacturerID,
                          mo.strImage,
                          mo.intModelID

                      };
                Console.WriteLine("Используем оптимальную выгрузку данных");
                Console.WriteLine("--------------------");
                foreach (var item in query)
                {
                    foreach (var item2 in item)
                    {
                        Console.WriteLine("intModelID = {0}\nintManufacturerID = {1}\nManufacturerDescription = {2}\nstrImage = {3}\nstrName = {4}",
                           item2.intModelID,
                            item2.intManufacturerID,
                            item2.ManufacturerDescription,
                            item2.strImage,
                            item2.strName);
                        Console.WriteLine("--------------------");
                    }

                }
                Console.WriteLine();
            }
            else if (option == 2)
            {
                Console.WriteLine("Используем отложенную загрузку данных");
                Console.WriteLine("--------------------");
                foreach (Manufacturer m in manufactures)
                {
                    foreach (TablesModel item in m.TablesModels)
                    {
                        Console.WriteLine("intModelID = {0}\nintManufacturerID = {1}\nManufacturerDescription = {2}\nstrImage = {3}\nstrName = {4}",
                             item.intModelID,
                              m.intManufacturerID,
                              m.ManufacturerDescription,
                              item.strImage,
                              item.strName);
                        Console.WriteLine("--------------------");
                    }
                }
            }
        }
        public static void Task_07()
        {
            Console.WriteLine("Удаляем только те поля где dEndDate=null...");
            List<TableEquipmentHistory> query = db.GetTable<TableEquipmentHistory>().Where(w => w.dEndDate == null).ToList();
            db.TableEquipmentHistories = db.GetTable<TableEquipmentHistory>();
            db.TableEquipmentHistories.DeleteAllOnSubmit(query);
            db.SubmitChanges();
            Console.WriteLine("Удаление прошло успешно)))");
        }
        public static void Task_03(List<EquipmentAndHistoryUnion> unlist)
        {
            if (unlist.Count != 0)
            {
                db.EquipmentAndHistoryUnions = db.GetTable<EquipmentAndHistoryUnion>();
                db.EquipmentAndHistoryUnions.InsertAllOnSubmit((unlist));
                db.SubmitChanges();
                Console.WriteLine("Данные в другую таблицу бы внесены успешно");
            }
            else
            {
                Console.WriteLine("Данные не  пришли!");
            }
        }
        public static void Task_06()
        {
            List<newEquipment> equipments = db.GetTable<newEquipment>().OrderBy(o => o.intEquipmentID).Select(s => s).ToList();
            List<TablesModel> models = db.GetTable<TablesModel>().Where(w => w.intModelID > 10).Select(s => s).ToList();
            var modelId = models.Select(s => s.intModelID).ToList();
            int[] arr = new int[modelId.Count()];
            var equ = equipments.Where(w => modelId.Contains(w.intModelID)).ToList();

            Console.WriteLine("--------------------------");
            foreach (newEquipment item in equ)
            {
                Console.WriteLine("intEquipmentID = {0}\nintGarageRoom = {1}\nintModelID = {2}\nstrManufYear = {3}", item.intEquipmentID,
                    item.intGarageRoom, item.intModelID, item.strManufYear);
                Console.WriteLine("--------------------------");
            }
            Console.WriteLine();
        }
    }
}
