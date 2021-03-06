﻿//Prevent user from user enter key to make mistakes
//Our method for updating task status with a single click!

//function PrintTable()
//{
//    var divToPrint = document.getElementById("table");
//    var popupWin = window.open('', '_blank', 'width=500,height=400,location=no,left=200px');
//    popupWin.document.write('<html>' +
//        '<head>' +
//        '<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous"/>' +
//        '<link rel="stylesheet" type="text/css" href="TableStyling.css" />'+
//        '</head>' +
//        '<body onload="window.print()">' + divToPrint.innerHTML + '</body></html>');
//}

function UpdateStatus(TaskID, NewStatus)
{
    PageMethods.UpdateStatus(TaskID, NewStatus);
}

function SetApprovedComplete(TaskID)
{
    var Confirmation = confirm("Er du sikker du vil sætte denne opgave færdig?");
    if (Confirmation == true) {
        PageMethods.SetApprovedComplete(TaskID);
    }
}


//Textarea char counter.
function CountChar(val) {
    var len = val.value.length;
    if (len >= 1000) {
        val.value.substring(0, 1000);
    }
    else {
        $('#CharCounter').text(999 - len);
        $('#CharCounter2').text(999 - len);
    }
}

//Called upon page load to hide MoreInfoRows
function HideMoreInfoRows(){
    $(document).ready(function(){
        $('.HiddenInfoRow').hide();
    })
}

//Called upon page load to hide SubTasks
function HideSubTasks() {
    $(document).ready(function () {
        $('.ImportantSubRow').hide();
        $('tr[class*=Maintask]').hide();
    })
}

//No idea what this is?
$(".btnSeccion").click(function (event) { //What is this??
    btnMostrarSeccion($(this));
    event.preventDefault();
})

//Called upon load to hide the form for creating new tasks.
function HideNewTaskRow()
{
    $(document).ready(function () {
        $('#InputRow').hide();
    })
}

//Our method called when clicking the Inputrow toggle button. 
var InputRowToggled = false;
function ToggleNewTaskRow()
{
    $(document).ready(function () {
        $('#InputRow').slideToggle();
        if (InputRowToggled) {
            $('#ToggleNewTasksButton').removeClass('btn-warning');
            $('#ToggleNewTasksButton').addClass('btn-info');
            $('#ToggleNewTasksButton').prop('value', 'Opret Ny Opgave');
            InputRowToggled = false;
        }
        else {
            $('#ToggleNewTasksButton').removeClass('btn-info');
            $('#ToggleNewTasksButton').addClass('btn-warning');
            $('#ToggleNewTasksButton').prop('value', 'Skjul');
            InputRowToggled = true;
        }
    })
}
//Function to show/hide subtasks
function ToggleSubTasks(MainTaskID)
{
    $(document).ready(function () {
        $('tr[class~=Maintask' + MainTaskID + ']').toggle(); //This will unfold partial matches too. How to fix?
        $('tr[class*=SubInfo' + MainTaskID + ']').hide();
    })
}

//Function to show/hide More information
function ToggleMoreInfo(MainTaskID)
{
    $(document).ready(function () {
        $('#TaskInfo' + MainTaskID).toggle();
    })
}

//Called upon page load in order to hide edit buttons in the table (UpdateStatus) - We no longer do this, as these are controlled by login!
function HideTableButtons() {
    $('.TableButton').hide();
}

//Our toggle button for displaying the status update buttons in the table.
var ButtonsToggled = false;
function ToggleTableButtons()
{
    $(document).ready(function(){
        $('.TableButton').toggle();
        if (ButtonsToggled) {
            $('#ToggleEditButton').prop('value', 'Redigér');
            $('#ToggleEditButton').removeClass('btn-danger');
            $('#ToggleEditButton').addClass('btn-info');
            ButtonsToggled = false;
        }
        else
        {
            $('#ToggleEditButton').prop('value', 'Afslut redigering');
            $('#ToggleEditButton').removeClass('btn-info');
            $('#ToggleEditButton').addClass('btn-danger');
            ButtonsToggled = true;
        }
    })
}

function HideEditTaskRow()
{
    $(document).ready(function () {
        $('#EditTaskRow').hide();
        $('#RowNewTaskButton').show();
    })
    Editing = false;
}


function EditTask(TaskID)
{
    $(document).ready(function () {
        //Visibility Control:
        $('#RowNewTaskButton').hide();
        $('#EditTaskRow').show();
        $('#Edit_TaskID').val(TaskID); //Putting ID of the task we want to edit into the box, so user don't have to.
    })
}

function HideCreateSubTaskTools(){
    $(document).ready(function(){
        $('#form_MainTaskID_Group').hide();
        $('#Input_Task_Update').hide();
        $('#Button_GoBack').hide();
    })
}

function CreateSubTask(TaskID){
    $(document).ready(function () {
        $('#ToggleNewTasksButton').hide();
        $('#Input_Task_Insert').hide();
        $('#form_MainTaskID_Group').show();
        $('#Input_Task_Update').show();
        $('#InputRow').show();
        $('#Button_GoBack').show();
        $('#Input_Task_MainTaskID').val(TaskID); //We just put the id of the maintask into the box.
    })
}

function ReturnToStandard()
{
    $(document).ready(function () {
        $('#ToggleNewTasksButton').show();
        $('#Input_Task_Insert').show();
        $('#form_MainTaskID_Group').hide();
        $('#Input_Task_Update').hide();
        $('#InputRow').hide();
        $('#Button_GoBack').hide();
    })
}