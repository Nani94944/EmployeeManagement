$(document).ready(function () {
    $('.delete-btn').click(function () {
        var id = $(this).data('id');
        if (confirm('Are you sure you want to delete this employee?')) {
            $.ajax({
                url: '/Employees/Delete/' + id,
                type: 'POST',
                success: function (result) {
                    if (result.success) {
                        alert(result.message);
                        location.reload();
                    } else {
                        alert(result.message);
                    }
                },
                error: function () {
                    alert('An error occurred while deleting the employee.');
                }
            });
        }
    });
});