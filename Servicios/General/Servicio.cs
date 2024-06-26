﻿using AccesoADatos;
using AutoMapper;
using Dominio.Excepciones;
using Dominio.Interfaces;

namespace Servicios
{
    public class Servicio<Modelo,DTO> : IServicio<DTO>
        where Modelo : ICopiable<Modelo>, IValidable
    {
        protected IRepositorio<Modelo> _repositorio;
        protected IMapper _mapeador;

        public Servicio(IMapper mapeador, IRepositorio<Modelo> repositorio)
        {
            _repositorio = repositorio;
            _mapeador = mapeador;
        }

        public virtual void Crear(DTO dto)
        {
            ExcepcionSiClaseNula(dto);
            Modelo modelo = _mapeador.Map<Modelo>(dto);
            modelo.Validar();
            ControlesAntesDeCrear(dto);
            _repositorio.Crear(modelo);
        }

        public void Borrar(int id)
        {
            ExcepcionSiIdInvalido(id);
            _repositorio.Borrar(id);
        }

        public void Actualizar(int id, DTO dto)
        {
            ExcepcionSiClaseNula(dto);
            ControlesAntesDeActualizar(id, dto);

            var original = _repositorio.GetPorId(id);
            var actualizado = _mapeador.Map<Modelo>(dto);

            original.Copiar(actualizado);
            _repositorio.Actualizar(original);
        }

        #region  Excepciones y controles
        protected virtual void ControlesAntesDeCrear(DTO dto)
        {
        }
        protected virtual void ControlesAntesDeActualizar(int id, DTO dto)
        {
        }
        protected void ExcepcionSiClaseNula(DTO clase)
        {
            if (clase == null)
            {
                throw new ExcepcionElementoInvalido("Clase Inválida");
            }
        }
        protected void ExcepcionSiIdInvalido(int id)
        {
            if (id < 0)
            {
                throw new ExcepcionElementoInvalido("Id inválido");
            }
        }
        protected void ExcepcionSiObjetoNoExiste(object o)
        {
            if (o == null)
            {
                throw new ExcepcionElementoInvalido("No existe un objeto con ese id");
            }
        }
        #endregion
    }
}