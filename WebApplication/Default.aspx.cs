using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

public partial class _Default : System.Web.UI.Page
{
    bool LoggedIn = false;
    bool AllowEdits;
    private List<string> DepartmentIndex;
    //At some point we should make this default to false, and set it true upon a login for security reasons.
    protected void Page_Load(object sender, EventArgs e)
    {
        //Check if the state is even set.
        if (Session["Login"] != null)
        {
            LoggedIn = (bool)Session["Login"];
        }
        //Then we check the value, defaults to false.
        if (LoggedIn)
        {
            Input_Password.Visible = false;
            AllowEdits = true;
            LoginButton.Visible = false;
            LogoutButton.Visible = true;
        }
        else
        {
            Input_Password.Visible = true;
            AllowEdits = false;
            LoginButton.Visible = true;
            LogoutButton.Visible = false;
        }
        //Controls that a visible depending if Edits Allowed or not.
        RowNewTaskButton.Visible = AllowEdits;
        InputRow.Visible = AllowEdits;
        EditTaskRow.Visible = AllowEdits;
        EditTaskInputs.Visible = AllowEdits;

        //When we load items from the database on load, but never get rid of them, we allocate memory but never clear it. Possible memory leak on bigger system?
        Table_Tasks.CssClass = "table table-condensed table-bordered TableChanges";
        //Load the headers
        LoadTableHeaders();
        if (!Page.IsPostBack) //Loading departments into the Dropdown Menu (when it's not a postback, to prevent the selection resetting on button clicks)
        {
            LoadDepartments();
        }
        DepartmentIndex = LoadDepartmentList();
        LoadPriortyRows(true, true, AllowEdits);
        LoadRows(true, AllowEdits);
        CheckSessionForEdit();
    }

    private List<string> LoadDepartmentList()
    {
        List<string> ReturnMe = new List<string>();
        using (var db = new ProjectManagerEntities())
        {
            var query = from Inquiry in db.Departments
                        select Inquiry;

            foreach (var Department in query)
            {
                ReturnMe.Add(Department.Department_Name);
            }
        }
        return ReturnMe;
    }
    
    private void LoadDepartments()
    {
        Input_Task_Department.Items.Clear();
        Input_Task_Department.Items.Add("Ingen");
        Edit_DepartmentDropdown.Items.Clear();
        Edit_DepartmentDropdown.Items.Add("Ingen");
        using (var db = new ProjectManagerEntities())
        {
            var query = from Inquiry in db.Departments
                        select Inquiry;

            foreach (var Department in query)
            {
                Input_Task_Department.Items.Add(Department.Department_Name);
                Edit_DepartmentDropdown.Items.Add(Department.Department_Name);
            }
        }
    }
    
    [WebMethod]
    public static void SetApprovedComplete(int ID)
    {
        //Perhabs put in a date for this change as well, this requires another database update however.
        using (var db = new ProjectManagerEntities())
        {
            var query = from Inquiry in db.Tasks
                        where Inquiry.Task_ID == ID
                        select Inquiry;
            foreach (var Task in query)
            {
                Task.Task_ApprovedComplete = true;
            }
            db.SaveChanges();
        }
    }

