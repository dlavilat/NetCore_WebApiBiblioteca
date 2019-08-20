using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiBiblioteca.Entities.Helpers
{
    public class PrimeraLetraMayusculaAttribute: ValidationAttribute
    {
        //Se sobreescribe el método IsValid
        //el value corresponde al valor del atributo donde se coloca la validación
        //validationContext nos trae información del contexto en el cual se ejecuta la validación
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //Aquí se valida si el campo es null o vacío y se retorna succes,
            //la razón por la que retorna succes, es porque ya hay una validación llamada
            //Required que lo va a detectar
            if(value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }
            //Se extrae la primer letra de la palabra
            var firstLetter = value.ToString()[0].ToString();

            //Se valida si firstLetter (la primer letra) no está en mayúscula
            if(firstLetter != firstLetter.ToUpper())
            {
                return new ValidationResult("La primera letra debe ser mayúscula");
            }

            return ValidationResult.Success;
        }
    }
}
