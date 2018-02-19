$(document).ready(function () {

    $('#myTable').DataTable();



    $("#DropDownList").change(function () {
        var URL = '/Parkway.Report.Mvc/MvcReport/Index';
        var URLext = $('#DropDownList').val();
        //alert(URLext);
        URL = $.trim(URL);
        URLext = $.trim(URLext);
        URL = URL + "/" + URLext;
        $.ajax({
            url: URL,
           // data: 'selectedValue',
            type: 'Post',
            success: function (data) {
                //$('#partialView').load('/Parkway.Report.Mvc/Shared/_PartialIndex');
                $('#partialViewContainer').html(data);
            },

            error: (function (xhr, status) {
                alert(status);
            })
        })

    });



});






//$(document).ready(function () {
    //$('ReportsDropDownList').change(function () {
//function redirect() {
    //alert("ok");

    //var URL = '/MvcReport/Index';
    //var URLext = $('ReportsDropDownList').val();
    //URL = $.trim(URL);
    //URLext = $.trim(URLext);
    //URL = URL + "/" + URLext;
    //$.ajax({
    //    url: URL,
    //    type: 'Post',
    //    success: function (data) {
    //    },

    //    error: (function (xhr, status) {
    //        alert(status);
    //    })
    //})
//}
    //});
//});