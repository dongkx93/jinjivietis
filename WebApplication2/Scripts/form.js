
if ($('#addSalaryDetailFilebtn')) {
    $('#addSalaryDetailFilebtn').click(function () {
        var salaryMonth = $("#salaryForm #SalaryMonth").val();
        $("#salaryUploadViewForm #moveUploadViewSalaryMonth").val(salaryMonth);
        $("#moveUploadViewSubmitbtn").click();
        return false;
    });
}
