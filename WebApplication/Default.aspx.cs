using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Load the headers
        LoadTableHeaders();
        LoadRows();
        Table_Tasks.CssClass = "table table-bordered";
    }

    private void LoadTableHeaders()
    {
        TableHeaderRow HeaderRow = new TableHeaderRow();
        HeaderRow.CssClass = "TableHeaderRow";
        // Create TableCell objects to contain

        TableHeaderCell headerTableCell1 = new TableHeaderCell();
        headerTableCell1.Text = "<h1>Opgave</h1>";
        //headerTableCell1.Scope = TableHeaderScope.Column;
        headerTableCell1.AbbreviatedText = "Opgave";
        headerTableCell1.CssClass = "TableHeaderCellRow1";

        TableHeaderCell headerTableCell2 = new TableHeaderCell();
        headerTableCell2.Text = "<h1>Action</h1>";
        //headerTableCell2.Scope = TableHeaderScope.Column;
        headerTableCell2.AbbreviatedText = "Action";
        headerTableCell2.CssClass = "TableHeaderCellRow2";

        TableHeaderCell headerTableCell3 = new TableHeaderCell();
        headerTableCell3.Text = "<h1>Forventet <br /> Start</h1>";
        //headerTableCell3.Scope = TableHeaderScope.Column;
        headerTableCell3.AbbreviatedText = "Start";
        headerTableCell3.CssClass = "TableHeaderCellRow3";

        TableHeaderCell headerTableCell4 = new TableHeaderCell();
        headerTableCell4.Text = "<h1>Personale</h1>";
        //headerTableCell4.Scope = TableHeaderScope.Column;
        headerTableCell4.AbbreviatedText = "Personale";
        headerTableCell4.CssClass = "TableHeaderCellRow4";


        //Vertial Coloumns
        TableHeaderCell headerTableCell5 = new TableHeaderCell();
        headerTableCell5.Text = "<p>Igang</p>";
        //headerTableCell5.Scope = TableHeaderScope.Column;
        headerTableCell5.AbbreviatedText = "Igang";
        headerTableCell5.CssClass = "vertical-text-table-purple vertical-text-table";

        TableHeaderCell headerTableCell6 = new TableHeaderCell();
        headerTableCell6.Text = "<p>Standby</p>";
        //headerTableCell6.Scope = TableHeaderScope.Column;
        headerTableCell6.AbbreviatedText = "Stand";
        headerTableCell6.CssClass = "vertical-text-table-yellow vertical-text-table";

        TableHeaderCell headerTableCell7 = new TableHeaderCell();
        headerTableCell7.Text = "<p>Færdig</p>";
        //headerTableCell7.Scope = TableHeaderScope.Column;
        headerTableCell7.AbbreviatedText = "Færdig";
        headerTableCell7.CssClass = "vertical-text-table-green vertical-text-table";

        TableHeaderCell headerTableCell8 = new TableHeaderCell();
        headerTableCell8.Text = "<p>Afventer. Godk.</p>";
        //headerTableCell8.Scope = TableHeaderScope.Column;
        headerTableCell8.AbbreviatedText = "Afv. Godk.";
        headerTableCell8.CssClass = "vertical-text-table-red vertical-text-table";

        // Add the TableHeaderCell objects to the Cells
        // collection of the TableHeaderRow.
        HeaderRow.Cells.Add(headerTableCell1);
        HeaderRow.Cells.Add(headerTableCell2);
        HeaderRow.Cells.Add(headerTableCell3);
        HeaderRow.Cells.Add(headerTableCell4);
        HeaderRow.Cells.Add(headerTableCell5);
        HeaderRow.Cells.Add(headerTableCell6);
        HeaderRow.Cells.Add(headerTableCell7);
        HeaderRow.Cells.Add(headerTableCell8);
        //HeaderRow.Cells.Add(headerTableCell9);

        // Add the TableHeaderRow as the first item 
        // in the Rows collection of the table.
        Table_Tasks.Rows.AddAt(0, HeaderRow);
    }


    private void LoadRows()
    {
        using (var db = new ProjectManagerEntities())
        {
            var query = from Inquiry in db.Tasks
                        select Inquiry;

            foreach (var item in query)
            {

                //IF we find a big task, go through all the items again, and find the items that have MainTask with ID = to BigTask?? (Might cause slowdowns)
                TableRow TempRow = new TableRow();

                if (item.Task_IsBigTask == true)
                {
                    TempRow.BackColor = System.Drawing.Color.Orange;
                }
                else
                {
                    TempRow.BackColor = System.Drawing.Color.Cornsilk;
                }

                TableCell Cell_TaskName = new TableCell();
                Cell_TaskName.Text = item.Task_Name;
                TempRow.Cells.Add(Cell_TaskName);

                TableCell Cell_TaskAction = new TableCell();
                Cell_TaskAction.Text = item.Task_Action;
                TempRow.Cells.Add(Cell_TaskAction);

                TableCell Cell_TaskStart = new TableCell();
                if (item.Task_Start != null)
                {
                    Cell_TaskStart.Text = item.Task_Start.ToString();
                }
                else
                {
                    Cell_TaskStart.Text = "";
                }
                TempRow.Cells.Add(Cell_TaskStart);

                TableCell Cell_TaskStaff = new TableCell();
                Cell_TaskStaff.Text = item.Task_Staff;
                TempRow.Cells.Add(Cell_TaskStaff);

                Table_Tasks.Rows.Add(TempRow);
            }
                
                /*from Inquiry in db.Bestillling
                       where Inquiry.Bestillingstype.ID == (int)Enums.BestillingsType.Foredrag
                       && Inquiry.approval == true
                       && Inquiry.startDato >= DateTime.Now
                       select Inquiry;*/
        }
            //This will be where the logic for adding rows to the table will be.
            //Add rows to the table.
            //for (int rowNum = 0; rowNum < 100; rowNum++)
            //{
            //    TableRow tempRow = new TableRow();
            //    for (int cellNum = 0; cellNum < 8; cellNum++)
            //    {
            //        TableCell tempCell = new TableCell();
            //        tempCell.Text = String.Format("({0},{1})", rowNum, cellNum);
            //        tempRow.Cells.Add(tempCell);
            //    }
            //    Table_Tasks.Rows.Add(tempRow);
            //}
    }
}