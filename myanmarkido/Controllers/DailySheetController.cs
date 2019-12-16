using myanmarkido.Models;
using myanmarkido.Repository;

using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;


namespace myanmarkido.Controllers
{
    public class DailySheetController : Controller
    {
        // GET: DailySheet
        public MemberCookie Getmember()
        {
            MemberCookie mc = new MemberCookie();

            if (Request.Cookies["dailyCookie"] != null)
            {
                mc.MemberID = Convert.ToInt32(Request.Cookies["dailyCookie"]["EmpId"]);
                mc.Role = Request.Cookies["dailyCookie"]["Role"];
                mc.Name = Request.Cookies["dailyCookie"]["UserName"];
                mc.Positon = Request.Cookies["dailyCookie"]["Position"];

            }


            return mc;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddDailySheet()
        {
            MemberCookie mc = Getmember();
            ViewBag.EmpId = mc.MemberID;
            ViewBag.Name = mc.Name;
            ViewBag.Position = mc.Positon;
            return View();

        }
        public ActionResult dailyresult()
        {
            MemberCookie mc = Getmember();
            dailysheetcontext context = new dailysheetcontext();
            var result = context.Dailyjobset.Where(a => a.EmpId == mc.MemberID).ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SendDailySheet(dailyjob job)
        {

            dailysheetcontext context = new dailysheetcontext();
            dailyjobRepository dailyrepo = new dailyjobRepository();
            dailyjob updatejob = null;
            
            if (job.JobId == 0)
            {
                var Rhour = Convert.ToDouble((job.Rhour.Hours)+(job.Rhour.Minutes) / 60.0);
                var Ehour = Convert.ToDouble((job.Ehour.Hours) + (job.Ehour.Minutes) / 60.0);
                var Whour = Convert.ToDouble((job.Whour.Hours) + (job.Whour.Minutes)/ 60.0);
                var Mhour = Convert.ToDouble((job.Mhour.Hours) + (job.Mhour.Minutes)/ 60.0);
                job.Revenuehour = Math.Round(Rhour, 2); ;
                job.Expenseshour = Math.Round(Ehour, 2); ;
                job.Warrantyhour = Math.Round(Whour, 2);
                job.Maintenancehour = Math.Round(Mhour,2);
                updatejob = dailyrepo.Add(job);
            }
            else
            {
                var Rhour = Convert.ToDouble((job.Rhour.Hours) + (job.Rhour.Minutes) / 60.0);
                var Ehour = Convert.ToDouble((job.Ehour.Hours) + (job.Ehour.Minutes) / 60.0);
                var Whour = Convert.ToDouble((job.Whour.Hours) + (job.Whour.Minutes)/60.0);
                var Mhour = Convert.ToDouble((job.Mhour.Hours) + (job.Mhour.Minutes)/ 60.0);
                job.Revenuehour = Math.Round(Rhour, 2); ;
                job.Expenseshour = Math.Round(Ehour, 2); ;
                job.Warrantyhour = Math.Round(Whour, 2);
                job.Maintenancehour = Math.Round(Mhour, 2);
                updatejob = dailyrepo.Update(job);
            }

            return RedirectToAction("AddDailySheet", "DailySheet");
        }
        public ActionResult Detailsheet(int? JobId)
        {
            dailysheetcontext context = new dailysheetcontext();
            var result = context.Dailyjobset.Where(a => a.JobId == JobId).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DetailData(int? JobId)
        {
            dailysheetcontext context = new dailysheetcontext();
            var result = context.Dailyjobset.Where(a => a.JobId == JobId).ToList();

            return View(result);
        }
        public ActionResult AdminJob()
        {
            return View();
        }
        public ActionResult EmployeeList(int page = 1, int pagesize = 10)
        {
            dailysheetcontext context = new dailysheetcontext();
            var model = new EmployeePaging();
            Account[] employeelist = context.Accountset.ToArray();
            var totalcount = employeelist.Count();
            var totalpage = (int)Math.Ceiling((double)totalcount / pagesize);
            var pagedList = new StaticPagedList<Account>(employeelist, page, pagesize, totalcount);
            model.employeeList = pagedList;
            model.TotalCount = totalcount;
            model.TotalPages = totalpage;
            return View(model);
        }
        public ActionResult Detail(int EmpId)
        {
            dailysheetcontext context = new dailysheetcontext();
            var result = context.Accountset.Where(e => e.EmpId == EmpId).FirstOrDefault();
            return View(result);
        }
        public ActionResult _dailylist(string n = "n", string Keyword = null, DateTime? fromdate = null, DateTime? todate = null, int page = 1, int pageSize = 10)
        {
            fromdate=(fromdate==null)? new DateTime(1800, 1, 1) : fromdate;
            todate = (todate == null) ? DateTime.UtcNow : todate;

            
            if (Keyword == null)
            {
                Keyword = "";
            }
            
            var param1 = new SqlParameter();
            param1.ParameterName = "@S";
            param1.SqlDbType = SqlDbType.DateTime;
            param1.SqlValue = fromdate;

            var param2 = new SqlParameter();
            param2.ParameterName = "@E";
            param2.SqlDbType = SqlDbType.DateTime;
            param2.SqlValue = todate;

            var param3 = new SqlParameter();
            param3.ParameterName = "@K";
            param3.SqlDbType = SqlDbType.VarChar;
            param3.SqlValue = Keyword;

          

           
            using (dailysheetcontext context = new dailysheetcontext())
            {
                
                IEnumerable <performanace> data = context.Database.SqlQuery<performanace>("DetailList @S,@E,@K", param1, param2, param3).ToList();

               
               
                var totalCount = data.Count();
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                var results = data.Skip(pageSize * (page - 1))
                                 .Take(pageSize);
                Model<performanace> model = new Model<performanace>()
                {
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    Results = results.ToArray()
                };
                ModelPaging<performanace> result = new ModelPaging<performanace>();
                var pagedList = new StaticPagedList<performanace>(model.Results, page, pageSize, model.TotalCount);
                result.Results = pagedList;
                result.TotalCount = model.TotalCount;
                result.TotalPages = model.TotalPages;


                return PartialView("_dailylist", result);

            }


        }

        public ActionResult ExportListToExcel(DateTime? fromdate, DateTime? todate, string keyword = null)
        {

            fromdate = (fromdate == null) ? new DateTime(1970, 1, 1) : fromdate;
            todate = (todate == null) ? DateTime.UtcNow : todate;

            if (keyword == null)
            {
                keyword = "";
            }

            var param1 = new SqlParameter();
            param1.ParameterName = "@S";
            param1.SqlDbType = SqlDbType.DateTime;
            param1.SqlValue = fromdate;

            var param2 = new SqlParameter();
            param2.ParameterName = "@E";
            param2.SqlDbType = SqlDbType.DateTime;
            param2.SqlValue = todate;

            var param3 = new SqlParameter();
            param3.ParameterName = "@K";
            param3.SqlDbType = SqlDbType.VarChar;
            param3.SqlValue = keyword;




            dailysheetcontext context = new dailysheetcontext();


            IEnumerable<performanace> data = context.Database.SqlQuery<performanace>("DetailList @S,@E,@K", param1, param2, param3).ToList();

            //    GridView gv = new GridView();

            //gv.DataSource = data;
            //gv.DataBind();
            //Response.ClearContent();
            //Response.Buffer = true;
            //string strDateFormat = string.Empty;
            //strDateFormat = string.Format("{0:yyyy-MMM-dd-hh-mm-ss}", DateTime.Now);
            //Response.AddHeader("content-disposition", "attachment; filename=UserDetails_" + strDateFormat + ".xls");
            //Response.ContentType = "application/ms-excel";
            //Response.Charset = "";
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter htw = new HtmlTextWriter(sw);
            //gv.RenderControl(htw);
            //Response.Output.Write(sw.ToString());
            //Response.Flush();
            //Response.End();


            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report");
            Sheet.Cells["D1:G1"].Merge=true;
            Sheet.Cells["D1"].Value = "Revenue(R1~5)";

            Sheet.Cells["H1:M1"].Merge = true;
            Sheet.Cells["H1"].Value = "Maintenance(M1~5)";

            Sheet.Cells["N1:R1"].Merge = true;
            Sheet.Cells["N1"].Value = "Warranty(D1~5)";

            Sheet.Cells["S1:AE1"].Merge = true;
            Sheet.Cells["S1"].Value = "Expenses(E)";

            Sheet.Cells["A2"].Value = "Name";
            Sheet.Cells["B2"].Value = "Position";
            Sheet.Cells["C2"].Value = "Date";
            Sheet.Cells["D2"].Value = "Actual Working hr";
            Sheet.Cells["E2"].Value = "Travelling hr";
            Sheet.Cells["F2"].Value = "Overtime";
            Sheet.Cells["G2"].Value = "Preparation";
            Sheet.Cells["H2"].Value = "Spent hr";
            Sheet.Cells["I2"].Value = "Actual Working hr";
            Sheet.Cells["J2"].Value = "Travelling hr";
            Sheet.Cells["K2"].Value = "Overtime";
            Sheet.Cells["L2"].Value = "Preparation";
            Sheet.Cells["M2"].Value = "Spent hr";
            Sheet.Cells["N2"].Value = "Actual Working hr";
            Sheet.Cells["O2"].Value = "Travelling hr";
            Sheet.Cells["P2"].Value = "Overtime";
            Sheet.Cells["Q2"].Value = "Preparation";
            Sheet.Cells["R2"].Value = "Spent hr";
            Sheet.Cells["S2"].Value = "Repair of Service Car(E01)";
            Sheet.Cells["T2"].Value = "Repair Service Tools(E02)";
            Sheet.Cells["U2"].Value = "Rework(E03)";
            Sheet.Cells["V2"].Value = "Service Policy(E04)";
            Sheet.Cells["W2"].Value = "Training(E05)";
            Sheet.Cells["X2"].Value = "Voluntary Visit(E06)";
            Sheet.Cells["Y2"].Value = "Non Productive(E07)";
            Sheet.Cells["Z2"].Value = "Travelling(E08)";
            Sheet.Cells["AA2"].Value = "Paper Works(E09)";
            Sheet.Cells["AB2"].Value = "Leave(E10)";
            Sheet.Cells["AC2"].Value = "Holiday(E11)";
            Sheet.Cells["AD2"].Value = "Jobless(E12)";
            Sheet.Cells["AE2"].Value = "Knowledge of service(E13)";
            int row = 3;
            foreach (var item in data)
            {

                Sheet.Cells[string.Format("A{0}", row)].Value = item.Name;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.Position;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.Date;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.RAHour;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.ROHour;
                Sheet.Cells[string.Format("F{0}", row)].Value = item.RPHour;
                Sheet.Cells[string.Format("G{0}", row)].Value = item.RTHour;
                Sheet.Cells[string.Format("H{0}", row)].Value = item.RSHour;

                Sheet.Cells[string.Format("I{0}", row)].Value = item.MAHour;
                Sheet.Cells[string.Format("J{0}", row)].Value = item.MOHour;
                Sheet.Cells[string.Format("K{0}", row)].Value = item.MPHour;
                Sheet.Cells[string.Format("L{0}", row)].Value = item.MTHour;
                Sheet.Cells[string.Format("M{0}", row)].Value = item.MSHour;

                Sheet.Cells[string.Format("N{0}", row)].Value = item.WAHour;
                Sheet.Cells[string.Format("O{0}", row)].Value = item.WOHour;
                Sheet.Cells[string.Format("P{0}", row)].Value = item.WPHour;
                Sheet.Cells[string.Format("Q{0}", row)].Value = item.WTHour;
                Sheet.Cells[string.Format("R{0}", row)].Value = item.WSHour;

                Sheet.Cells[string.Format("S{0}", row)].Value = item.ECHour;
                Sheet.Cells[string.Format("T{0}", row)].Value = item.EHHour;
                Sheet.Cells[string.Format("U{0}", row)].Value = item.EJHour;
                Sheet.Cells[string.Format("V{0}", row)].Value = item.EKHour;
                Sheet.Cells[string.Format("W{0}", row)].Value = item.ELHour;
                Sheet.Cells[string.Format("X{0}", row)].Value = item.ENHour;
                Sheet.Cells[string.Format("Y{0}", row)].Value = item.EPHour;
                Sheet.Cells[string.Format("Z{0}", row)].Value = item.EPWHour;
                Sheet.Cells[string.Format("AA{0}", row)].Value = item.ERHour;
                Sheet.Cells[string.Format("AB{0}", row)].Value = item.ETOHour;
                Sheet.Cells[string.Format("AC{0}", row)].Value = item.ETRHour;
                Sheet.Cells[string.Format("AD{0}", row)].Value = item.EVHour;
                Sheet.Cells[string.Format("AE{0}", row)].Value = item.ETHour;

                row++;
            }


            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;  filename=Report.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();


            return RedirectToAction("AdminJob", "DailySheet");
            
        }
   
       
      
        public ActionResult _Detail(string position,string name, DateTime? fromdate, DateTime? todate)
        {
            MemberCookie pc = Getmember();
            fromdate = (fromdate == null) ? new DateTime(1970, 1, 1) : fromdate;
            todate = (todate == null) ? DateTime.UtcNow : todate;
            dailysheetcontext context = new dailysheetcontext();
          
            IEnumerable<dailyjob> result = context.Dailyjobset.Where(a => a.Position == position && a.Name == name && a.date >= fromdate && a.date <= todate).OrderByDescending(a => a.date).ToList();
            

            Detailjson data = new Detailjson();

            data.date = result.Select(e => e.date.Value.ToString("MMMM dd/ yyyy")).ToArray();
            data.Revenuetye = result.Select(e => e.Revenuetype).ToArray();
            data.Rdescription = result.Select(e => e.Rdescription).ToArray();
            data.Revenuehour = result.Select(e => e.Revenuehour).ToArray();
            data.RJobNo = result.Select(e => e.RJobNo).ToArray();
            data.Maintenancetype = result.Select(e => e.Maintenancetype).ToArray();
            data.Mdescription = result.Select(e => e.Mdescription).ToArray();
            data.Maintenancehour = result.Select(e => e.Maintenancehour).ToArray();
            data.MJobNo = result.Select(e => e.MJobNo).ToArray();
            data.Warrantytype = result.Select(e => e.Warrantytype).ToArray();
            data.Wdescription = result.Select(e => e.Wdescription).ToArray();
            data.Warrantyhour = result.Select(e => e.Warrantyhour).ToArray();
            data.WJobNo = result.Select(e => e.WJobNo).ToArray();
            data.Expensetype = result.Select(e => e.Expensestype).ToArray();
            data.Edescription = result.Select(e => e.Edescritption).ToArray();
            data.Expensehour = result.Select(e => e.Expenseshour).ToArray();
           

            ViewBag.position = position;
            ViewBag.name = name;
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult DeleteEmployee(int EmpId)
        {
            using (dailysheetcontext context = new dailysheetcontext())
            {
                var sqlquery = String.Format("Delete Account WHERE EmpId={0}", EmpId);
              
                context.Database.ExecuteSqlCommand(sqlquery);
               
            }
            
            return Json("Success", JsonRequestBehavior.AllowGet);

        }
    }
}