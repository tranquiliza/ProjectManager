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
            $('#ToggleEditButton').prop('value', 'Rediger');
            ButtonsToggled = false;
        }
        else
        {
            $('#ToggleEditButton').prop('value', 'Afslut redigering');
            ButtonsToggled = true;
        }
    })
}