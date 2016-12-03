using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

public partial class _Default : System.Web.UI.Page
{
    bool AllowEdits = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        //When we load items from the database on load, but never get rid of them, we allocate memory but never clear it. Possible memory leak on bigger system?
        Table_Tasks.CssClass = "table table-bordered TableChanges";
        //Load the headers
        LoadTableHeaders();
        //Possibly make this a feature for JavaScript to handle, as this would allow individual clients to change what they see. (Instead of changing all)
        //Security wise, disabling this will make sure nobody gets access!
        //Maybe the same with undertasks?
        if (AllowEdits == false)
        {
            Controls.Visible = false;
            RowNewTaskButton.Visible = false;
            InputRow.Visible = false;
        }
        LoadPriortyRows(true, true, AllowEdits);
        LoadRows(true, AllowEdits);
        LoadDepartments();
    }
    
    public void LoadDepartments()
    {
        Input_Task_Department.Items.Clear();
        Input_Task_Department.Items.Add("Ingen");
        using (var db = new ProjectManagerEntities())
        {
            var query = from Inquiry in db.Departments
                        select Inquiry;

            foreach (var Department in query)
            {
                Input_Task_Department.Items.Add(Department.Department_Name);
            }
        }
    }

    [WebMethod]
    public static void UpdateStatus(int ID, int NewStatus) //Example on website uses a string? Not sure why. Person also does manual con string.
    {
        //THIS IS POSSIBLY STUPIDLY DANGEROUS AND EASY TO ABUSE!!
        //Thinking about it, it's possible to execute injections with this?
        
        using (var db = new ProjectManagerEntities())
        {
            var query = from Inquiry in db.Tasks
                        where Inquiry.Task_ID == ID
                        select Inquiry;

            foreach (var Task in query)
            {
                if (NewStatus == 0)
                {
                    Task.Task_ApprovedDate = DateTime.Now;
                }
                if (NewStatus == 2)
                {
                    Task.Task_CompletionDate = DateTime.Now;
                }
                Task.Task_Status = NewStatus;
            }

            //Possible connect this so it only savechanges when user clicks the buttons, 
            //requires redesign of all buttons though to make them inputs to aboid page refresh on click.
            db.SaveChanges();
        }
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


        //To make them all the same, make some images to put in, instead of the text to make it the same size!
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

        TableHeaderCell headerTableCell9 = new TableHeaderCell();
        headerTableCell9.CssClass = "vertical-text-table-default vertical-text-table";
        headerTableCell9.Text = "<p>Udfold</p>";

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
        //Collapse undertasks Cell
        HeaderRow.Cells.Add(headerTableCell9);

        // Add the TableHeaderRow as the first item 
        // in the Rows collection of the table.
        Table_Tasks.Rows.AddAt(0, HeaderRow);
    }
    
    private void LoadPriortyRows(bool ShouldLoad = true, bool LoadSubTasks = true, bool AllowEdits = false)
    {
        if (ShouldLoad)
        {
            using (var db = new ProjectManagerEntities())
            {
                var query = from Inquiry in db.Tasks
                            where Inquiry.Task_IsPriority == true
                            orderby Inquiry.Task_Status
                            select Inquiry;

                foreach (var Task in query)
                {
                    TableRow TempRow = new TableRow();
                    TempRow.CssClass = "ImportantRow";

                    TableCell Cell_TaskName = new TableCell();
                    Cell_TaskName.Text = Task.Task_Name;
                    TempRow.Cells.Add(Cell_TaskName);

                    TableCell Cell_TaskAction = new TableCell();
                    Cell_TaskAction.Text = Task.Task_Action;
                    TempRow.Cells.Add(Cell_TaskAction);

                    TableCell Cell_TaskStart = new TableCell();
                    if (Task.Task_Start != null)
                    {
                        Cell_TaskStart.Text = Task.Task_Start.Value.ToShortDateString();
                    }
                    TempRow.Cells.Add(Cell_TaskStart);

                    TableCell Cell_TaskStaff = new TableCell();
                    Cell_TaskStaff.Text = Task.Task_Staff;
                    TempRow.Cells.Add(Cell_TaskStaff);

                    TableCell Status0 = new TableCell();
                    TableCell Status1 = new TableCell();
                    TableCell Status2 = new TableCell();
                    TableCell Status3 = new TableCell();
                    //These tablecells could contain buttons that have methods to update tasks


                    switch (Task.Task_Status)
                    {
                        case 0: //If task is Started (Igang)
                            Status0.BackColor = System.Drawing.Color.Purple;
                            if (AllowEdits)
                            {
                                Status1.Text = "<button class='TableButton' onclick='UpdateStatus(" + Task.Task_ID + ", " + 1 + ")'></button>";
                                Status2.Text = "<button class='TableButton' onclick='UpdateStatus(" + Task.Task_ID + ", " + 2 + ")'></button>";
                                Status3.Text = "<button class='TableButton' onclick='UpdateStatus(" + Task.Task_ID + ", " + 3 + ")'></button>";
                            }
                            break;
                        case 1: //If task is Standby
                            Status1.BackColor = System.Drawing.Color.Yellow;
                            if (AllowEdits)
                            {
                                Status0.Text = "<button class='TableButton' onclick='UpdateStatus(" + Task.Task_ID + ", " + 0 + ")'></button>";
                                Status2.Text = "<button class='TableButton' onclick='UpdateStatus(" + Task.Task_ID + ", " + 2 + ")'></button>";
                                Status3.Text = "<button class='TableButton' onclick='UpdateStatus(" + Task.Task_ID + ", " + 3 + ")'></button>";
                            }
                            break;
                        case 2: //If task is finished
                            Status2.BackColor = System.Drawing.Color.Green;
                            if (AllowEdits)
                            {
                                Status0.Text = "<button class='TableButton' onclick='UpdateStatus(" + Task.Task_ID + ", " + 0 + ")'></button>";
                                Status1.Text = "<button class='TableButton' onclick='UpdateStatus(" + Task.Task_ID + ", " + 1 + ")'></button>";
                                Status3.Text = "<button class='TableButton' onclick='UpdateStatus(" + Task.Task_ID + ", " + 3 + ")'></button>";
                            }
                            break;
                        case 3://If task is Awaiting Accept.
                            Status3.BackColor = System.Drawing.Color.Red;
                            if (AllowEdits)
                            {
                                Status0.Text = "<button class='TableButton' onclick='UpdateStatus(" + Task.Task_ID + ", " + 0 + ")'></button>";
                                Status1.Text = "<button class='TableButton' onclick='UpdateStatus(" + Task.Task_ID + ", " + 1 + ")'></button>";
                                Status2.Text = "<button class='TableButton' onclick='UpdateStatus(" + Task.Task_ID + ", " + 2 + ")'></button>";
                            }
                            break;

                        default:
                            //We should never hit this.
                            break;
                    }
                    TempRow.Cells.Add(Status0);
                    TempRow.Cells.Add(Status1);
                    TempRow.Cells.Add(Status2);
                    TempRow.Cells.Add(Status3);


                    if (LoadSubTasks != true)
                    {
                        Table_Tasks.Rows.Add(TempRow);
                        break;
                    }
                    else
                    {
                        TableCell Cell_ToggleButton = new TableCell();
                        if (Task.Tasks1.Count > 0)
                        {
                            Cell_ToggleButton.Text = "<input type='button' Class='TableButtonToggleSubTask' onclick='ToggleSubTasks(" + Task.Task_ID + ")'</input>";
                            TempRow.Cells.Add(Cell_ToggleButton);
                        }
                        Table_Tasks.Rows.Add(TempRow);
                    }
                    
                    //Finally add the row to the table.
                    
                    foreach (var Subtask in Task.Tasks1) //Need to find a way to sort the Subtasks prior to adding them to the table!
                    {
                        TableRow Row_Subtask = new TableRow();
                        Row_Subtask.CssClass = "ImportantSubRow " + "Maintask" + Task.Task_ID;

                        TableCell Cell_SubTaskName = new TableCell();
                        Cell_SubTaskName.Text = "<b>&#8226 </b>" + Subtask.Task_Name;
                        Row_Subtask.Cells.Add(Cell_SubTaskName);

                        TableCell Cell_SubTaskAction = new TableCell();
                        Cell_SubTaskAction.Text = Subtask.Task_Action;
                        Row_Subtask.Cells.Add(Cell_SubTaskAction);

                        TableCell Cell_SubTaskStart = new TableCell();
                        if (Subtask.Task_Start != null)
                        {
                            Cell_SubTaskStart.Text = Subtask.Task_Start.ToString();
                        }
                        Row_Subtask.Cells.Add(Cell_SubTaskStart);

                        TableCell Cell_SubTaskStaff = new TableCell();
                        Cell_SubTaskStaff.Text = Subtask.Task_Staff;
                        Row_Subtask.Cells.Add(Cell_SubTaskStaff);

                        TableCell SubStatus0 = new TableCell();
                        TableCell SubStatus1 = new TableCell();
                        TableCell SubStatus2 = new TableCell();
                        TableCell SubStatus3 = new TableCell();

                        switch (Subtask.Task_Status)
                        {
                            case 0:
                                SubStatus0.BackColor = System.Drawing.Color.Purple;
                                if (AllowEdits)
                                {
                                    SubStatus1.Text = "<button class='TableButton' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 1 + ")'></button>";
                                    SubStatus2.Text = "<button class='TableButton' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 2 + ")'></button>";
                                    SubStatus3.Text = "<button class='TableButton' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 3 + ")'></button>";
                                }
                                break;
                            case 1:
                                SubStatus1.BackColor = System.Drawing.Color.Yellow;
                                if (AllowEdits)
                                {
                                    SubStatus0.Text = "<button class='TableButton' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 0 + ")'></button>";
                                    SubStatus2.Text = "<button class='TableButton' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 2 + ")'></button>";
                                    SubStatus3.Text = "<button class='TableButton' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 3 + ")'></button>";
                                }
                                break;
                            case 2:
                                SubStatus2.BackColor = System.Drawing.Color.Green;
                                if (AllowEdits)
                                {
                                    SubStatus0.Text = "<button class='TableButton' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 0 + ")'></button>";
                                    SubStatus1.Text = "<button class='TableButton' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 1 + ")'></button>";
                                    SubStatus3.Text = "<button class='TableButton' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 3 + ")'></button>";
                                }
                                break;
                            case 3:
                                SubStatus3.BackColor = System.Drawing.Color.Red;
                                if (AllowEdits)
                                {
                                    SubStatus0.Text = "<button class='TableButton' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 0 + ")'></button>";
                                    SubStatus1.Text = "<button class='TableButton' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 1 + ")'></button>";
                                    SubStatus2.Text = "<button class='TableButton' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 2 + ")'></button>";
                                }
                                break;
                            default:
                                //We should never hit this.
                                break;
                        }

                        Row_Subtask.Cells.Add(SubStatus0);
                        Row_Subtask.Cells.Add(SubStatus1);
                        Row_Subtask.Cells.Add(SubStatus2);
                        Row_Subtask.Cells.Add(SubStatus3);

                        //Finally add the row to the table.
                        Table_Tasks.Rows.Add(Row_Subtask);
                    }
                }
            }
        }
    }

    private void LoadRows(bool LoadSubTasks = true, bool AllowEdits = false)
    {
        using (var db = new ProjectManagerEntities())
        {
            var query = from Inquiry in db.Tasks
                        where Inquiry.Task_IsPriority == false
                        && Inquiry.Task_MainTask == null
                        orderby Inquiry.Task_Status
                        select Inquiry;

            foreach (var Task in query)
            {
                TableRow TempRow = new TableRow();
                //TempRow.BackColor = System.Drawing.Color.OrangeRed;

                TableCell Cell_TaskName = new TableCell();
                Cell_TaskName.Text = Task.Task_Name;
                TempRow.Cells.Add(Cell_TaskName);

                TableCell Cell_TaskAction = new TableCell();
                Cell_TaskAction.Text = Task.Task_Action;
                TempRow.Cells.Add(Cell_TaskAction);

                TableCell Cell_TaskStart = new TableCell();
                if (Task.Task_Start != null)
                {
                    Cell_TaskStart.Text = Task.Task_Start.Value.ToShortDateString();
                }
                TempRow.Cells.Add(Cell_TaskStart);

                TableCell Cell_TaskStaff = new TableCell();
                Cell_TaskStaff.Text = Task.Task_Staff;
                TempRow.Cells.Add(Cell_TaskStaff);

                TableCell Status0 = new TableCell();
                TableCell Status1 = new TableCell();
                TableCell Status2 = new TableCell();
                TableCell Status3 = new TableCell();

                switch (Task.Task_Status)
                {
                    case 0: //If task is Started (Igang)
                        Status0.BackColor = System.Drawing.Color.Purple;
                        if (AllowEdits)
                        {
                            Status1.Text = "<button class='TableButton' onclick='UpdateStatus(" + Task.Task_ID + ", " + 1 + ")'></button>";
                            Status2.Text = "<button class='TableButton' onclick='UpdateStatus(" + Task.Task_ID + ", " + 2 + ")'></button>";
                            Status3.Text = "<button class='TableButton' onclick='UpdateStatus(" + Task.Task_ID + ", " + 3 + ")'></button>";
                        }
                        break;
                    case 1: //If task is Standby
                        Status1.BackColor = System.Drawing.Color.Yellow;
                        if (AllowEdits)
                        {
                            Status0.Text = "<button class='TableButton' onclick='UpdateStatus(" + Task.Task_ID + ", " + 0 + ")'></button>";
                            Status2.Text = "<button class='TableButton' onclick='UpdateStatus(" + Task.Task_ID + ", " + 2 + ")'></button>";
                            Status3.Text = "<button class='TableButton' onclick='UpdateStatus(" + Task.Task_ID + ", " + 3 + ")'></button>";
                        }
                        break;
                    case 2: //If task is finished
                        Status2.BackColor = System.Drawing.Color.Green;
                        if (AllowEdits)
                        {
                            Status0.Text = "<button class='TableButton' onclick='UpdateStatus(" + Task.Task_ID + ", " + 0 + ")'></button>";
                            Status1.Text = "<button class='TableButton' onclick='UpdateStatus(" + Task.Task_ID + ", " + 1 + ")'></button>";
                            Status3.Text = "<button class='TableButton' onclick='UpdateStatus(" + Task.Task_ID + ", " + 3 + ")'></button>";
                        }
                        break;
                    case 3://If task is Awaiting Accept.
                        Status3.BackColor = System.Drawing.Color.Red;
                        if (AllowEdits)
                        {
                            Status0.Text = "<button class='TableButton' onclick='UpdateStatus(" + Task.Task_ID + ", " + 0 + ")'></button>";
                            Status1.Text = "<button class='TableButton' onclick='UpdateStatus(" + Task.Task_ID + ", " + 1 + ")'></button>";
                            Status2.Text = "<button class='TableButton' onclick='UpdateStatus(" + Task.Task_ID + ", " + 2 + ")'></button>";
                        }
                        break;

                    default:
                        //We should never hit this.
                        break;
                }
                TempRow.Cells.Add(Status0);
                TempRow.Cells.Add(Status1);
                TempRow.Cells.Add(Status2);
                TempRow.Cells.Add(Status3);

                //Finally add the row to the table.

                if (LoadSubTasks != true)
                {
                    Table_Tasks.Rows.Add(TempRow);
                    break;
                }
                else
                {
                    TableCell Cell_ToggleButton = new TableCell();
                    if (Task.Tasks1.Count > 0)
                    {
                        Cell_ToggleButton.Text = "<input type='button' Class='TableButtonToggleSubTask' onclick='ToggleSubTasks(" + Task.Task_ID + ")'>";
                        TempRow.Cells.Add(Cell_ToggleButton);
                    }
                    Table_Tasks.Rows.Add(TempRow);
                }

                foreach (var Subtask in Task.Tasks1) //Need to find a way to sort the Subtasks prior to adding them to the table!
                {
                    TableRow Row_Subtask = new TableRow();
                    Row_Subtask.BackColor = System.Drawing.Color.Wheat; //Change this later so it's clearly visible it's subtasks!!!!
                    Row_Subtask.CssClass = "Maintask" + Task.Task_ID;

                    TableCell Cell_SubTaskName = new TableCell();
                    Cell_SubTaskName.Text = "<b>&#8226 </b>" + Subtask.Task_Name;
                    Row_Subtask.Cells.Add(Cell_SubTaskName);

                    TableCell Cell_SubTaskAction = new TableCell();
                    Cell_SubTaskAction.Text = Subtask.Task_Action;
                    Row_Subtask.Cells.Add(Cell_SubTaskAction);

                    TableCell Cell_SubTaskStart = new TableCell();
                    if (Subtask.Task_Start != null)
                    {
                        Cell_SubTaskStart.Text = Subtask.Task_Start.ToString();
                    }
                    Row_Subtask.Cells.Add(Cell_SubTaskStart);

                    TableCell Cell_SubTaskStaff = new TableCell();
                    Cell_SubTaskStaff.Text = Subtask.Task_Staff;
                    Row_Subtask.Cells.Add(Cell_SubTaskStaff);

                    TableCell SubStatus0 = new TableCell();
                    TableCell SubStatus1 = new TableCell();
                    TableCell SubStatus2 = new TableCell();
                    TableCell SubStatus3 = new TableCell();

                    switch (Subtask.Task_Status)
                    {
                        case 0:
                            SubStatus0.BackColor = System.Drawing.Color.Purple;
                            if (AllowEdits)
                            {
                                SubStatus1.Text = "<button class='TableButton' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 1 + ")'></button>";
                                SubStatus2.Text = "<button class='TableButton' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 2 + ")'></button>";
                                SubStatus3.Text = "<button class='TableButton' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 3 + ")'></button>";
                            }
                            break;
                        case 1:
                            SubStatus1.BackColor = System.Drawing.Color.Yellow;
                            if (AllowEdits)
                            {
                                SubStatus0.Text = "<button class='TableButton' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 0 + ")'></button>";
                                SubStatus2.Text = "<button class='TableButton' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 2 + ")'></button>";
                                SubStatus3.Text = "<button class='TableButton' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 3 + ")'></button>";
                            }
                            break;
                        case 2:
                            SubStatus2.BackColor = System.Drawing.Color.Green;
                            if (AllowEdits)
                            {
                                SubStatus0.Text = "<button class='TableButton' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 0 + ")'></button>";
                                SubStatus1.Text = "<button class='TableButton' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 1 + ")'></button>";
                                SubStatus3.Text = "<button class='TableButton' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 3 + ")'></button>";
                            }
                            break;
                        case 3:
                            SubStatus3.BackColor = System.Drawing.Color.Red;
                            if (AllowEdits)
                            {
                                SubStatus0.Text = "<button class='TableButton' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 0 + ")'></button>";
                                SubStatus1.Text = "<button class='TableButton' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 1 + ")'></button>";
                                SubStatus2.Text = "<button class='TableButton' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 2 + ")'></button>";
                            }
                            break;
                        default:
                            //We should never hit this.
                            break;
                    }

                    Row_Subtask.Cells.Add(SubStatus0);
                    Row_Subtask.Cells.Add(SubStatus1);
                    Row_Subtask.Cells.Add(SubStatus2);
                    Row_Subtask.Cells.Add(SubStatus3);

                    //Finally add the row to the table.
                    Table_Tasks.Rows.Add(Row_Subtask);
                }
            }
        }
        #region .TestRowCells.
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
        #endregion
    }

    protected void Input_Task_Insert_Click(object sender, EventArgs e)
    {
        Tasks NewTask = new Tasks();
        NewTask.Task_Name = Input_Task_Name.Value;
        NewTask.Task_Action = Input_Task_Action.Value;

        DateTime StartDate;
        DateTime.TryParse(Input_Task_StartDate.Value, out StartDate);
        if (Input_Task_StartDate.Value == "")
        {
            NewTask.Task_Start = null;
        }
        else
        {
            NewTask.Task_Start = StartDate;
        }

        DateTime Deadline;
        DateTime.TryParse(Input_Task_Deadline.Value, out Deadline);
        if (Input_Task_Deadline.Value == "")
        {
            NewTask.Task_Deadline = null;
        }
        else
        {
            NewTask.Task_Deadline = Deadline;
        }

        if (Input_Task_Staff.Value == "")
        {
            NewTask.Task_Staff = null;
        }
        else
        {
            NewTask.Task_Staff = Input_Task_Staff.Value;
        }

        decimal Price;
        decimal.TryParse(Input_Task_Price.Value, out Price);
        if (Price == 0)
        {
            NewTask.Task_Price = null;
        }
        else
        {
            NewTask.Task_Price = Price;
        }
        
        NewTask.Task_IsPriority = Input_Task_IsPriority.Checked;
        NewTask.Task_CreationDate = DateTime.Now;
        NewTask.Task_CompletionDate = null;
        NewTask.Task_ApprovedDate = null;

        if (Input_Task_Department.SelectedValue == "Ingen")
        {
            NewTask.Task_Department = null;
        }
        else
        {
            using (var db = new ProjectManagerEntities())
            {
                var query = from Inquiry in db.Departments
                            where Inquiry.Department_Name == Input_Task_Department.SelectedValue
                            select Inquiry;

                int SelectedDepartmentID = 0; //If it finds no number it will just put it on Dev (I guess that's the easy way of making bug reports)
                foreach (var Department in query)
                {
                    SelectedDepartmentID = Department.Department_ID;
                }
                NewTask.Task_Department = SelectedDepartmentID;
            }
        }
        NewTask.Task_Status = 3;
        
        using (var db = new ProjectManagerEntities())
        {
            db.Tasks.Add(NewTask);
            db.SaveChanges();
        }
        Response.Redirect(Request.RawUrl);
    }
}