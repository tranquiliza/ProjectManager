﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="TableStyling.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous"/>
    
    <script type="text/javascript">
        function UpdateStatus(TaskID, NewStatus)
        {
            PageMethods.UpdateStatus(TaskID, NewStatus);
        }
    </script>

</head>

<body>
    <form id="Form1" runat="server">
        <asp:ScriptManager EnablePageMethods="true" ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="row">
            <div class="col-md-8 col-lg-offset-2">
                <asp:Table ID="Table_Tasks" runat="server">

                </asp:Table>
            </div>
        </div>
    </form>
</body>
</html>