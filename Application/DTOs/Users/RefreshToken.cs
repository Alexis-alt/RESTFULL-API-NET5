using System;

namespace Application.DTOs.Users
{
    public class RefreshToken
    {
        public int Id { get; set; }

        public string Token { get; set; }

        public DateTime Expires { get; set; }
        //Verifica si la fecha de expiración ya pasó
        public bool IsExpired => DateTime.Now >= Expires;
        //Hora de creación
        public DateTime Created { get; set; }
        //Creado por el host...
        public string CreatedByIp { get; set; }
        //Hora de cancelación
        public DateTime? Revoked { get; set; }
        //Host que revoca
        public string RevokedByIp { get; set; }
        //Sustituido por el token 
        public string ReplacedByToken { get; set; }

        //verifica que este activo aún
        public bool IsActive => Revoked == null && !IsExpired;
    }
}
