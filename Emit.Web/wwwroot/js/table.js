var userdata = [];

const table = $('#empoyees').dataTable({
    "processing": true,
    "pageLength": 20,
    "ajax": {
        "url": '/api/identity',
        'type': 'get',
        "dataFilter": function (response) {
            addEventListeners();
            userdata = JSON.parse(response).data;
            return response;
        },
    },
    "columns": [
        {
            "data": function (data, type, row) {
                return `<div class="info" data-id='${data.id}' ><i class="fas fa-info-circle text-primary"></i></div>`
            }
        },
        { "data": "email", "name": "email", "autoWidth": true },
        {
            "data": function (data, type, row) {
                if (data.isConfirmed) {
                    return ` <div class="pl-4"><i class="fas fa-user-check" style="font-size:2em;color:forestgreen"></i></div>`
                }
                else {
                   return ` <div class="pl-4"><i class="fas fa-user-times" style="font-size:2em;color:crimson"></i></div>`
                }
            }
        },
        { "data": "sector", "name": "sector", "autoWidth": true },
        {
            "data": function (data, type, full, row) {
                return `
                <span class="edit" data-id='${data.id}'><i class="fas fa-user-edit" style="font-size:2em;color:forestgreen;cursor:pointer"></i></span>
                <span class="delete" data-id='${data.id}'><i class='fas fa-trash-alt' style="font-size:2em;color:crimson;cursor:pointer"></i></span>
            `
            }
        }
    ],
    'columnDefs': [{
        "targets": [0,4], 
        "searchable": false,
        "orderable": false 
    }],
    "order": [[1, "asc"]]
});  

function addEventListeners() {
    $("#empoyees").on('click', '.delete', function (e) {
        var id = $(this).attr('data-id')
        const options = {
            method: 'DELETE',
            headers: { 'Content-Type': 'application/json' },
        }
        fetch("api/identity/" + id, options)
            .catch(err => console.log(err.toString()))
        $(this).closest("tr").fadeOut();
    });
    $("#empoyees").on('click', '.edit', function (e) {
        var id = $(this).attr('data-id')
        $('#exampleModalCenter').modal("show");
        fetch('user/' + id + "/edit")
            .then(x => x.text())
            .then(function (html) {
                console.log(html)
                $("#edit-user-body").html(html);
            })
    });
    $("#empoyees").on('mouseenter', '.info', function (e) {
        var element = $(this);
        var id = $(this).attr('data-id')
        var user = userdata.find(x => x.id == id)
        console.log(user)
        var list = `<div class='info-display' style='top:${-(user.roles.length * 20)}px'>`;
        user.roles.forEach(x => list += `<span>${x}</span>`);
        list += "</div>"
        element.append(list).on("mouseleave", function () {
            $(this).children('div').fadeToggle().remove();
        });
    });
};