using System;
using System.Collections.Generic;
using Comdis.Models.VM;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Comdis.Helpers
{
    public class FormValidationHelper
    {
        
        public static List<FormField> processErrorsInForm(List<KeyValuePair<string, ModelStateEntry>> pErros)
        {
            List<FormField> errorsInForm = new List<FormField>();

            foreach (var item in pErros)
            {

                foreach (var error in item.Value.Errors)
                {
                    FormField newError = new FormField();
                    newError.FieldName = item.Key;
                    newError.Message = error.ErrorMessage;

                    errorsInForm.Add(newError);

                }

            }

            return errorsInForm;
        }

        public FormValidationHelper()
        {
        }
    }
}
