 using Application.DTOs.Users;
using Application.Enums;
using Application.Exceptions;
using Application.Features.Usuarios.Commands.DeleteUserCommand;
using Application.Features.Usuarios.Commands.UpdateUserCommand;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Settings;
using Identity.Contexts;
using Identity.Helpers;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Services
{
    //SERVICIOS DE AUTENTICACIÓN



    public  class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IdentityContext _context;

        //Clase implementadora del patron options que extrae los datos del JWT del JSON
        private readonly JWTSettings _jwtSettings;
        private readonly IDateTimeService _dateTimeService;


        //Constructor
        public AccountService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, IOptions<JWTSettings> jwtSettings, IDateTimeService dateTimeService, IdentityContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
            _dateTimeService = dateTimeService;
            _context = context;
        }



        #region Commands

        //Negocio para Autenticar un usuario 
        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {
            //Verificar si existe el usuario
            var usuario = await _userManager.FindByEmailAsync(request.Email);
            if (usuario == null)
            {
                //Respuesta del server ya no sigue el flujo y se termina ejecución aquí
                throw new ApiException($"No hay una cuenta registrada con el email ${request.Email}.");
            }
            //Verificar las credenciales
            var result = await _signInManager.PasswordSignInAsync(usuario.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                //Respuesta del server ya no sigue el flujo y se termina ejecución aquí
                throw new ApiException($"Las credenciales del usuario no son validas ${request.Email}.");
            }

            //Cuando se verifica que las credenciales esten correctas

            //Se genera el JWT
            JwtSecurityToken jwtSecurityToken = await GenerateJWTToken(usuario);

            //Se genera una clase de tipo AuthenticationResponse la cual devuelve un objeto de respuesta con información
            AuthenticationResponse response = new AuthenticationResponse();

            //Se adjunta a la obj de repuesta para la autenticación,  los datos que contendra
            response.Id = usuario.Id;
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.Email = usuario.Email;
            response.UserName = usuario.UserName;

            //Extramos los roles para adjuntarlos a la respuesta
            var rolesList = await _userManager.GetRolesAsync(usuario).ConfigureAwait(false);

            response.Roles = rolesList.ToList();


            //AQUI SE PODRIA CHECAR LO DE CONFIMRACIÓN DE EMAIL o NÚMERO TELEFONICO 
            //Creando un servicio que se mande llamar aquí
            //Single responsibilite principle
            //Single responsibilite principle

            //Indicamos que ya se confirmó el email
            response.IsVerified = usuario.EmailConfirmed;



            var refreshToken = GenerateRefreshToken(ipAddress);
            response.RefreshToken = refreshToken.Token;



            //Regresamos la Respuesta con un TOKEN detro 
            return new Response<AuthenticationResponse>(response, $"Usuario Autenticado {usuario.UserName}");
        }

        //Negocio para Borrar usuario
        public async Task<Response<string>> DeleteUserAsync(DeleteUserCommand request)
        {
            var userToDelete = await _userManager.FindByIdAsync(request.IdUser);

            if (userToDelete == null)
            {
                throw new KeyNotFoundException($"Usuario no encontrado con el Id: {request.IdUser}");

            }
            else
            {

                var result = await _userManager.DeleteAsync(userToDelete);

                if (result.Succeeded)
                {
                    return new Response<string>(userToDelete.UserName, "Se eliminó correctamente");

                }
                else
                {

                    throw new ApiException($"{result.Errors}.");


                }


            }

        }

        //Negocio para registrar un usuario
        public async Task<Response<string>> RegisterAsync(RegisterRequest request, string origin)
        {
            //Se verifica que el USUARIO(Nombre) no exista
            var usuarioConElMismoUserName = await _userManager.FindByNameAsync(request.UserName);

            //Si ya existía
            if (usuarioConElMismoUserName != null)
            {
                throw new ApiException($"El nombre de usuario {request.UserName} ya fue registrado previamente.");
            }


            //Cuando se sigue el flujo si el usuario no existe

            //Se toman los datos de la request de regitro
            var usuario = new ApplicationUser
            {
                Email = request.Email,
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                UserName = request.UserName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            //Se verifica que el Email no este registrado ya
            var usuarioConElMismoCorreo = await _userManager.FindByEmailAsync(request.Email);
            if (usuarioConElMismoCorreo != null)
            {
                throw new ApiException($"El email {request.Email} ya fue registrado previamente.");
            }
            else
            {
                //Se crea el usuario
                var result = await _userManager.CreateAsync(usuario, request.Password);

                if (result.Succeeded)
                {
                    //Se le asigna el rol 
                    await _userManager.AddToRoleAsync(usuario, Roles.Basic.ToString());
                    //Regresamos este un mensaje de exito con el nombre del usuario
                    return new Response<string>(usuario.Id, message: $"Usuario registrado exitosamente. {request.UserName}");
                }
                else
                {
                    //Manejar mejor los errores debido a que viene en lista
                    throw new ApiException($"{result.Errors}.");
                }
            }
        }

        //Negocio para Actualizar usuario
        public async Task<Response<string>> UpdateUserAsync(UpdateUserCommand request)
        {

            var userToModify = await _userManager.FindByIdAsync(request.IdUser);

            if (userToModify == null)
            {
                throw new KeyNotFoundException($"Usuario no encontrado con el Id: {request.IdUser}");


            }
            else
            {

                userToModify.Nombre = request.Nombre;
                userToModify.Apellido = request.Apellido;
                userToModify.Email = request.Email;
                userToModify.UserName = request.UserName;

                var result = await _userManager.UpdateAsync(userToModify);

                if (result.Succeeded)
                {

                    return new Response<string>(userToModify.Id, message: $"Usuario modificado exitosamente. {request.UserName}");


                }
                else
                {
                    throw new ApiException($"{result.Errors}.");


                }



            }

        }


        #endregion



        #region Querys

        public async Task<Response<List<UsuarioDto>>> GetAllUsers()
        {

            var lstUsers = await (from user in _context.Users
                                  join userRoles in _context.UserRoles on user.Id equals userRoles.UserId
                                  join role in _context.Roles on userRoles.RoleId equals role.Id
                                  select new UsuarioDto(user.Id,user.Nombre,user.Apellido,user.UserName,user.PasswordHash,user.Email,role.Id,role.Name))
                        .ToListAsync();


            return new Response<List<UsuarioDto>>(lstUsers);


        }

        


        #endregion





        //Método privado para generar el Token al usuario
        private async Task<JwtSecurityToken> GenerateJWTToken(ApplicationUser usuario)
        {

            var userClaims = await _userManager.GetClaimsAsync(usuario);

            //Obtenemos la lista de roles
            var roles = await _userManager.GetRolesAsync(usuario);

            //Creamos una lista donde se guardará los claims con el rol/roles del usuario que se este autenticando
            var roleClaims = new List<Claim>();



            for (int i = 0; i < roles.Count; i++)
            {
                //Añadimos el claim a la lista claims para roles
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            //Obtenemos la ip mediante el método estatico de la clase que creamos para llevar la recuperación de la ip
            string ipAddress = IpHelper.GetIpAddress();


            //Definiendo los claims o información de contendrá el TOKEN
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim("uid", usuario.Id),
                new Claim("ip", ipAddress),
            }
            .Union(userClaims)//A{ñadimos los claims a la lista de claims
            .Union(roleClaims);



            //Se extrae el secret 
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

            //Se firma el TOKEN con el secret cifrado en SHA256
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);


            //Se juntan todos los elementos del TOKEN
            var jwtSecurityToken = new JwtSecurityToken(

                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials
                );


            //Retornamos el TOKEN
            return jwtSecurityToken;
        }
   
        
        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now,
                CreatedByIp = ipAddress
            };
        }





        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }








    }

  
}
