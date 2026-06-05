using Garantias.API.Models;

namespace Garantias.API.Helpers
{
    public static class TipoDanoHelper
    {
        public static string GetDescripcion(TipoDanoEnum tipo)
        {
            return tipo switch
            {
                TipoDanoEnum.DanoDeFabrica => "Daño de fábrica",
                TipoDanoEnum.DanoDeUsuario => "Daño de usuario",
                TipoDanoEnum.Software => "Software",
                _ => "Desconocido"
            };
        }
    }
}