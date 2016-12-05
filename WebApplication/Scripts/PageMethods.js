//Prevent user from user enter key to make mistakes!
function UpdateStatus(TaskID, NewStatus)
{
    PageMethods.UpdateStatus(TaskID, NewStatus);
}

function CountChar(val) {
    var len = val.value.length;
    if (len >= 1000) {
        val.value.substring(0, 1000);
    }
    else {
        $('#CharCounter').text(999 - len);
    }
}

function HideMoreInfoRows()
{
    $(document).ready(function(){
        $('.HiddenInfoRow').hide();
    })
}

function HideSubTasks() {
    $(document).ready(function () {
        $('.ImportantSubRow').hide();
        $('tr[class*=Maintask]').hide();
    })
}

$(".btnSeccion").click(function (event) { //What is this??
    btnMostrarSeccion($(this));
    event.preventDefault();
})

function HideNewTaskRow()
{
    $(document).ready(function () {
        $('#InputRow').hide();
    })
}

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

function ToggleSubTasks(MainTaskID)
{
    $(document).ready(function () {
        $('tr[class*=Maintask' + MainTaskID + ']').toggle(); //This will unfold partial matches too. How to fix?
        $('tr[class*=SubInfo' + MainTaskID + ']').hide();
    })
}

function ToggleMoreInfo(MainTaskID)
{
    $(document).ready(function () {
        $('#TaskInfo' + MainTaskID).toggle();
    })
}

function HideTableButtons() {
    $('.TableButton').hide();
}


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