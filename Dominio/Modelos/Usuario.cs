﻿using Dominio.Excepciones;
using Dominio.Interfaces;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dominio.Modelos
{
    public class Usuario : IValidable, ICopiable<Usuario>
    {
        public int UsuarioId { get; set; }
        public bool EsAdmin { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public string ContraseñaHasheada { get; set; }

        public void Copiar(Usuario usuario)
        {
            UsuarioId = usuario.UsuarioId;
            EsAdmin = usuario.EsAdmin;
            Nombre = usuario.Nombre;
            Apellido = usuario.Apellido;
            Email = usuario.Email;
            ContraseñaHasheada = usuario.ContraseñaHasheada;
        }

        public void Validar()
        {
            ValidarNombreYApellido(Nombre, Apellido);
            Utiles.ExcepcionSiStringVacio(Apellido, "Apellido vacío");
            Utiles.ExcepcionSiEmailInvalido(Email, "Email invalido");
            Utiles.ExcepcionSiStringVacio(ContraseñaHasheada, "Contraseña hasheada vacia");
        }
        private void ValidarNombreYApellido(string nombre, string apellido) {
            if (!EsValido(nombre))
            {
                throw new ArgumentException("El nombre no es válido.");
            }

            if (!EsValido(apellido))
            {
                throw new ArgumentException("El apellido no es válido.");
            }
        }
        private bool EsValido(string texto)
        {
            // Expresión regular que permite solo caracteres alfabéticos, espacio, apóstrofe o guión del medio
            string patron = @"^[a-zA-ZáéíóúüÁÉÍÓÚÜñÑ]+(?:[' -][a-zA-ZáéíóúüÁÉÍÓÚÜñÑ]+)*$";

            // Verifica si el texto coincide con el patrón
            return Regex.IsMatch(texto, patron);
        }
    }
}