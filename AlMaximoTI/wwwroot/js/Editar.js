document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('editarForm');

    if (form) {
        form.addEventListener('submit', function (event) {
            event.preventDefault();

            const id = document.getElementById('id').value;
            const clave = document.getElementById('clave').value;
            const nombre = document.getElementById('nombre').value;
            const tipo = document.getElementById('tipo').value;
            const precio = document.getElementById('precio').value;

            if (!clave || !nombre || !tipo || !precio) {
                alert('Todos los campos son obligatorios.');
                return;
            }

            const data = {
                id: id,
                clave: clave,
                nombre: nombre,
                tipo: tipo,
                precio: precio
            };

            fetch('/api/productos/editar', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            })
                .then(response => response.json())
                .then(data => {
                    if (data.success) {
                        alert('Producto actualizado correctamente.');
                        window.location.href = '/productos';
                    } else {
                        alert('Error al actualizar el producto.');
                    }
                })
                .catch(error => {
                    console.error('Error:', error);
                    alert('Error al actualizar el producto.');
                });
        });
    } else {
        console.error('El formulario no se encontró en el DOM.');
    }
});
