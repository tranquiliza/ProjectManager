<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Project-Manager Main</title>
    <link rel="stylesheet" type="text/css" href="TableStyling.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script src="Scripts/PageMethods.js"></script>
</head>

<body>
    
    <form id="Form1" runat="server">
        
        <!--Row for controls! -->
        <div runat="server" class="row" id="Controls">
            <div class="col-md-8 col-md-offset-2">
                <input class="btn btn-info" id="ToggleEditButton" onclick="ToggleTableButtons()" value="Redigér" type="button" />
            </div>
        </div>
        <!--We need this is order to make our javascript update buttons work!-->
        <asp:ScriptManager EnablePageMethods="true" ID="ScriptManager2" runat="server"></asp:ScriptManager>
        <!--Row for the table!-->
        <div class="row" id="table">
            <div class="col-md-8 col-md-offset-2">
                <asp:Table ID="Table_Tasks" runat="server">

                </asp:Table>
            </div>
        </div>
        <!--Row for some table controls i suppose.-->
        <div runat="server" class="row" id="RowNewTaskButton">
            <div style="align-content:center" class="col-md-8 col-md-offset-2">
                <input class="btn btn-info" id="ToggleNewTasksButton" onclick="ToggleNewTaskRow()" value="Opret Ny Opgave" type="button" />
            </div>
        </div>

        <!--INPUT FORM STARTS HERE: THIS IS FOR MAKING NEW TASKS!-->
        <div class="row">
            <div runat="server" id="InputRow">
                <!--Left Column Bigger one-->
                <div class="col-md-6 col-md-offset-2">
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
                <!--Right column, smaller one!-->
                <div class="col-md-2">
                    <div class="form-group">
                        <label for="Input_StartDate">Forventet Start</label>
                        <input id="Input_Task_StartDate" type="date" class="form-control" runat="server" />
                    </div>
                    <div class="form-group">
                        <label for="StartDate">Opgave Frist</label>
                        <input id="Input_Task_Deadline" type="date" class="form-control" runat="server" />
                    </div>
                    <div class="form-group">
                        <label for="DropDownList1">Personale</label>
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
    </form>
    <script>
        HideTableButtons();
        HideNewTaskRow();
        HideSubTasks();
        HideMoreInfoRows();
        HideUpdateTaskTools();
    </script>
</body>
</html>
