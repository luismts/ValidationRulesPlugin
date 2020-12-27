using Plugin.ValidationRules.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Plugin.ValidationRules
{
    public class MapperValidator<Validator, Model> where Model : new()
    {
        public Model Map(Validator validator) 
        {
            if (validator == null)
                return default(Model);

            Model newModel = new Model();

            Type modelObjectType = newModel.GetType();
            PropertyInfo[] modelPropList = modelObjectType.GetProperties();

            Type validatorType = validator.GetType();
            PropertyInfo[] validatorPropList = validatorType.GetProperties();

            foreach (PropertyInfo validatorPropInfo in validatorPropList)
            {
                foreach (PropertyInfo modelPropInfo in modelPropList)
                {
                    if (modelPropInfo.Name == validatorPropInfo.Name)
                    {
                        try
                        {
                            PropertyInfo validatorProp = validatorPropInfo.PropertyType.GetProperty(nameof(Validatable<string>.Value));
                            
                            if (validatorProp == null)
                                break;

                            var validatorPropValue = validatorPropInfo.GetValue(validator); // Not working directly
                            var propValue = validatorProp.GetValue(validatorPropValue, null);

                            modelPropInfo.SetValue(newModel, propValue, null);                            
                        }
                        catch (Exception) { }

                        break;
                    }
                }
            }
            
            return newModel;
        }

        
    }
}