    [WebMethod]
    public static void UpdateStatus(int ID, int NewStatus)
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
                if (NewStatus != 3 && Task.Task_Status == 3) //Only if current state is 3. We only approve it when it's moved from unapproved -> Approved
                {
                    Task.Task_ApprovedDate = DateTime.Now;
                }
                if (NewStatus == 3) //If we set task into Unapproved, the Approval date should be null.
                {
                    Task.Task_ApprovedDate = null;
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

        TableHeaderCell headerTableCell0 = new TableHeaderCell();
        headerTableCell0.Text = "<img src='Images/MoreInfo.png' />";
        headerTableCell0.CssClass = "vertical-text-table";
        HeaderRow.Cells.Add(headerTableCell0);

        TableHeaderCell headerTableCell1 = new TableHeaderCell();
        headerTableCell1.Text = "<h1>Opgave</h1>";
        //headerTableCell1.Scope = TableHeaderScope.Column;
        headerTableCell1.AbbreviatedText = "Opgave";
        headerTableCell1.CssClass = "TableHeaderCellRow1";
        HeaderRow.Cells.Add(headerTableCell1);

        TableHeaderCell headerTableCell2 = new TableHeaderCell();
        headerTableCell2.Text = "<h1>Action</h1>";
        //headerTableCell2.Scope = TableHeaderScope.Column;
        headerTableCell2.AbbreviatedText = "Action";
        headerTableCell2.CssClass = "TableHeaderCellRow2";
        HeaderRow.Cells.Add(headerTableCell2);

        TableHeaderCell headerTableCell3 = new TableHeaderCell();
        headerTableCell3.Text = "<h1>Forventet <br /> Start</h1>";
        //headerTableCell3.Scope = TableHeaderScope.Column;
        headerTableCell3.AbbreviatedText = "Start";
        headerTableCell3.CssClass = "TableHeaderCellRow3";
        HeaderRow.Cells.Add(headerTableCell3);

        TableHeaderCell headerTableCell4 = new TableHeaderCell();
        headerTableCell4.Text = "<h1>Personale</h1>";
        //headerTableCell4.Scope = TableHeaderScope.Column;
        headerTableCell4.AbbreviatedText = "Personale";
        headerTableCell4.CssClass = "TableHeaderCellRow4";
        HeaderRow.Cells.Add(headerTableCell4);


        //To make them all the same, make some images to put in, instead of the text to make it the same size!
        //Vertial Coloumns
        TableHeaderCell headerTableCell5 = new TableHeaderCell();
        headerTableCell5.Text = "<img src='Images/Igang.png' />";
        headerTableCell5.CssClass = "vertical-text-table";
        //headerTableCell5.CssClass = "vertical-text-table-purple vertical-text-table";
        HeaderRow.Cells.Add(headerTableCell5);

        TableHeaderCell headerTableCell6 = new TableHeaderCell();
        headerTableCell6.Text = "<img src='Images/Stanby.png' />";
        headerTableCell6.CssClass = "vertical-text-table";
        //headerTableCell6.CssClass = "vertical-text-table-yellow vertical-text-table";
        HeaderRow.Cells.Add(headerTableCell6);

        TableHeaderCell headerTableCell7 = new TableHeaderCell();
        headerTableCell7.Text = "<img src='Images/Færdig.png' />";
        headerTableCell7.CssClass = "vertical-text-table";
        //headerTableCell7.CssClass = "vertical-text-table-green vertical-text-table";
        HeaderRow.Cells.Add(headerTableCell7);

        TableHeaderCell headerTableCell8 = new TableHeaderCell();
        headerTableCell8.Text = "<img src='Images/AfventerGodk.png' />";
        headerTableCell8.CssClass = "vertical-text-table";
        //headerTableCell8.CssClass = "vertical-text-table-red vertical-text-table";
        HeaderRow.Cells.Add(headerTableCell8);
        
        TableHeaderCell headerTableCell9 = new TableHeaderCell();
        headerTableCell9.Text = "<img src='Images/Udfold.png' />";
        headerTableCell9.CssClass = "vertical-text-table";
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
                        && Inquiry.Task_MainTask == null
                        && Inquiry.Task_ApprovedComplete != true
                            orderby Inquiry.Task_Status
                            select Inquiry;

                foreach (var Task in query)
                {
                    TableRow TempRow = new TableRow();
                    TempRow.CssClass = "ImportantRow";

                    TableCell Cell_MoreInfoButton = new TableCell();
                    Cell_MoreInfoButton.Text = "<input type='button' Class='TableButtonToggleSubTask' onclick='ToggleMoreInfo(" + Task.Task_ID + ")'</input>"; //FunctionCall(ToggleMoreInfo(#));
                    TempRow.Cells.Add(Cell_MoreInfoButton);
                    
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
                                Status1.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Task.Task_ID + ", " + 1 + ")'></button>";
                                Status2.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Task.Task_ID + ", " + 2 + ")'></button>";
                                Status3.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Task.Task_ID + ", " + 3 + ")'></button>";
                            }
                            break;
                        case 1: //If task is Standby
                            Status1.BackColor = System.Drawing.Color.Yellow;
                            if (AllowEdits)
                            {
                                Status0.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Task.Task_ID + ", " + 0 + ")'></button>";
                                Status2.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Task.Task_ID + ", " + 2 + ")'></button>";
                                Status3.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Task.Task_ID + ", " + 3 + ")'></button>";
                            }
                            break;
                        case 2: //If task is finished
                            Status2.BackColor = System.Drawing.Color.Green;
                            if (AllowEdits)
                            {
                                Status0.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Task.Task_ID + ", " + 0 + ")'></button>";
                                Status1.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Task.Task_ID + ", " + 1 + ")'></button>";
                                Status3.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Task.Task_ID + ", " + 3 + ")'></button>";
                            }
                            break;
                        case 3://If task is Awaiting Accept.
                            Status3.BackColor = System.Drawing.Color.Red;
                            if (AllowEdits)
                            {
                                Status0.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Task.Task_ID + ", " + 0 + ")'></button>";
                                Status1.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Task.Task_ID + ", " + 1 + ")'></button>";
                                Status2.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Task.Task_ID + ", " + 2 + ")'></button>";
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

                    //More info Row:
                    TableRow MoreInfoRow = new TableRow();
                    MoreInfoRow.CssClass = "HiddenInfoRow"; //We use the class to hide all on load once document is loaded. 
                    MoreInfoRow.ID = "TaskInfo" + Task.Task_ID;

                    TableCell FillerCell = new TableCell(); //Button for EDIT TASK??
                    MoreInfoRow.Cells.Add(FillerCell);

                    TableCell MoreInfoCell = new TableCell();
                    MoreInfoCell.Text = "TaskID: " + Task.Task_ID + "<br />";//Don't need to check this, as there cannot be NULL values of TASK_ID
                    if (LoggedIn)
                    {
                        MoreInfoCell.Text += "<a href='#' onclick='CreateSubTask(" + Task.Task_ID + "); return false;'>Opret Underopgave</a><br />";
                        MoreInfoCell.Text += "<a href='#' onclick='EditTask(" + Task.Task_ID + "); return false;'>Redigér</a><br />";
                        if (Task.Task_Status == 2)
                        {
                            MoreInfoCell.Text += "<a href='.' onclick='SetApprovedComplete(" + Task.Task_ID + ")';'>Godkend Færdig</a>";
                        }
                    }
                    MoreInfoRow.Cells.Add(MoreInfoCell);

                    TableCell MoreInfoCell2 = new TableCell();
                    if (Task.Task_Price != null)
                    {
                        MoreInfoCell2.Text += "Pris: " + Task.Task_Price + " kr.<br />";
                    }
                    if (Task.Task_Department != null)
                    {
                        MoreInfoCell2.Text += "Afdeling:  " + DepartmentIndex[Task.Task_Department.Value];
                    }
                    MoreInfoRow.Cells.Add(MoreInfoCell2);

                    //DateTimesCell
                    TableCell MoreInfoCell3 = new TableCell();
                    if (Task.Task_Deadline != null)
                    {
                        MoreInfoCell3.Text += "Frist: " + Task.Task_Deadline.Value.ToShortDateString() + "<br />";
                    }
                    if (Task.Task_CreationDate != null)
                    {
                        MoreInfoCell3.Text += "Oprettet: " + Task.Task_CreationDate.Value.ToShortDateString() + "<br />";
                    }
                    if (Task.Task_ApprovedDate != null)
                    {
                        MoreInfoCell3.Text += "Godkendt: " + Task.Task_ApprovedDate.Value.ToShortDateString() + "<br />";
                    }
                    if (Task.Task_CompletionDate != null)
                    {
                        MoreInfoCell3.Text += "Færdigjort: " + Task.Task_CompletionDate.Value.ToShortDateString() + "<br />";
                    }
                    MoreInfoRow.Cells.Add(MoreInfoCell3);
                    //Remember to add the row to the table!!!!

                    if (LoadSubTasks != true) //If we're not loading subtasks, we just add the row and break off.
                    {
                        Table_Tasks.Rows.Add(TempRow);
                        Table_Tasks.Rows.Add(MoreInfoRow);
                        break;
                    }
                    else //IF we are however adding subrows, We add a button cell and then we add the row!
                    {
                        TableCell Cell_ToggleButton = new TableCell();
                        if (Task.Tasks1.Count > 0)
                        {
                            Cell_ToggleButton.Text = "<input type='button' Class='TableButtonToggleSubTask' onclick='ToggleSubTasks(" + Task.Task_ID + ")'</input>";
                            TempRow.Cells.Add(Cell_ToggleButton);
                        }
                        Table_Tasks.Rows.Add(TempRow);
                        Table_Tasks.Rows.Add(MoreInfoRow);
                    }
                    

                    //Loading of subtasks!
                    foreach (var Subtask in Task.Tasks1) //Need to find a way to sort the Subtasks prior to adding them to the table!
                    {
                        TableRow Row_Subtask = new TableRow();
                        Row_Subtask.CssClass = "ImportantSubRow " + "Maintask" + Task.Task_ID;

                        TableCell MoreInfoButtonCell = new TableCell();
                        MoreInfoButtonCell.Text = "<input type='button' Class='TableButtonToggleSubTask' onclick='ToggleMoreInfo(" + Subtask.Task_ID + ")'</input>";
                        Row_Subtask.Cells.Add(MoreInfoButtonCell);

                        TableCell Cell_SubTaskName = new TableCell();
                        Cell_SubTaskName.Text = "<b>&#8226 </b>" + Subtask.Task_Name;
                        Row_Subtask.Cells.Add(Cell_SubTaskName);

                        TableCell Cell_SubTaskAction = new TableCell();
                        Cell_SubTaskAction.Text = Subtask.Task_Action;
                        Row_Subtask.Cells.Add(Cell_SubTaskAction);

                        TableCell Cell_SubTaskStart = new TableCell();
                        if (Subtask.Task_Start != null)
                        {
                            Cell_SubTaskStart.Text = Subtask.Task_Start.Value.ToShortDateString();
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
                                    SubStatus1.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 1 + ")'></button>";
                                    SubStatus2.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 2 + ")'></button>";
                                    SubStatus3.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 3 + ")'></button>";
                                }
                                break;
                            case 1:
                                SubStatus1.BackColor = System.Drawing.Color.Yellow;
                                if (AllowEdits)
                                {
                                    SubStatus0.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 0 + ")'></button>";
                                    SubStatus2.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 2 + ")'></button>";
                                    SubStatus3.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 3 + ")'></button>";
                                }
                                break;
                            case 2:
                                SubStatus2.BackColor = System.Drawing.Color.Green;
                                if (AllowEdits)
                                {
                                    SubStatus0.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 0 + ")'></button>";
                                    SubStatus1.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 1 + ")'></button>";
                                    SubStatus3.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 3 + ")'></button>";
                                }
                                break;
                            case 3:
                                SubStatus3.BackColor = System.Drawing.Color.Red;
                                if (AllowEdits)
                                {
                                    SubStatus0.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 0 + ")'></button>";
                                    SubStatus1.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 1 + ")'></button>";
                                    SubStatus2.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 2 + ")'></button>";
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

                        //TODO Add Logic for MoreTaskInfo
                        TableRow SubMoreInfoRow = new TableRow();
                        SubMoreInfoRow.CssClass = "HiddenInfoRow SubInfo" + Task.Task_ID; //We use the class to hide all on load once document is loaded. SubInfo + MainTaskID to hide if clicking the SubTask button.
                        SubMoreInfoRow.ID = "TaskInfo" + Subtask.Task_ID;
                        
                        TableCell SubFillerCell = new TableCell(); //Button for EDIT TASK??
                        SubMoreInfoRow.Cells.Add(SubFillerCell);

                        TableCell SubMoreInfoCell = new TableCell();
                        SubMoreInfoCell.Text = "TaskID: " + Subtask.Task_ID + "<br />"; //Don't need to check this, as there cannot be NULL values of TASK_ID
                        if (LoggedIn)
                        {
                            SubMoreInfoCell.Text += "<a href='#' onclick='EditTask(" + Subtask.Task_ID + "); return false;'>Redigér</a><br />";
                        }
                        SubMoreInfoRow.Cells.Add(SubMoreInfoCell);

                        TableCell SubMoreInfoCell2 = new TableCell();
                        if (Subtask.Task_Price != null)
                        {
                            SubMoreInfoCell2.Text += "Pris: " + Subtask.Task_Price + " kr.<br />";
                        }
                        if (Subtask.Task_Department != null)
                        {
                            SubMoreInfoCell2.Text += "Afdeling:  " + DepartmentIndex[Subtask.Task_Department.Value];
                        }
                        SubMoreInfoRow.Cells.Add(SubMoreInfoCell2);

                        //DateTimesCell
                        TableCell SubMoreInfoCell3 = new TableCell();
                        if (Subtask.Task_Deadline != null)
                        {
                            SubMoreInfoCell3.Text += "Frist: " + Subtask.Task_Deadline.Value.ToShortDateString() + "<br />";
                        }
                        if (Subtask.Task_CreationDate != null)
                        {
                            SubMoreInfoCell3.Text += "Oprettet: " + Subtask.Task_CreationDate.Value.ToShortDateString() + "<br />";
                        }
                        if (Subtask.Task_ApprovedDate != null)
                        {
                            SubMoreInfoCell3.Text += "Godkendt: " + Subtask.Task_ApprovedDate.Value.ToShortDateString() + "<br />";
                        }
                        if (Subtask.Task_CompletionDate != null)
                        {
                            SubMoreInfoCell3.Text += "Færdigjort: " + Subtask.Task_CompletionDate.Value.ToShortDateString() + "<br />";
                        }
                        SubMoreInfoRow.Cells.Add(SubMoreInfoCell3);

                        //Finally add the row to the table.
                        Table_Tasks.Rows.Add(Row_Subtask);
                        Table_Tasks.Rows.Add(SubMoreInfoRow);
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
                    && Inquiry.Task_ApprovedComplete != true
                        orderby Inquiry.Task_Status ascending
                        select Inquiry;

            foreach (var Task in query)
            {
                TableRow TempRow = new TableRow();
                //TempRow.BackColor = System.Drawing.Color.OrangeRed;

                TableCell MoreInfoButton = new TableCell();
                MoreInfoButton.Text = "<input type='button' Class='TableButtonToggleSubTask' onclick='ToggleMoreInfo(" + Task.Task_ID + ")'</input>";
                TempRow.Cells.Add(MoreInfoButton);


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
                            Status1.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Task.Task_ID + ", " + 1 + ")'></button>";
                            Status2.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Task.Task_ID + ", " + 2 + ")'></button>";
                            Status3.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Task.Task_ID + ", " + 3 + ")'></button>";
                        }
                        break;
                    case 1: //If task is Standby
                        Status1.BackColor = System.Drawing.Color.Yellow;
                        if (AllowEdits)
                        {
                            Status0.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Task.Task_ID + ", " + 0 + ")'></button>";
                            Status2.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Task.Task_ID + ", " + 2 + ")'></button>";
                            Status3.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Task.Task_ID + ", " + 3 + ")'></button>";
                        }
                        break;
                    case 2: //If task is finished
                        Status2.BackColor = System.Drawing.Color.Green;
                        if (AllowEdits)
                        {
                            Status0.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Task.Task_ID + ", " + 0 + ")'></button>";
                            Status1.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Task.Task_ID + ", " + 1 + ")'></button>";
                            Status3.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Task.Task_ID + ", " + 3 + ")'></button>";
                        }
                        break;
                    case 3://If task is Awaiting Accept.
                        Status3.BackColor = System.Drawing.Color.Red;
                        if (AllowEdits)
                        {
                            Status0.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Task.Task_ID + ", " + 0 + ")'></button>";
                            Status1.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Task.Task_ID + ", " + 1 + ")'></button>";
                            Status2.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Task.Task_ID + ", " + 2 + ")'></button>";
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

                TableRow MoreInfoRow = new TableRow();
                MoreInfoRow.CssClass = "HiddenInfoRow"; //We use the class to hide all on load once document is loaded. 
                MoreInfoRow.ID = "TaskInfo" + Task.Task_ID;

                TableCell FillerCell = new TableCell();
                //FillerCell.Text = "<input type='button' Class='TableButtonToggleSubTask' onclick='CreateSubTask(" + Task.Task_ID + ")'</input>"; ;
                MoreInfoRow.Cells.Add(FillerCell);

                TableCell MoreInfoCell = new TableCell();
                MoreInfoCell.Text = "TaskID: " + Task.Task_ID + "<br />";//Don't need to check this, as there cannot be NULL values of TASK_ID
                if (LoggedIn)
                {
                    MoreInfoCell.Text += "<a href='#' onclick='CreateSubTask(" + Task.Task_ID + "); return false;'>Opret Underopgave</a><br />";
                    MoreInfoCell.Text += "<a href='#' onclick='EditTask(" + Task.Task_ID + "); return false;'>Redigér</a><br />";
                    if (Task.Task_Status == 2)
                    {
                        MoreInfoCell.Text += "<a href='.' onclick='SetApprovedComplete(" + Task.Task_ID + ");''>Godkend Færdig</a>";
                    }
                }

                MoreInfoRow.Cells.Add(MoreInfoCell);

                TableCell MoreInfoCell2 = new TableCell();
                if (Task.Task_Price != null)
                {
                    MoreInfoCell2.Text += "Pris: " + Task.Task_Price + " kr.<br />";
                }
                if (Task.Task_Department != null)
                {
                    MoreInfoCell2.Text += "Afdeling:  " + DepartmentIndex[Task.Task_Department.Value];
                }
                MoreInfoRow.Cells.Add(MoreInfoCell2);

                //DateTimesCell
                TableCell MoreInfoCell3 = new TableCell();
                if (Task.Task_Deadline != null)
                {
                    MoreInfoCell3.Text += "Frist: " + Task.Task_Deadline.Value.ToShortDateString() + "<br />";
                }
                if (Task.Task_CreationDate != null)
                {
                    MoreInfoCell3.Text += "Oprettet: " + Task.Task_CreationDate.Value.ToShortDateString() + "<br />";
                }
                if (Task.Task_ApprovedDate != null)
                {
                    MoreInfoCell3.Text += "Godkendt: " + Task.Task_ApprovedDate.Value.ToShortDateString() + "<br />";
                }
                if (Task.Task_CompletionDate != null)
                {
                    MoreInfoCell3.Text += "Færdigjort: " + Task.Task_CompletionDate.Value.ToShortDateString() + "<br />";
                }
                MoreInfoRow.Cells.Add(MoreInfoCell3);
                //Remember to add the row to the table!!!!

                if (LoadSubTasks != true)
                {
                    Table_Tasks.Rows.Add(TempRow);
                    Table_Tasks.Rows.Add(MoreInfoRow);
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
                    Table_Tasks.Rows.Add(MoreInfoRow);
                }

                foreach (var Subtask in Task.Tasks1) //Need to find a way to sort the Subtasks prior to adding them to the table!
                {
                    TableRow Row_Subtask = new TableRow();
                    Row_Subtask.BackColor = System.Drawing.Color.Wheat; //Change this later so it's clearly visible it's subtasks!!!!
                    Row_Subtask.CssClass = "Maintask" + Task.Task_ID;

                    TableCell MoreInfoButtonCell = new TableCell();
                    MoreInfoButtonCell.Text = "<input type='button' Class='TableButtonToggleSubTask' onclick='ToggleMoreInfo(" + Subtask.Task_ID + ")'</input>";
                    Row_Subtask.Cells.Add(MoreInfoButtonCell);

                    TableCell Cell_SubTaskName = new TableCell();
                    Cell_SubTaskName.Text = "<b>&#8226 </b>" + Subtask.Task_Name; //Bullet Points
                    Row_Subtask.Cells.Add(Cell_SubTaskName);

                    TableCell Cell_SubTaskAction = new TableCell();
                    Cell_SubTaskAction.Text = Subtask.Task_Action;
                    Row_Subtask.Cells.Add(Cell_SubTaskAction);

                    TableCell Cell_SubTaskStart = new TableCell();
                    if (Subtask.Task_Start != null)
                    {
                        Cell_SubTaskStart.Text = Subtask.Task_Start.Value.ToShortDateString();
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
                                SubStatus1.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 1 + ")'></button>";
                                SubStatus2.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 2 + ")'></button>";
                                SubStatus3.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 3 + ")'></button>";
                            }
                            break;
                        case 1:
                            SubStatus1.BackColor = System.Drawing.Color.Yellow;
                            if (AllowEdits)
                            {
                                SubStatus0.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 0 + ")'></button>";
                                SubStatus2.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 2 + ")'></button>";
                                SubStatus3.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 3 + ")'></button>";
                            }
                            break;
                        case 2:
                            SubStatus2.BackColor = System.Drawing.Color.Green;
                            if (AllowEdits)
                            {
                                SubStatus0.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 0 + ")'></button>";
                                SubStatus1.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 1 + ")'></button>";
                                SubStatus3.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 3 + ")'></button>";
                            }
                            break;
                        case 3:
                            SubStatus3.BackColor = System.Drawing.Color.Red;
                            if (AllowEdits)
                            {
                                SubStatus0.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 0 + ")'></button>";
                                SubStatus1.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 1 + ")'></button>";
                                SubStatus2.Text = "<button class='TableButton btn btn-primary' onclick='UpdateStatus(" + Subtask.Task_ID + ", " + 2 + ")'></button>";
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

                    TableRow SubMoreInfoRow = new TableRow();
                    SubMoreInfoRow.CssClass = "HiddenInfoRow SubInfo" + Task.Task_ID; //We use the class to hide all on load once document is loaded. SubInfo + MainTaskID to hide if clicking the SubTask button.
                    SubMoreInfoRow.ID = "TaskInfo" + Subtask.Task_ID;

                    TableCell SubFillerCell = new TableCell(); //Button for EDIT TASK??
                    SubMoreInfoRow.Cells.Add(SubFillerCell);

                    TableCell SubMoreInfoCell = new TableCell();
                    SubMoreInfoCell.Text = "TaskID: " + Subtask.Task_ID + "<br />"; //Don't need to check this, as there cannot be NULL values of TASK_ID
                    if (LoggedIn)
                    {
                        SubMoreInfoCell.Text += "<a href='#' onclick='EditTask(" + Subtask.Task_ID + "); return false;'>Redigér</a><br />";
                    }
                    SubMoreInfoRow.Cells.Add(SubMoreInfoCell);

                    TableCell SubMoreInfoCell2 = new TableCell();
                    if (Subtask.Task_Price != null)
                    {
                        SubMoreInfoCell2.Text += "Pris: " + Subtask.Task_Price + " kr.<br />";
                    }
                    if (Subtask.Task_Department != null)
                    {
                        SubMoreInfoCell2.Text += "Afdeling:  " + DepartmentIndex[Subtask.Task_Department.Value];
                    }
                    SubMoreInfoRow.Cells.Add(SubMoreInfoCell2);

                    //DateTimesCell
                    TableCell SubMoreInfoCell3 = new TableCell();
                    if (Subtask.Task_Deadline != null)
                    {
                        SubMoreInfoCell3.Text += "Frist: " + Subtask.Task_Deadline.Value.ToShortDateString() + "<br />";
                    }
                    if (Subtask.Task_CreationDate != null)
                    {
                        SubMoreInfoCell3.Text += "Oprettet: " + Subtask.Task_CreationDate.Value.ToShortDateString() + "<br />";
                    }
                    if (Subtask.Task_ApprovedDate != null)
                    {
                        SubMoreInfoCell3.Text += "Godkendt: " + Subtask.Task_ApprovedDate.Value.ToShortDateString() + "<br />";
                    }
                    if (Subtask.Task_CompletionDate != null)
                    {
                        SubMoreInfoCell3.Text += "Færdigjort: " + Subtask.Task_CompletionDate.Value.ToShortDateString() + "<br />";
                    }
                    SubMoreInfoRow.Cells.Add(SubMoreInfoCell3);

                    //Finally add the row to the table.
                    Table_Tasks.Rows.Add(Row_Subtask);
                    Table_Tasks.Rows.Add(SubMoreInfoRow);
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

    protected void Input_SubTask_Insert_Click(object sender, EventArgs e)
    {
        Tasks NewTask = new Tasks();

        int MainTaskID;
        int.TryParse(Input_Task_MainTaskID.Value, out MainTaskID);
        NewTask.Task_MainTask = MainTaskID;
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

    protected void Login_Click(object sender, EventArgs e)
    {
        //byte[] data = System.Text.Encoding.ASCII.GetBytes(Input_Password.Value);
        //data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data); //Encryption for once I figure out.
        //string hash = System.Text.Encoding.ASCII.GetString(data);

        if (Session["Login"] == null)
        {
            using (var db = new ProjectManagerEntities())
            {
                var query = from Inquiry in db.Logins
                            select Inquiry;
                foreach (var Login in query)
                {
                    if (Login.Login_Password == Input_Password.Value)
                    {
                        Session.Add("Login", true);
                        break;
                    }
                }
            }
        }
        Response.Redirect(Request.RawUrl);
    }

    protected void LogoutButton_Click(object sender, EventArgs e)
    {
        if (Session["Login"] != null)
        {
            Session.Remove("Login");
        }
        if (Session["EditTaskID"] != null)
        {
            Session.Remove("EditTaskID");
        }
        Response.Redirect(Request.RawUrl);
    }

    //Edit methods
    protected void Edit_TaskButton_Click(object sender, EventArgs e)
    {
        int Task_ID;
        int.TryParse(Edit_TaskID.Value, out Task_ID);

        Session.Add("EditTaskID", Task_ID);
        Response.Redirect(Request.RawUrl);
    }

    private void CheckSessionForEdit()
    {
        //Session.Add("EditTaskID", 1009); //I added this line just for testing purposes. 
        if (Session["EditTaskID"] != null)
        {
            int TaskID = (int)Session["EditTaskID"];
            LoadTaskInfoForEdit(TaskID);
            //We hide the table and other rows that we don't need while we edit.
            EditTaskRow.Visible = false;
            InputRow.Visible = false;
            RowNewTaskButton.Visible = false;
            Table_Tasks.Visible = false;
        }
        else//If there is no Edit in progress:
        {
            EditTaskInputs.Visible = false;
        }
    }

    private void LoadTaskInfoForEdit(int TaskID)
    {
        //This will be called upon page load, if the Session["EditTaskID"] != null!
        //This function will load all the information from the selected task into controls that can then be edited!
        if (Page.IsPostBack)
        {
            return;
        }

        using (var db = new ProjectManagerEntities())
        {
            var query = from Inquiry in db.Tasks
                        where Inquiry.Task_ID == TaskID
                        select Inquiry;

            foreach (var Task in query)
            {
                if (Task.Task_Name != null)
                {
                    Edit_TaskName.Value = Task.Task_Name;
                }
                if (Task.Task_Action != null)
                {
                    Edit_TaskAction.Value = Task.Task_Action;
                }
                if (Task.Task_Start != null)
                {
                    DateTime StartDate = Task.Task_Start.Value;
                    Edit_StartDate.Value = StartDate.ToString("yyyy-MM-dd");
                }
                if (Task.Task_Deadline != null)
                {
                    DateTime Deadline = Task.Task_Deadline.Value;
                    Edit_Deadline.Value = Task.Task_Deadline.Value.ToString("yyyy-MM-dd");
                }
                if (Task.Task_Staff != null)
                {
                    Edit_Staff.Value = Task.Task_Staff;
                }
                if (Task.Departments != null)
                {
                    Edit_DepartmentDropdown.SelectedValue = DepartmentIndex[Task.Task_Department.Value];
                }
                if (Task.Task_Price != null)
                {
                    Edit_Price.Value = Task.Task_Price.ToString();
                }
                if (Task.Task_IsPriority != null)
                {
                    Edit_PriorityTask.Checked = Task.Task_IsPriority.Value;
                }
            }
        }
    }

    protected void Button_EditTask_Abort_Click(object sender, EventArgs e)
    {
        if (Session["EditTaskID"] != null) //When we abort, we just remove the EditTaskID, so we return to the mainwindow
        {
            Session.Remove("EditTaskID");
        }
        Response.Redirect(Request.RawUrl);
    }

    protected void Button_EditTask_Confirm_Click(object sender, EventArgs e)
    {
        int TaskToUpdate;
        if (Session["EditTaskID"] != null)
        {
            TaskToUpdate = (int)Session["EditTaskID"];
        }
        else
        {
            return;
        }
        //This is where the logic goes for updating a task
        using (var db = new ProjectManagerEntities())
        {
            var query = from Inquiry in db.Tasks
                        where Inquiry.Task_ID == TaskToUpdate
                        select Inquiry;

            foreach (var Task in query)
            {
                if (Edit_TaskName.Value != null)
                {
                    Task.Task_Name = Edit_TaskName.Value;
                }
                if (Edit_TaskAction.Value != null)
                {
                    Task.Task_Action = Edit_TaskAction.Value;
                }
                if (Edit_StartDate.Value != "")
                {
                    DateTime StartDate;
                    DateTime.TryParse(Edit_StartDate.Value, out StartDate);
                    Task.Task_Start = StartDate;
                }
                if (Edit_Deadline.Value != "")
                {
                    DateTime Deadline;
                    DateTime.TryParse(Edit_Deadline.Value, out Deadline);
                    Task.Task_Deadline = Deadline;
                }
                if (Edit_Staff.Value != null)
                {
                    Task.Task_Staff = Edit_Staff.Value;
                }
                if (Edit_DepartmentDropdown.SelectedValue != null)
                {
                    if (Edit_DepartmentDropdown.SelectedValue == "Ingen")
                    {
                        Task.Task_Department = null;
                    }
                    else
                    {
                        Task.Task_Department = Edit_DepartmentDropdown.SelectedIndex - 1; //We know that index 0 will always be "Ingen", otherwise they should follow the indexing of the Database.
                    }
                }

                if (Edit_Price.Value != null)
                {
                    if (Edit_Price.Value == "")
                    {
                        Task.Task_Price = null;
                    }
                    else
                    {
                        decimal Price;
                        decimal.TryParse(Edit_Price.Value, out Price);
                        Task.Task_Price = Price;
                    }
                }
                Task.Task_IsPriority = Edit_PriorityTask.Checked;
            }
            db.SaveChanges();
            if (Session["EditTaskID"] != null) //We're done, so now we can remove the Session Variable.
            {
                Session.Remove("EditTaskID");
            }
            Response.Redirect(Request.RawUrl);
        }
    }
}