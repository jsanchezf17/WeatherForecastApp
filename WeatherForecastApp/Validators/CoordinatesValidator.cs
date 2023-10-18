namespace WeatherForecastApp.Validators
{
    public class CoordinatesValidator : ICoordinatesValidator
    {
        /// <summary>
        /// Validates the Value of Latitude
        /// </summary>
        /// <param name="latitude"></param>
        /// <returns></returns>
        private bool IsValidLatitude(double latitude) 
        {
            return latitude >= -90.0 && latitude <= 90.0;
        }

        /// <summary>
        /// Validates the Value of Longitude
        /// </summary>
        /// <param name="longitude"></param>
        /// <returns></returns>
        private bool IsValidLongitude(double longitude)
        {
            return longitude >= -180.0 && longitude <= 180.0;
        }

        /// <summary>
        /// Validates both Latitude and Longitude Values are within the allowed Range
        /// And Throws errors
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public void ValidateCoordinates(double latitude, double longitude)
        {
            var validationErrors = new List<string>();

            //Validate Latitude Value
            if (!IsValidLatitude(latitude))
            {
                validationErrors.Add($"The Latitude input of {latitude} is outside the range of valid values (-90 to 90)");
            }

            //Validate Longitude Value
            if (!IsValidLongitude(longitude))
            {
                validationErrors.Add($"The Longitude input of {longitude} is outside the range of valid values (-180 to 180)");
            }


            if(validationErrors.Count > 0)
            {
                throw new Exception(concatErrors(validationErrors));
            }

        }

        private string concatErrors(List<string> errors)
        {
            string listOfErrors = string.Join("\n", errors);
            return listOfErrors;
        }
    }
}
