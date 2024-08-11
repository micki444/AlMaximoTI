
const _modeloProducto = {
    idProducto: 0,
    clave: "",
    nombre: "",
    refTipoProducto: {
        id: 0,
        nombre: ""
    },
    esActivo: 0,
    precio: 0.0,
    proveedores: [
        {
            productoId: 0,
            refProveedor: {
                id: 0,
                nombre: ""
            },
            claveProveedor: "",
            costo: 0.0
        }
    ]
};

document.addEventListener('DOMContentLoaded', function () {
    cargarTiposProducto();
    document.getElementById('searchForm').addEventListener('submit', function (event) {
        event.preventDefault();
        MostrarProductos();
    });
});

function cargarTiposProducto() {
    fetch('/Home/ObtenerTiposProducto')
        .then(response => response.json())
        .then(data => {
            const select = document.getElementById('tipo');
            data.forEach(tipo => {
                const option = document.createElement('option');
                option.value = tipo.nombre;
                option.text = tipo.nombre;
                select.appendChild(option);
            });
        })
        .catch(error => console.error('Error al cargar tipos de producto:', error));
}

function MostrarProductos() {
    var clave = document.getElementById('clave').value;
    var tipo = document.getElementById('tipo').value;

    

    fetch(`/Home/Buscar?clave=${clave}&tipo=${tipo}`)
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            console.log(responseJson); // Verifica los datos en la consola
            if (responseJson.length > 0) {
                $("#tablaProductos tbody").html(""); // Limpiar el contenido existente

                responseJson.forEach((producto) => {
                    $("#tablaProductos tbody").append(
                        $("<tr>").append(
                            $("<td>").text(producto.clave),
                            $("<td>").text(producto.nombre),
                            $("<td>").text(producto.refTipoProducto.nombre), // Acceder a la propiedad anidada
                            $("<td>").text(producto.esActivo ? "Sí" : "No"), // Convertir byte a texto
                            $("<td>").text(producto.precio !== undefined ? producto.precio.toFixed(2) : "N/A"), // Verificar si Precio está definido
                            $("<td>").append(
                                $("<button>").addClass("btn btn-primary btn-sm boton-editar-producto").text("Editar").data("dataProducto", producto),
                                $("<button>").addClass("btn btn-danger btn-sm ms-2 boton-eliminar-producto").text("Eliminar").data("dataProducto", producto)
                            )
                        )
                    );
                });
            } else {
                $("#tablaProductos tbody").html("<tr><td colspan='6'>No se encontraron productos.</td></tr>");
            }
        })
        .catch(error => console.error('Error:', error));
}


$(document).on("click", ".boton-nuevo-empleado", function () {

    _modeloEmpleado.idEmpleado = 0;
    _modeloEmpleado.nombreCompleto = "";
    _modeloEmpleado.idDepartamento = 0;
    _modeloEmpleado.sueldo = 0;
    _modeloEmpleado.fechaContrato = "";

    MostrarModal();

})

$(document).on("click", ".boton-editar-empleado", function () {

    const _empleado = $(this).data("dataEmpleado");


    _modeloEmpleado.idEmpleado = _empleado.idEmpleado;
    _modeloEmpleado.nombreCompleto = _empleado.nombreCompleto;
    _modeloEmpleado.idDepartamento = _empleado.refDepartamento.idDepartamento;
    _modeloEmpleado.sueldo = _empleado.sueldo;
    _modeloEmpleado.fechaContrato = _empleado.fechaContrato;

    MostrarModal();

})

$(document).on("click", ".boton-guardar-cambios-empleado", function () {

    const modelo = {
        idEmpleado: _modeloEmpleado.idEmpleado,
        nombreCompleto: $("#txtNombreCompleto").val(),
        refDepartamento: {
            idDepartamento: $("#cboDepartamento").val()
        },
        sueldo: $("#txtSueldo").val(),
        fechaContrato: $("#txtFechaContrato").val()
    }


    if (_modeloEmpleado.idEmpleado == 0) {

        fetch("/Home/guardarEmpleado", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {

                if (responseJson.valor) {
                    $("#modalEmpleado").modal("hide");
                    Swal.fire("Listo!", "Empleado fue creado", "success");
                    MostrarEmpleados();
                }
                else
                    Swal.fire("Lo sentimos", "No se puedo crear", "error");
            })

    } else {

        fetch("/Home/editarEmpleado", {
            method: "PUT",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {

                if (responseJson.valor) {
                    $("#modalEmpleado").modal("hide");
                    Swal.fire("Listo!", "Empleado fue actualizado", "success");
                    MostrarEmpleados();
                }
                else
                    Swal.fire("Lo sentimos", "No se puedo actualizar", "error");
            })

    }


})


$(document).on("click", ".boton-eliminar-empleado", function () {

    const _empleado = $(this).data("dataEmpleado");

    Swal.fire({
        title: "Esta seguro?",
        text: `Eliminar empleado "${_empleado.nombreCompleto}"`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si, eliminar",
        cancelButtonText: "No, volver"
    }).then((result) => {

        if (result.isConfirmed) {

            fetch(`/Home/eliminarEmpleado?idEmpleado=${_empleado.idEmpleado}`, {
                method: "DELETE"
            })
                .then(response => {
                    return response.ok ? response.json() : Promise.reject(response)
                })
                .then(responseJson => {

                    if (responseJson.valor) {
                        Swal.fire("Listo!", "Empleado fue elminado", "success");
                        MostrarEmpleados();
                    }
                    else
                        Swal.fire("Lo sentimos", "No se puedo eliminar", "error");
                })

        }



    })

})