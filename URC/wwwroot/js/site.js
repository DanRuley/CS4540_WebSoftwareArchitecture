/* Author:    Dan Ruley
  Partner:   None
  Date:      October, 2020
  Course: CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Dan Ruley - This work may not be copied for use in Academic Coursework.

  I, Dan Ruley, certify that I wrote this code from scratch and did
  not copy it in part or whole from another source.  Any references used
  in the completion of the assignment are cited in my README file and in
  the appropriate method header.

  Javascript that is called when the checkboxes in the RolesTable view are clicked.
  It uses AJAX to call the ChangeRole function in the Controller and updates the database
  with the desired result.
*/

/*determines if the request is to add or delete, and creates the appropriate JSON user data object, 
then uses AJAX POST method to call the ChangeRoles function and update the DB.*/
updateDBRequest = function (usrN, rl) {
    var n = $(event.target).prop("checked");
    var actn;
    if (n) {
        actn = 'add';
    }
    else {
        actn = 'remove';
    }

    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    //Adding a role
    if (actn == 'add') {
        swal.fire({
            title: "Do you really want to add role: " + rl + "\n\nTo the user: " + usrN + "?",
            showCancelButton: true,
            confirmButtonText: 'Yes, I understand this action will update the UsersRoles Database',
        }).then((result) => {
            //confirmed add ==> AJAX DB change call
            if (result.isConfirmed) {
                $.ajax(
                    {
                        url: "ChangeRole",
                        type: "POST",
                        data: {
                            userName: usrN,
                            role: rl,
                            action: actn
                        },

                        success: function (data) {
                            toastr["success"]("Role has been successfully changed");
                        },
                        error: function () {
                            toastr["error"]("Server Error. Try Again");
                        }
                    });
            }
            //Add was canceled ==> no DB change
            else {
                $(event.target).prop("checked", false);
                swal.fire("Cancelled, role was not added", {
                    icon: "cancelled",
                }).then((result) => { reload(); });
            }
        });
    }
    else {
        swal.fire({
            title: "Do you really want to remove role: " + rl + "\n\nFrom the user: " + usrN + "?",
            showCancelButton: true,
            confirmButtonText: 'Yes, I understand this action will update the UsersRoles Database',
        }).then((result) => {

            if (result.isConfirmed) {
                //confirmed removal ==> AJAX DB change call
                $.ajax(
                    {
                        url: "ChangeRole",
                        type: "POST",
                        data: {
                            userName: usrN,
                            role: rl,
                            action: actn
                        },

                        success: function (data) {
                            toastr["success"]("Role has been successfully changed");
                        },
                        error: function () {
                            toastr["error"]("Server Error. Try Again");
                        }
                    });

            }
            //Remove was cancelled ==> no DB change
            else {
                $(event.target).prop("checked", true);
                toastr["success"]("Role has been successfully changed").then((result) => { reload(); });
            }
        });
    }
}

/*Reloads the page*/
function reload() {
    location.reload();
}

/*Deletes the user from the DB*/
function deleteUsr(usrN, rl) {
    swal.fire({
        title: "Do you really want to delete user: " + usrN + "?",
        showCancelButton: true,
        confirmButtonText: 'Yes, I understand this action will update the UsersRoles Database',
    }).then((result) => {
        //confirmed add ==> AJAX DB change call
        if (result.isConfirmed) {
            $.ajax(
                {
                    url: "DeleteUser",
                    type: "POST", //HTTP POST Method  
                    data: {
                        userName: usrN,
                        role: rl,
                        action: 'delete'
                    },

                    success: function (data) {
                        swal.fire("Database has been updated", {
                            icon: "success",
                        }).then(reload());
                    },
                    error: function () {
                        swal.fire("Error: something went horribly wrong.", {
                            icon: "error",
                        });
                    }
                });
        }
        else {
            $(event.target).prop("checked", true);

            swal.fire("Cancelled, no change made.", {
                icon: "cancelled",
            }).then();
        }
    });
}

async function AJAXSubmit(oFormElement) {
    const formData = new FormData(oFormElement);

    try {
        const response = await fetch(oFormElement.action, {
            method: 'POST',
            headers: {
                'RequestVerificationToken': getCookie('RequestVerificationToken')
            },
            body: formData
        });

        data = await response.json()
        if (response.ok) {
            var filestr = data.message;
            $("#response").html("<hr> <div style=\"padding: 5px; margin-left: 75px; font: 15px;\">Success, the following image has been uploaded to the server and will be associated with your Opportunity: </div>");
            $("#response").append('<img style="width: 150px; height: 150px; padding: 5px; margin-left: 75px;" src="/uploads/' + data.message + '"/> <hr>');
            $("#response").data = data.message;
            document.getElementById("ImgFile").value = '/uploads/' + data.message;
        }
        else {
            $("#response").html("Error: " + data.message);
        }
        
    } catch (error) {
        $("#response").html("Error: " + error);
    }
}

function getCookie(name) {
    var value = "; " + document.cookie;
    var parts = value.split("; " + name + "=");
    if (parts.length == 2) return parts.pop().split(";").shift();
}


