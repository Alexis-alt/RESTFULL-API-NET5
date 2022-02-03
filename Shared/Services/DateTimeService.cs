using Application.Interfaces;
using DevExpress.Xpo;
using System;

namespace Shared.Services
{
    //En la carpeta de Clases Services se agregan servicios o funcionalidades que se comparten con todo el proyecto
    //Generalemte es una Interfaz (Alojada en la carpeta de interfaces) que tiene una clase que la implementa, esto para que pueda ser inyectada


    public class DateTimeService : IDateTimeService
    {


        //Propiedad que regresa un valos de tiempo
        public DateTime NowUtc => DateTime.UtcNow;



    }

}