using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

using Sales_Rep.Models;

namespace Sales_Rep.Reports
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var id = Request.QueryString["id"];

            if (!Page.IsPostBack)
            {
                if (id == "1")
                {
                    using (SALES_REPEntities db = new SALES_REPEntities())
                    {

                        GLListReportViewer.LocalReport.ReportPath = Server.MapPath("~/Reports/Report_Reorder.rdlc");
                        GLListReportViewer.LocalReport.DataSources.Clear();

                        ReportDataSource rdc1 = new ReportDataSource("DataSet_Reaorder", db.tblProducts.Where(x=>x.reorder>= x.qty).ToList());

                        GLListReportViewer.LocalReport.DataSources.Add(rdc1);

                    }

                    GLListReportViewer.LocalReport.Refresh();
                    GLListReportViewer.DataBind();
                }

                else if (id == "2")
                {

                    string searchText = string.Empty;
                    string searchTextTwo = string.Empty;


                    string st = "0";
                    string st1 = "0";



                    if (Request.QueryString["searchText"] != null && Request.QueryString["searchText1"] != null)
                    {
                        searchText = Request.QueryString["searchText"].ToString();
                        searchTextTwo = Request.QueryString["searchText1"].ToString();


                        st = searchText;
                        st1 = searchTextTwo;


                    }



                    using (SALES_REPEntities db = new SALES_REPEntities())
                    {
                        DateTime dt = Convert.ToDateTime(st);
                        DateTime dt1 = Convert.ToDateTime(st1);

                        GLListReportViewer.LocalReport.ReportPath = Server.MapPath("~/Reports/ReportSale.rdlc");
                        GLListReportViewer.LocalReport.DataSources.Clear();

                        ReportDataSource rdc1 = new ReportDataSource("DataSetSale", db.tblCarts.Where(x=>x.sdate>=dt && x.sdate<=dt1).ToList());




                        GLListReportViewer.LocalReport.DataSources.Add(rdc1);


                    }




                    GLListReportViewer.LocalReport.Refresh();
                    GLListReportViewer.DataBind();
                }

              else  if (id == "3")
                {
                    using (SALES_REPEntities db = new SALES_REPEntities())
                    {

                        GLListReportViewer.LocalReport.ReportPath = Server.MapPath("~/Reports/ReportSalesRep.rdlc");
                        GLListReportViewer.LocalReport.DataSources.Clear();

                        ReportDataSource rdc1 = new ReportDataSource("DataSetSalesRep", db.tblSalesReps.Where(x=>x.Status==0).ToList());

                        GLListReportViewer.LocalReport.DataSources.Add(rdc1);

                    }

                    GLListReportViewer.LocalReport.Refresh();
                    GLListReportViewer.DataBind();
                }

            }
        }
    }
}