/*Performs a post request and uploads the selected image file to the server*/
async function AJAXImageUpload(oFormElement, type) {
    const formData = new FormData(oFormElement);
try {
    const response = await fetch(oFormElement.action, {
        method: 'POST',
        body: formData
    });
    data = await response.json();

    if (response.ok) {
        if (type == 'professor') {
            var filestr = data.message;
            $("#response-professor").html("<hr> <div style=\"padding: 5px; margin-left: 75px; font: 15px;\">Success, the following image has been uploaded to the server and will be associated with your Opportunity: </div>");
            $("#response-professor").append('<img style="width: 150px; height: 150px; padding: 5px; margin-left: 75px;" src="/uploads/' + data.message + '"/> <hr>');
            $("#response-professor").data = data.message;
            document.getElementById("ProfessorImage").value = '/uploads/' + data.message;
        }
        if (type == 'generic') {
            $("#response-generic").html("<hr> <div style=\"padding: 5px; margin-left: 75px; font: 15px;\">Success, the following image has been uploaded to the server and will be associated with your Opportunity: </div>");
            $("#response-generic").append('<img style="width: 150px; height: 150px; padding: 5px; margin-left: 75px;" src="/uploads/' + data.message + '"/> <hr>');
            $("#response-generic").data = data.message;
            document.getElementById("ProfessorImage").value = '/uploads/' + data.message;
        }
    }
    else {
        if (type == 'professor')
            $("#response-professor").html("Error: " + data.message);
        if (type == 'generic')
            $("#response-generic").html("Error: " + data.message);
        }
}
catch (error) {
    if (type == 'professor')
        $("#response-professor").html("Error: " + error);
    if (type == 'generic')
        $("#response-generic").html("Error: " + error);
}
}

/*  Used to perform a post request and upload the selected file to the server.
 */
async function AJAXResumeUpload(oFormElement) {
    const formData = new FormData(oFormElement);
    try {
        const response = await fetch(oFormElement.action, {
            method: 'POST',
            body: formData
        });
        data = await response.json();

        if (response.ok) {
            var filestr = data.message;
            $("#response").html("<hr> <div style=\"padding: 5px; margin-left: 50px; font: 15px;\">Success, the resume file you uploaded is attached to your application: </div>");
            $("#response").data = data.message;
            document.getElementById("ResumeFile").value = '/uploads/' + data.message;
        }
        else {
            $("#response").html("Error: " + data.message);
        }
    }
    catch (error) {
        $("#response").html("Error: " + error);
    }
}

/*Simple swal alert*/
function activateApplication(appID, action) {
    swal.fire({ title: "Updating database..." });
}

/*Called by student search to do the post request and update the HTML of the student summaries.*/
function FindStudents() {
    document.getElementById("SearchResult").innerHTML = "";
    document.getElementById("ResultTable").innerHTML = "";
    var data = {
        SearchString: document.getElementById("StudSearch").value
    };
    if (data.SearchString == "") {
        $("#SearchResult").append('Search string must not be empty.');
    }
    else {
        $.post("GetStudentSummary", data).done(function (result) {
            if (result.length == 0) {
                $("#SearchResult").append('No matches found.');
                return;
            }

            $("#SearchResult").append('Students matching "' + data.SearchString + '":');
            $("#ResultTable").append('<tr>' +
                '<th>ID</th>' +
                '<th>Name</th>' +
                '<th>GPA</th>' +
                '<th>Student Summary</th>' +
                '<th>Details</th></tr>');

            for (i = 0; i < result.length; i++) {
                $("#ResultTable").append('<tr>' +
                    '<td>' + `${result[i].id}` + '</td>' +
                    '<td>' + `${result[i].name}` + '</td>' +
                    '<td>' + `${result[i].gpa}` + '</td>' +
                    '<td>' + `${result[i].statement_Summary}` + '</td>' +
                    '<td>' + '<a href="/StudentApplications/Details/' + result[i].id + '"> Details </a>' + '</tr>');
            }
        })
            .fail(function (result) {
                $("#ResultTable").append('Error, something went wrong');
            });
    }
}

/* Called when user clicks on the notification icon **/
function ShowNotificationList() {
    let notificationElement = document.getElementById("notification");
    notificationElement.classList.toggle("show-notification-list");
}

function ClearNotification(notificationId) {
    $.ajax(
        {
            url: "/Home/ClearNotification",
            type: "POST",
            data: {
                notificationId: notificationId,
            },

            success: function () {
                $(`#notification-${notificationId}`).remove();
                let notificationNumber = $("#notification-number")[0];
                let number = parseInt(notificationNumber.innerText);
                if (number == 1) {
                    notificationNumber.remove();
                } else {
                    notificationNumber.textContent = number - 1;
                }
            },
            error: function () {
                swal.fire("Error: something went horribly wrong.", {
                    icon: "error",
                });
            }
        });
}

function ClearAllNotification(userId) {
    $.ajax(
        {
            url: "/Home/ClearAllNotification",
            type: "POST",
            data: {
                userId: userId,
            },

            success: function () {
                $(`#notification-list`).remove();
                $("#notification-number").remove();
            },
            error: function () {
            }
        });
}

/* Window Event to trigger when user clicks outside of the dropdown to re hide it 
 * Source: https://www.tutorialrepublic.com/faq/hide-dropdown-menu-on-click-outside-of-the-element-in-jquery.php
 * **/
$(document).on("click", function (event) {
    let notificationIcon = $("#notification-icon");
    if (document.getElementById("notification-icon") != null && notificationIcon !== event.target && !notificationIcon.has(event.target).length) {
        document.getElementById("notification").classList.remove("show-notification-list");
    }
});
