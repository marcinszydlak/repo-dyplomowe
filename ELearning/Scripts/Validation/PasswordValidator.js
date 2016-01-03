$(document).ready(function () {
    $('#diff').attr('style', 'color:red');
    $('#diff').hide();
    $('#Login').prop('disabled',true)
    var x = $('#Password').val();
    var y = $('#Powtorz').val();

    $('#Powtorz').change(function () {
        x = $('#Password').val();
        y = $('#Powtorz').val();
        if (x != y) {
            $('#diff').show();
            $("#save").prop("disabled", true);
        }
        else {
            $('#diff').hide();
            $("#save").prop("disabled", false);
        }
    });

    $('#Password').change(function () {
        x = $('#Password').val();
        y = $('#Powtorz').val();
        if (x != y) {
            $('#diff').show();
            $("#save").prop("disabled", true);
        }
        else {
            $('#diff').hide();
            $("#save").prop("disabled", false);
        }
    });
});

