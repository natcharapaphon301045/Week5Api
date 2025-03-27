$(function () {
    $("#createStudentForm").on("submit", function (event) {
        event.preventDefault();

        var studentData = {
            studentName: $("#studentName").val(),
            major: $("#major").val()
        };

        $.ajax({
            type: "POST",
            url: "/Student/Create",
            data: studentData,
            success: function (response) {
                alert("Student added successfully!");
                $("#createStudentModal").modal("hide");
            },
            error: function () {
                alert("Error adding student.");
            }
        });
    });
});
