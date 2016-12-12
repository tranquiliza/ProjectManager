<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Project-Manager Main</title>
    <link rel="stylesheet" type="text/css" href="Styling/css/TableStyling.css" media="screen" />
    <link rel="stylesheet" type="text/css" href="Styling/css/bootstrap.css" media="screen" />
    <link rel="stylesheet" type="text/css" href="Styling/css/PrinterStyling.css" media="print" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script src="Scripts/PageMethods.js"></script>
    <%--<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />--%>
</head>

<body>
    
    <form id="Form1" runat="server">
        
        <%--<!--Row for controls! -->--%>
        <div runat="server" class="row NoPrint" id="Controls">
            <div class="col-md-2 col-md-offset-1">
                <div class="form-inline">
                    <div id="LoginForm" runat="server" class="form-group">
                        <input style="margin-bottom:2px;" id="Input_Password" class="form-control" type="password" placeholder="Kodeord" runat="server" />
                    </div>
                    <asp:Button ID="LoginButton" runat="server" Text="Log Ind" CssClass="btn btn-info" OnClick="Login_Click" />
                    <asp:Button ID="LogoutButton" runat="server" Text="Log Ud" CssClass="btn btn-danger" OnClick="LogoutButton_Click" />
                    <%--<input type="button" onclick="PrintTable();" value="Print" />--%>
                </div>

                <%--<!--<input class="btn btn-info" id="ToggleEditButton" onclick="ToggleTableButtons()" value="Redigér" type="button" />-->--%>
            </div>
        </div>
        <%--<!--We need this is order to make our javascript update buttons work!-->--%>
        <asp:ScriptManager EnablePageMethods="true" ID="ScriptManager2" runat="server"></asp:ScriptManager>
        <!--Row for the table!-->
        <div class="row" id="table">
            <div class="col-md-10 col-md-offset-1">
                <asp:Table ID="Table_Tasks" runat="server">

                </asp:Table>
            </div>
        </div>
        <%--<!--Row for some table controls i suppose.-->--%>
        <div runat="server" class="row NoPrint" id="RowNewTaskButton">
            <div style="align-content:center" class="col-md-10 col-md-offset-1">
                <input class="btn btn-info" id="ToggleNewTasksButton" onclick="ToggleNewTaskRow()" value="Opret Ny Opgave" type="button" />
            </div>
        </div>

        <%--<!--INPUT FORM STARTS HERE: THIS IS FOR MAKING NEW TASKS!-->--%>
        <div class="row NoPrint">
            <div runat="server" id="InputRow">
                <%--<!--Left Column Bigger one-->--%>
                <div class="col-md-6 col-md-offset-1">
                    <div id="form_MainTaskID_Group" class="form-group">
                        <label for="Input_Task_MainTaskID" >MainTaskID</label>
                        <input type="text" class="form-control" id="Input_Task_MainTaskID" readonly="true" runat="server" />
                    </div>
                    <div class="form-group">
                        <label for="Input_Task_Name">Opgave Navn</label>
                        <input type="text" maxlength="200" class="form-control" id="Input_Task_Name" placeholder="Reperation i køkken" runat="server" />
                    </div>
                    <div class="form-group">
                        <label for="Input_Task_Action">Action</label>
                        <textarea maxlength="1000" onkeyup="CountChar(this)" id="Input_Task_Action" class="form-control max" rows="9" placeholder="KæmpeSTOR opgavebeskrivelse her!" runat="server"></textarea>
                        <label id="CharCounter">1000</label>
                    </div>
                    <asp:Button CssClass="btn btn-info" ID="Input_Task_Insert" runat="server" Text="Gem" OnClick="Input_Task_Insert_Click" />
                    <asp:Button CssClass="btn btn-info" ID="Input_Task_Update" runat="server" Text="Opret Underopgave" OnClick="Input_SubTask_Insert_Click" />
                    <input id="Button_GoBack" type="button" class="btn btn-danger" value="Fortryd" onclick="ReturnToStandard()" />
                </div>
                <%--<!--Right column, smaller one!-->--%>
                <div class="col-md-2">
                    <div class="form-group">
                        <label for="Input_StartDate">Forventet Start</label>
                        <input id="Input_Task_StartDate" type="date" class="form-control" runat="server" />
                    </div>
                    <div class="form-group">
                        <label for="Input_Task_Deadline">Opgave Frist</label>
                        <input id="Input_Task_Deadline" type="date" class="form-control" runat="server" />
                    </div>
                    <div class="form-group">
                        <label for="Input_Task_Staff">Personale</label>
                        <input id="Input_Task_Staff" maxlength="200" type="text" class="form-control" runat="server" />
                    </div>
                    <div class="form-group">
                        <label for="Input_Task_Department">Afdeling</label>
                        <asp:DropDownList ID="Input_Task_Department" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                    <div class="input-group">
                        <div class="input-group-addon">DKK</div>
                        <input type="text" class="form-control" id="Input_Task_Price" placeholder="50,75" runat="server" />
                        <div class="input-group-addon">,-</div>
                    </div>
                    <div class="checkbox">
                        <label>
                            <input id="Input_Task_IsPriority" runat="server" type="checkbox" /> Prioritets Opgave
                        </label>
                    </div>
                </div>
            </div>
        </div>
        <div id="EditTaskRow" runat="server" class="row NoPrint">
            <div class="col-md-6 col-md-offset-1">
                <div class="form-inline">
                    <input readonly="true" type="number" id="Edit_TaskID" class="form-control" runat="server" />
                    <asp:Button CssClass="btn btn-info" ID="Edit_TaskButton" runat="server" Text="Redigér" OnClick="Edit_TaskButton_Click" />
                    <input type="button" class="btn btn-danger" id="Edit_AbortEditTaskButton" onclick="HideEditTaskRow()" runat="server" value="Fortryd" />
                </div>
            </div>
        </div>
        <div id="EditTaskInputs" runat="server" class="row NoPrint">
            <%--<!--Left Column-->--%>
            <div class="col-md-6 col-md-offset-1">
                <div class="form-group">
                    <label for="Edit_TaskName">Opgave Navn</label>
                    <input type="text" maxlength="200" class="form-control" id="Edit_TaskName" placeholder="Reperation i køkken" runat="server" />
                </div>
                <div class="form-group">
                    <label for="Edit_TaskAction">Action</label>
                    <textarea maxlength="1000" onkeyup="CountChar(this)" id="Edit_TaskAction" class="form-control max" rows="9" placeholder="KæmpeSTOR opgavebeskrivelse her!" runat="server"></textarea>
                    <label id="CharCounter2">1000</label>
                </div>
                <asp:Button CssClass="btn btn-info" ID="Button_EditTask_Confirm" runat="server" Text="Gem" OnClick="Button_EditTask_Confirm_Click" />
                <asp:Button CssClass="btn btn-danger" ID="Button_EditTask_Abort" runat="server" Text="Fortryd" OnClick="Button_EditTask_Abort_Click" />
            </div>
            <%--<!--Right Column-->--%>
            <div class="col-md-2">
                <div class="form-group">
                    <label for="Edit_StartDate">Forventet Start</label>
                    <input id="Edit_StartDate" type="date" class="form-control" runat="server" />
                </div>
                <div class="form-group">
                    <label for="Edit_Deadline">Opgave Frist</label>
                    <input id="Edit_Deadline" type="date" class="form-control" runat="server" />
                </div>
                <div class="form-group">
                    <label for="Edit_Staff">Personale</label>
                    <input id="Edit_Staff" maxlength="200" type="text" class="form-control" runat="server" />
                </div>
                <div class="form-group">
                    <label for="Edit_DepartmentDropdown">Afdeling</label>
                    <asp:DropDownList ID="Edit_DepartmentDropdown" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>
                <div class="input-group">
                    <div class="input-group-addon">DKK</div>
                    <input type="text" class="form-control" id="Edit_Price" placeholder="50,75" runat="server" />
                    <div class="input-group-addon">,-</div>
                </div>
                <div class="checkbox">
                    <label>
                        <input id="Edit_PriorityTask" runat="server" type="checkbox" /> Prioritets Opgave
                    </label>
                </div>
            </div>
        </div>
    </form>
    <script>
        HideNewTaskRow();
        HideSubTasks();
        HideMoreInfoRows();
        HideCreateSubTaskTools();
        HideEditTaskRow();
    </script>
</body>
</html>
