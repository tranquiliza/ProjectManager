//Prevent user from user enter key to make mistakes!
function UpdateStatus(TaskID, NewStatus)
{
    PageMethods.UpdateStatus(TaskID, NewStatus);
}

function HideSubTasks() {
    $(document).ready(function () {
        $('.ImportantSubRow').hide();
        $('tr[class*=Maintask]').hide();
    })
}

$(".btnSeccion").click(function (event) {
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
        $('tr[class*=Maintask' + MainTaskID + ']').toggle();
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