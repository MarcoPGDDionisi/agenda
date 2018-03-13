/*****************************************************************
    Carrega Inputs --> Variavel
   ******************************************************************/
email_readInputs = function () {
    var InputValues = {
        IdEmail: $("#IdEmail").val(),
        IdContato: $("#IdContato").val(),
        Email: $("#Email").val()
    }
    return InputValues;
}


/*****************************************************************
 Evento ==>  $("#btn_email_incluir").click()
******************************************************************/
$("#btn_email_incluir").on("click", function () {
    $("#IdEmail").val('0');
    $("#Email").val('');

})

/*****************************************************************
 Carrega  Variavel --> Inputs
******************************************************************/
Email_readWrite = function (obj) {
    $("#IdEmail").val(obj.IdEmail);
    $("#Email").val(obj.Email);
}

/*****************************************************************
 Evento ==>  $("#btn_email_save").click()
******************************************************************/

$("#btn_email_save").on("click", function () {
   
    user_contatos = email_readInputs();
    if (email_validade()) {
        obj = comum.post('/Home/email_InsertUpdate/', { 'objList': user_contatos });
        Email_readWrite(obj.objList);
        Email_populateDataTable($('#IdContato').val())
        alert("Inclusão/Alteração efetuada com Sucesso !!!")
    }

})

/*****************************************************************
 Evento ==>  $("#btn_email_delete").click()
******************************************************************/
$("#btn_email_delete").on("click", function () {
    if ($("#IdContato").val() === '0') {
        alert('Contato Inexistente !!!')
    }
    else if ($("#IdEmail").val() === '0') {
        alert('Email Inexistente !!!')
    }
    else {
        if (confirm('Confirma exclusão desse Email ?')) {
            comum.post('/Home/Email_Excluir/', { 'IdEmail': $("#IdEmail").val() });
            alert('Email Excluído com Sucesso !!!')
            Email_populateDataTable($('#IdContato').val())         
            $("#btn_email_incluir").click();
        }
        else {
            alert('Exclusão Cancelada pelo Usuário');
        }
    }
})

/*****************************************************************
 Contato --> Valida os Dados Inseridos
******************************************************************/
email_validade = function () {

    var msg = '';

    if ($("#IdContato").val() === '0') {
        msg += '\n\n Efetivar o Cadastro do Contato';
    }

    if ($("#Email").val().replace(new RegExp(' ', 'g'), '') === '') {
        msg += '\n\nEmail - Preenchimento Obrigatório;';
    }
    if (msg !== '') {
        alert('Favor Corrogir as seguintes informações:' + msg)
        return false
    }
    return true;

}

Email_populateDataTable = function (IdContato) {

    objList = comum.post('/Home/email_SelectList/', { 'IdContato': IdContato });

    $('#TbEmail').DataTable({
        "data": objList.objList,
        "dom": 'Bfrtip',
        "async": "false",
        "bDestroy": true,
        "paging": false,
        "extend": 'excelHtml5',
        "pageLength": 50,
        "scrollY": "200px",
        "scrollCollapse": true,
        "height": "600px",
        "decimal": ",",
        "thousands": ".",
        "bFilter": false,
        "emptyTable": "No data available in table",
        "language": {
            "info": "",
            "lengthMenu": "",
            "infoFiltered": "(Filtrado de _MAX_ total registros)",
            "processing": "<div><img src='~/images/waiting.gif' /></div>",
            "search": "Busca:",
            "loadingRecords": "Aguarde - Carregando Grid ...",
            "zeroRecords": "Não foram licalizados Registros",
            "paginate": {
                "first": "Primeiro",
                "last": "Último",
                "next": "Próximo",
                "previous": "Anterior"
            }
        },
        "select": {
            style: 'single',
            info: false
        },
        columns: [
            { title: "Email", data: "Email" }
        ]
    });

}


$(document).ready(function () {
  

    //CargaTemporaria_Contatos()
    //Email_populateDataTable($('#IdContato').val())


    //$('#TbTelefone tbody').on('click', 'tr', function (e) {
    //    row_data = $('#TbTelefone').DataTable().row(e.target._DT_CellIndex.row).data()
    //    email_readWrite(row_data);
    //});

});